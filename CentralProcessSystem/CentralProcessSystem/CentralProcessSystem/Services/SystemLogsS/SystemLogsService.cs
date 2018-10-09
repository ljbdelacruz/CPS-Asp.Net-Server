using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.SystemLogsS
{
    public static class SystemLogsService
    {
        public static bool Insert(Guid id, string desc, Guid oid, Guid aid, Guid dtid, bool ia) {
            try {
                var data = SystemLogsVM.set(id, desc, oid, aid, dtid, ia);
                using (var context = new CentralProcessContext()) {
                    context.SystemLogsDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SystemLogsDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    context.SystemLogsDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid aid, string desc, Guid dtid, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SystemLogsDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    query.Description = desc;
                    query.DateTimeStorageID = dtid;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<SystemLogs> GetByOIDAPI(Guid oid, Guid api) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.SystemLogsDB where i.OwnerID == oid && i.API == api select i).ToList();
                return query;
            }
        }
        


    }
}