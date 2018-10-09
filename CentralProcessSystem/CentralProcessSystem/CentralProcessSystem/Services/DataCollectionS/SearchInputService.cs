using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.DataCollectionS
{
    public static class SearchInputService
    {
        #region db queries
        public static bool Insert(Guid id, string sinput, Guid oid, Guid  dtid, Guid api) {
            try {
                var data = SearchInputsDataVM.set(id, sinput, oid, dtid, api);
                using (var context = new CentralProcessContext()) {
                    context.SearchInputDataDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid api) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SearchInputDataDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    context.SearchInputDataDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string sinput, Guid oid, Guid dtid, Guid api) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.SearchInputDataDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    query.SearchInput = sinput;
                    query.DateTimeID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<SearchInputsData> GetByOIDAPI(Guid oid, Guid api) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.SearchInputDataDB where i.OwnerID == oid && i.API == api select i).ToList();
                return query;
            }
        }
        public static List<SearchInputsData> GetByOID(Guid oid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.SearchInputDataDB where i.OwnerID == oid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities

        #endregion

    }
}