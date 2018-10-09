using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.UserInfoS
{
    public static class UsersService
    {
        #region Db queries
        public static bool Insert(Guid id, string fname, string lname, string mname, string add, string email, string pass, string cnum, bool isAllow, Guid areg, Guid profID, Guid dtid) {
            try {
                var data = UsersVM.set(id, fname, lname, mname, add, email, pass, cnum, isAllow, areg, profID, dtid);
                using (var context = new CentralProcessContext()) {
                    context.UsersDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string fname, string lname, string mname, string add, string email, string pass, string cnum, bool isAllow, Guid areg, Guid profID, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.UsersDB where i.ID == id select i).FirstOrDefault();
                    query.Firstname = fname;
                    query.Lastname = lname;
                    query.MiddleName = mname;
                    query.Address = add;
                    query.EmailAddress = email;
                    query.Password = pass;
                    query.ContactNumber = cnum;
                    query.isAllowAccess = isAllow;
                    query.ApplicationRegistered = areg;
                    query.ProfileImageID = profID;
                    query.DateTimeStorageID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateIsAllowAccess(Guid id, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.UsersDB where i.ID == id select i).FirstOrDefault();
                    query.isAllowAccess = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static bool UpdatePassword(Guid id, string password) {
            try
            {
                using (var context = new CentralProcessContext()){
                    var query = (from i in context.UsersDB where i.ID == id select i).FirstOrDefault();
                    query.Password = password;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateProfileImage(Guid id, Guid profID) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.UsersDB where i.ID == id select i).FirstOrDefault();
                    query.ProfileImageID = profID;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static Users GetByEmailAddress(string email) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UsersDB where i.EmailAddress.Equals(email) select i).FirstOrDefault();
                return query;
            }
        }
        public static Users GetByIDEmail(Guid id, string email) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UsersDB where i.ID == id && i.EmailAddress.Equals(email) select i).FirstOrDefault();
                return query;
            }
        }
        public static List<Users> GetByContactNumber(string cnum) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UsersDB where i.ContactNumber.Equals(cnum) select i).ToList();
                return query;
            }
        }
        public static Users GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UsersDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<Users> GetAll() {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UsersDB select i).ToList();
                return query;
            }
        }

        #endregion
        #region functionalities
        public static bool IsUserExist(Guid id) {
            try {
                if (GetByID(id) != null) {
                    //user exist
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public static string GenerateHashPassword(string password)
        {
            var hasher = new PasswordHasher();
            return hasher.HashPassword(password);
        }
        public static bool ComparePassword(string hashedPassword, string providedPassword)
        {
            var hasher = new PasswordHasher();
            var result=hasher.VerifyHashedPassword(hashedPassword, providedPassword);
            switch (result)
            {
                case PasswordVerificationResult.Success:
                    return true;
                case PasswordVerificationResult.Failed:
                    return false;
                default:
                    return false;
            }
        }
        //setting sub data sub classes of this class properties are set here
        public static UsersVM SetSubData(Users item, Guid aid) {
            var model = UsersVM.MToVM(item);
            model.DateTimeData = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByOID(item.DateTimeStorageID, aid).FirstOrDefault());
            model.ProfileImage = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByIDAdmin(item.ProfileImageID));
            return model;
        }
        public static List<UsersVM> SetSubDatas(List<Users> list, Guid aid)
        {
            var nlist = new List<UsersVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }

        public static UsersVM SetSubDataAdmin(Users item) {
            var model = UsersVM.MToVM(item);
            model.DateTimeData = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(item.DateTimeStorageID));
            model.ProfileImage = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByIDAdmin(item.ProfileImageID));
            return model;
        }
        public static List<UsersVM> SetSubDatasAdmin(List<Users> items) {
            var list = new List<UsersVM>();
            foreach (var model in items) {
                list.Add(SetSubDataAdmin(model));
            }
            return list;
        }

        #endregion
        #region securityModule
        public static bool HasUserAccess(string email, string pass,Guid  accessID) {
            try {
                var data = GetByEmailAddress(email);
                if (ComparePassword(data.Password, pass)) {
                    if (UserAccessLevelService.HasAccess(data.ID, accessID)) {
                        return true;
                    }
                }
                return false;
            } catch { return false; }
        }
        #endregion
    }
}