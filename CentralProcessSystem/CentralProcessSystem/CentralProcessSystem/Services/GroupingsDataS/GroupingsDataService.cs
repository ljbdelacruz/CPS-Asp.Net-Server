using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.MessagingAppS;
using CentralProcessSystem.Services.StatusReferenceS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.GroupingsDataS
{
    public static class GroupingsDataService
    {
        #region db queries
        public static bool Insert(Guid id, Guid sid, Guid oid, Guid aid, int order, Guid dtid, bool ia, Guid catID) {
            try {
                var data = GroupingsDataVM.set(id, sid, oid, aid, order, dtid, ia, catID);
                using (var context = new CentralProcessContext()) {
                    context.GroupingsDataDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.GroupingsDataDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    context.GroupingsDataDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid aid, Guid sid, int order, bool ia) {
            try {
                using(var context=new CentralProcessContext()) {
                    var query = (from i in context.GroupingsDataDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    query.SourceID = sid;
                    query.Order = order;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<GroupingsData> GetByOIDAID(Guid oid, Guid aid, bool ia){
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.GroupingsDataDB where i.OwnerID == oid && i.API == aid && i.isArchived==ia select i).ToList();
                return query;
            }
        }
        public static List<GroupingsData> GetByOIDCIDAID(Guid oid, Guid cid, Guid aid, bool ia) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.GroupingsDataDB where i.OwnerID == oid && i.API == aid && i.CategoryID == cid && i.isArchived==ia select i).ToList();
                return query;
            }
        }
        public static List<GroupingsData> GetBySIDCIDAID(Guid sid, Guid cid, Guid aid, bool ia) {
            using (var context = new CentralProcessContext())
            {
                var query = (from i in context.GroupingsDataDB where i.SourceID == sid && i.API == aid && i.CategoryID == cid && i.isArchived == ia select i).ToList();
                return query;
            }
        }
        public static List<GroupingsData> GetBySIDCID(Guid sid, Guid cid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.GroupingsDataDB where i.SourceID == sid && i.CategoryID == cid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static GroupingsDataVM SetSubData(GroupingsData data, Guid aid) {
            var model = GroupingsDataVM.MToVM(data);
            model.DateTimeData = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            model.CategoryStatus = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByOIDAID(data.CategoryID, aid).FirstOrDefault());
            return model;
        }
        public static GroupingsDataVM SetSubDataMessagingRoom(GroupingsData model, Guid aid) {
            var data = SetSubData(model, aid);
            data.User = UsersVM.MToVM(UsersService.GetByID(model.OwnerID));
            data.MessagingRoom = MessagingRoomVM.MToVM(MessagingRoomService.GetByID(model.SourceID, aid));
            return data;
        }

        /*
            0-normal setSubData, 1-messagingSubData 
        */
        public static List<GroupingsDataVM> SetSubDatas(List<GroupingsData> list, Guid aid, int option){
            var nlist = new List<GroupingsDataVM>();
            foreach (var item in list){
                var model = option==0? SetSubData(item, aid) : SetSubDataMessagingRoom(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        public static bool RemoveByList(List<GroupingsData> list) {
            try {
                foreach (var item in list) {
                    Remove(item.ID, item.OwnerID, item.API);
                }
                return true;
            } catch { return false; }
        }
        #endregion


    }
}