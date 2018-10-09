using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.DateTimeStorageS
{
    public static class DateTimeStorageService
    {
        #region db queries
        public static bool Insert(Guid id, Guid oid, Guid aid, DateTime ca, DateTime ua, Guid cid) {
            try {
                var data = DateTimeStorageVM.set(id, oid, aid, ca, ua, cid);
                using (var context = new CentralProcessContext()) {
                    context.DateTimeStorageDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid, Guid oid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.DateTimeStorageDB where i.ID == id && i.API == aid && i.OwnerID == oid select i).FirstOrDefault();
                    context.DateTimeStorageDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        //use only on server side do not build api on this
        public static bool RemoveAdmin(Guid id) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.DateTimeStorageDB where i.ID == id select i).FirstOrDefault();
                    context.DateTimeStorageDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static bool Update(Guid id, Guid oid, DateTime ua) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.DateTimeStorageDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                    query.UpdatedAt = ua;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<DateTimeStorage> GetByOID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.DateTimeStorageDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        }
        public static DateTimeStorage GetByID(Guid id) {
            using(var context=new CentralProcessContext())
            {
                var query = (from i in context.DateTimeStorageDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
        #region util
        public static DateTime GetByTZ(string tz) {
            return DateTimeUtil.GetTimeNowByUTC(tz);
        }
        public static bool InsertByTZ(Guid id, Guid oid, Guid aid, string tz, Guid cid) {
            try {
                var time = DateTimeUtil.GetTimeNowByUTC(tz);
                if (Insert(id, oid, aid, time, time, cid)){
                    return true;
                }
                return false;
            } catch { return false; }

        }
        #endregion


    }
}