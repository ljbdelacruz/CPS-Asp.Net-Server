using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.MyCompanyInformationS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.UserInfoS
{
    public static class UserAccessLevelService
    {
        #region db queries
        public static bool Insert(Guid id, Guid uid, Guid alid, Guid aid, Guid dtid, bool ia) {
            try {
                var data = UserAccessLevelVM.set(id, uid, alid, aid, dtid, ia);
                using (var context = new CentralProcessContext()){
                    context.UserAccessLevelDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid uid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.UserAccessLevelDB where i.ID == id && i.UserID == uid && i.ApplicationID == aid select i).FirstOrDefault();
                    context.UserAccessLevelDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<UserAccessLevel> GetByUIDAID(Guid uid, Guid aid, bool ia) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UserAccessLevelDB where i.UserID == uid && i.ApplicationID == aid && i.isArchived == ia select i).ToList();
                return query;
            }
        }
        public static List<UserAccessLevel> GetByUID(Guid uid, bool ia) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.UserAccessLevelDB where i.UserID == uid && i.isArchived == ia select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static UserAccessLevelVM SetSubData(UserAccessLevel item, Guid aid)
        {
            var model = UserAccessLevelVM.MToVM(item);
            model.AccessLevel = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(item.AccessLevelID));
            model.DateTimeData = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(item.DateTimeStorageID));
            return model;
        }
        public static List<UserAccessLevelVM> SetSubDatas(List<UserAccessLevel> list, Guid aid) {
            var nlist = new List<UserAccessLevelVM>();
            foreach (var item in list){
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        public static UserAccessLevelVM SetSubDataAdmin(UserAccessLevel model) {
            var data = UserAccessLevelVM.MToVM(model);
            data.AccessLevel = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(model.AccessLevelID));
            data.DateTimeData = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(model.DateTimeStorageID));
            data.Application = ApplicationInformationVM.MToVM(ApplicationInformationService.GetByID(model.ApplicationID));
            return data;
        }
        public static List<UserAccessLevelVM> SetSubDatasAdmin(List<UserAccessLevel> models) {
            var list = new List<UserAccessLevelVM>();
            foreach (var model in models) {
                list.Add(SetSubDataAdmin(model));
            }
            return list;
        }

        #endregion

        #region security modules
        public static bool IsSuperAdminAccess(Guid uid) {
            var uals = GetByUID(uid, false);
            if (uals.Where(x => x.AccessLevelID == Guid.Parse("3c35cccc-d48d-4721-9283-d58faeac6cc1")).Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool HasAccess(Guid uid, Guid alid) {
            var uals = GetByUID(uid, false);
            if (uals.Where(x => x.AccessLevelID == alid).Count() > 0)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}