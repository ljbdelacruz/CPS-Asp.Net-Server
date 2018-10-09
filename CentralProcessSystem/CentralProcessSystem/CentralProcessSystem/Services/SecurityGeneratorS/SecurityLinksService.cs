using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.SecurityGeneratorS
{
    public static class SecurityLinksService
    {
        #region db query
        public static bool Insert(Guid id, Guid cid, string url, Guid oid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = SecurityLinksVM.set(id, cid, url, oid);
                    context.SecurityLinksDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SecurityLinksDB where i.ID == id select i).FirstOrDefault();
                    context.SecurityLinksDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static SecurityLinks GetByURL(string url) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.SecurityLinksDB where i.URL.Equals(url) select i).FirstOrDefault();
                return query;
            }
        }
        #endregion

    }
}