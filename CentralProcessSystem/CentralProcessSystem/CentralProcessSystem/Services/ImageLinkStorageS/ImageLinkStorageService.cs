using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.ImageLinkStorageS
{
    public static class ImageLinkStorageService
    {
        #region db queries
        public static bool Insert(Guid id, Guid oid, string source, Guid api, Guid dtid, bool ia, Guid cid, int order) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = ImageLinkStorageVM.set(id, oid, source, api, dtid, ia, cid, order);
                    context.ImageLinkStorageDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static bool Remove(Guid id, Guid oid, Guid api) {
            try {
                using (var context = new CentralProcessContext()){
                    var query = (from i in context.ImageLinkStorageDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    context.ImageLinkStorageDB.Remove(query);
                    context.SaveChanges();
                    if (DateTimeStorageService.Remove(query.DateTimeStorageID, api, query.ID)) {
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }


        public static bool RemoveByOID(Guid id, Guid api) {
            try {
                var data = GetByOIDAPI(id, api);
                foreach (var model in data) {
                    Remove(model.ID, id, api);
                }
                return true;
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid api, string source, Guid dtid, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ImageLinkStorageDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    query.Source = source;
                    query.DateTimeStorageID = dtid;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static ImageLinkStorage GetByID(Guid id, Guid oid, Guid api) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ImageLinkStorageDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                return query;
            }
        }
        public static ImageLinkStorage GetByIDAdmin(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ImageLinkStorageDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<ImageLinkStorage> GetByOIDAPI(Guid oid, Guid api) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ImageLinkStorageDB where i.OwnerID == oid && i.API == api select i).ToList();
                query = SortByOrder(query, false);
                return query;
            }
        }

        public static List<ImageLinkStorage> GetByOIDCID(Guid oid, Guid aid, Guid cid)
        {
            using (var context = new CentralProcessContext())
            {
                var query = (from i in context.ImageLinkStorageDB where i.OwnerID == oid && i.API == aid && i.CategoryID == cid select i).ToList();
                query = SortByOrder(query, false);
                return query;
            }
        }
        #endregion


        #region util
        public static List<ImageLinkStorage> SortByOrder(List<ImageLinkStorage> list, bool isDesc) {
            return isDesc ? list.OrderByDescending(x => x.Order).ToList() : list.OrderBy(x => x.Order).ToList();
        }
        #endregion
    }
}