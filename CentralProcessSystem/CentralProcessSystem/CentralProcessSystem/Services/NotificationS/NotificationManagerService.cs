using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.NotificationS
{
    public static class NotificationManagerService
    {
        #region db queries
        public static bool Insert(Guid id, string title, string message, Guid oid, Guid aid, Guid dtID, bool ir, bool ia) {
            try {
                var data = NotificationManagerVM.set(id, title, message, oid, aid, dtID, ir, ia);
                using (var context = new CentralProcessContext()) {
                    context.NotificationManagerDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.NotificationManagerDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    context.NotificationManagerDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid aid, string title, string message, Guid dtid, bool ir, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.NotificationManagerDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    query.Title = title;
                    query.Message = message;
                    query.DateTimeStorageID = dtid;
                    query.isRead = ir;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<NotificationManager> GetByOIDAID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext())
            {
                var query = (from i in context.NotificationManagerDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        }
        public static NotificationManager GetByID(Guid id, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.NotificationManagerDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static NotificationManagerVM SetSubData(NotificationManager data, Guid aid)
        {
            var model = NotificationManagerVM.MToVM(data);
            model.DateTime = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            return model;
        }
        public static List<NotificationManagerVM> SetSubDatas(List<NotificationManager> list, Guid aid)
        {
            var nlist = new List<NotificationManagerVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }

        #endregion

    }
}