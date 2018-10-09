using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.DescriptionStorageS
{
    public class DescriptionStorageService
    {

        #region db query
        public static bool Insert(Guid id, string title, string desc, Guid oid, Guid aid, Guid cid, int order) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = DescriptionStorageVM.set(id, title, desc, oid, aid, cid, order);
                    context.DescriptionStorageDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid){
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.DescriptionStorageDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    context.DescriptionStorageDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool RemoveByOID(Guid oid, Guid aid) {
            try {
                var data = GetByOID(oid, aid);
                foreach (var model in data){
                    Remove(model.ID, model.OwnerID, model.API);
                }
                return true;
            } catch { return false; }
        }

        public static DescriptionStorage GetByID(Guid id, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.DescriptionStorageDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                return query;
            }
        }
        public static List<DescriptionStorage> GetByOID(Guid id, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.DescriptionStorageDB where i.OwnerID == id && i.API == aid select i).ToList();
                return query;
            }
        }
        public static List<DescriptionStorage> GetByOIDCID(Guid oid, Guid aid, Guid cid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.DescriptionStorageDB where i.OwnerID == oid && i.API == aid && i.CategoryID == cid select i).ToList();
                return query;
            }
        }

        #endregion
        #region util
        public static DescriptionStorageVM SetSubData(DescriptionStorage model) {
            var data = DescriptionStorageVM.MToVM(model);
            data.Category = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(model.CategoryID));
            return data;
        }
        public static List<DescriptionStorageVM> SetSubDatas(List<DescriptionStorage> models) {
            var list = new List<DescriptionStorageVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model));
            }
            return list;
        }
        #endregion

    }
}