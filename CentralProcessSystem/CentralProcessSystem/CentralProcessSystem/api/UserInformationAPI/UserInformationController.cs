using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.LocationStorageS;
using CentralProcessSystem.Services.MyCompanyInformationS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.UserInformationAPI
{
    public class UserInformationController : Controller
    {
        private Guid superAdmin = Guid.Parse("3c35cccc-d48d-4721-9283-d58faeac6cc1");
        #region SignalRData
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SRDInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var h = Request.Form["h"];
                if (SignalRDataService.Insert(id, oid, sid, api, dtid, h, false)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Signal"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SRDRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                if (SignalRDataService.Remove(id, oid, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Signal"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SRDUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (SignalRDataService.Update(id, oid, api, sid, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Signal"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SRDGetOwner(string id, string aid) {
            try {
                var data = SignalRDataService.GetByOIDAPI(Guid.Parse(id), Guid.Parse(aid));
                return Success(SignalRDataVM.MToVM(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #endregion
        #region userAccessLevel
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UALInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var alid = Guid.Parse(Request.Form["alid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (UserAccessLevelService.Insert(id, uid, alid, aid, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert(""));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UALRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (UserAccessLevelService.Remove(id, uid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove(""));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UALGetByList(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
        
                var data = UserAccessLevelService.GetByUIDAID(id, aid, false);
                if (data.Count <= 0) {
                    if (InsertUserAccessLevel(aid, id)){
                        data = UserAccessLevelService.GetByUIDAID(id, aid, false);
                    }
                }
                var vms = UserAccessLevelService.SetSubDatas(data, aid);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UALGetByUID() {
            try {
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                var uid = Guid.Parse(Request.Form["uid"]);
                if (UsersService.HasUserAccess(email, pass, superAdmin)) {
                    var data = UserAccessLevelService.GetByUID(uid, false);
                    return Success(UserAccessLevelService.SetSubDatasAdmin(data));
                }
                return Failed(MessageUtilityService.ContactAdmin("Content"));
            } catch {
                return Failed(MessageUtilityService.ServerError());
            }
        }

        #endregion
        #endregion
        #region User
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UInsert() {
            var dtid = Guid.Parse(Request.Form["dtid"]);
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var fname = Request.Form["fname"];
                var lname = Request.Form["lname"];
                var mname = Request.Form["mname"];
                var add = Request.Form["add"];
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                var repass = Request.Form["rpass"];
                var cnum = Request.Form["cnum"];
                var isAllow = Boolean.Parse(Request.Form["ia"]);
                var areg = Guid.Parse(Request.Form["areg"]);
                var profID = Guid.Parse(Request.Form["profid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var hashedPassword = UsersService.GenerateHashPassword(pass);
                if(ApplicationInformationService.GetByID(areg) == null)
                {
                    DateTimeStorageService.RemoveAdmin(dtid);
                    return Failed("Please do not modify link to complete the signup process "+areg);
                }
                if (!pass.Equals(repass)){
                    DateTimeStorageService.RemoveAdmin(dtid);
                    return Failed("Make sure password matches retype password");
                }
                if (ValidateEmailAddress(email)) {
                    if (ValidateContactNumber(cnum))
                    {
                        if (UsersService.Insert(id, fname, lname, mname, add, email, hashedPassword, cnum, isAllow, areg, profID, dtid))
                        {
                            InsertNewUserInformation(id, aid, Guid.Parse("7d789492-1c6c-4ea2-9e1e-893a68620d1e"));
                            return Success(id.ToString());
                        }
                    }
                    else {
                        DateTimeStorageService.RemoveAdmin(dtid);
                        return Failed(MessageUtilityService.InUse("Mobile Number"));
                    }
                }
                DateTimeStorageService.RemoveAdmin(dtid);
                return Failed(MessageUtilityService.FailedInsert("Signup"));
            } catch {
                DateTimeStorageService.RemoveAdmin(dtid);
                return Failed(MessageUtilityService.ServerError());
            }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UAuthenticate() {
            try {
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var userInfo = UsersService.GetByEmailAddress(email);
                if (userInfo == null) {
                    return Failed(MessageUtilityService.AuthenticationFailed());
                }
                if (userInfo.isAllowAccess) {
                    if (UsersService.ComparePassword(userInfo.Password, pass)){
                        var vm = UsersService.SetSubData(userInfo, aid);
                        if (UserAccessLevelService.HasAccess(userInfo.ID, superAdmin)) {
                            vm.Password = pass;
                        }
                        return Success(vm);
                    }
                }
                return Failed(MessageUtilityService.AuthenticationFailed());
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UUpdatePassword() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var npass = Request.Form["npass"];
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                if (UsersService.HasUserAccess(email, pass, superAdmin)) {
                    var userInfo = UsersService.GetByID(id);
                    if (UsersService.Update(id, userInfo.Firstname, userInfo.Lastname, userInfo.MiddleName, userInfo.Address, userInfo.EmailAddress, UsersService.GenerateHashPassword(npass), userInfo.ContactNumber, userInfo.isAllowAccess, userInfo.ApplicationRegistered, userInfo.ProfileImageID, userInfo.DateTimeStorageID))
                    {
                        return Success(id.ToString());
                    }
                }
                return Failed(MessageUtilityService.FailedUpdate("Password"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UUpdateProfileImage() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var profID = Guid.Parse(Request.Form["profID"]);
                var userInfo = UsersService.GetByID(id);
                if (UsersService.Update(id, userInfo.Firstname, userInfo.Lastname, userInfo.MiddleName, userInfo.Address, userInfo.EmailAddress, userInfo.Password, userInfo.ContactNumber, userInfo.isAllowAccess, userInfo.ApplicationRegistered, profID, userInfo.DateTimeStorageID)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Profile Image"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UUpdateAccess() {
            try {
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                //super admin
                var uid = Guid.Parse(Request.Form["uid"]);
                var access = Boolean.Parse(Request.Form["iaa"]);
                if (UsersService.HasUserAccess(email, pass, superAdmin))
                {
                    var data = UsersService.GetByID(uid);
                    if (UsersService.Update(data.ID, data.Firstname, data.Lastname, data.MiddleName, data.Address, data.EmailAddress, data.Password, data.ContactNumber, access, data.ApplicationRegistered, data.ProfileImageID, data.DateTimeStorageID)) {
                        return Success(uid.ToString());
                    }
                    return Failed(MessageUtilityService.FailedUpdate("User"));
                }
                return Failed(MessageUtilityService.ContactAdmin("Modify Content"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UGetID() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var email = Request.Form["email"];
                var data = UsersService.GetByIDEmail(id, email);
                return Success(UsersService.SetSubDataAdmin(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UGetAdmin() {
            try {
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                if (UsersService.HasUserAccess(email, pass, superAdmin)) {
                    var data = UsersService.GetAll();
                    return Success(UsersService.SetSubDatasAdmin(data));
                }
                return Failed(MessageUtilityService.ContactAdmin("Content"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #endregion
        #region util
        private static bool InsertUserAccessLevel(Guid aid, Guid uid) {
            try {
                var dtid = Guid.NewGuid();
                var ualID = Guid.NewGuid();
                DateTimeStorageService.Insert(dtid, ualID, aid, DateTime.Now, DateTime.Now, Guid.Parse("c4926f90-2be8-4c62-94ed-5399be276f11"));
                //check if application exist and user exist
                if (ApplicationInformationService.IsApplicationExist(aid) && UsersService.IsUserExist(uid)) {
                    //insert new user access level for this user
                    var ual = Guid.Parse("a2e2d83d-dd8d-4a66-bacf-94ad90344ca7");
                    UserAccessLevelService.Insert(ualID, uid, ual, aid, dtid, false);
                }
                return true;
            }
            catch { return false; }
        }

        //validate email address if it is allowed to be used
        private static bool InsertNewUserInformation(Guid userID, Guid aid, Guid locCatID) {
            try {
                var dtid = Guid.NewGuid();
                DateTimeStorageService.Insert(dtid, userID, aid, DateTime.Now, DateTime.Now, Guid.Parse("a2e2d83d-dd8d-4a66-bacf-94ad90344ca7"));
                LocationStorageService.Insert(Guid.NewGuid(), userID, locCatID, 0, 0, "User Location", dtid, false);
                return true;
            } catch { return false; }
        }
        private static bool ValidateEmailAddress(string email) {
            if (UsersService.GetByEmailAddress(email) == null)
            {
                //valid to use
                return true;
            }
            else {
                return false;
            }
        }
        private static bool ValidateContactNumber(string cnum) {
            if (UsersService.GetByContactNumber(cnum).Count <= 0)
            {
                //contact number not in use yet
                return true;
            }
            else {
                return false;
            }
        }
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}