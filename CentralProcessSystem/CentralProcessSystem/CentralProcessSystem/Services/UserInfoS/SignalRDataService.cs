using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.UserInfoS
{
    public static class SignalRDataService
    {
        #region db query
        public static bool Insert(Guid id, Guid oid, Guid sid, Guid api, Guid dtid, string hub, bool ia) {
            try {
                var data = SignalRDataVM.set(id, oid, sid, api, dtid, hub, ia);
                using (var context = new CentralProcessContext()) {
                    context.SignalRDataDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid api) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SignalRDataDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    context.SignalRDataDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid api, Guid sid, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SignalRDataDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    query.SignalRID = sid;
                    query.DateTimeID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateActive(Guid id, Guid aid, string hub, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SignalRDataDB where i.ID == id && i.API == aid && i.Hub.Equals(hub) select i).FirstOrDefault();
                    query.isActive = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static SignalRData GetByOIDAPI(Guid id, Guid api) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.SignalRDataDB where i.OwnerID == id && i.API == api select i).FirstOrDefault();
                return query;
            }
        }
        #endregion

    }
}