using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.StatusReferenceS
{
    public static class StatusTypesReferencesService
    {
        #region db queries
        public static bool Insert(Guid id, string name, string desc, Guid oid, Guid aid, Guid dtid, Guid catID) {
            try {
                var data = StatusTypesReferencesVM.set(id, name, desc, oid, aid, dtid, catID);
                using (var context = new CentralProcessContext()) {
                    context.StatusTypesReferencesDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid, Guid oid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.StatusTypesReferencesDB where i.ID == id && i.API == aid && i.OwnerID == oid select i).FirstOrDefault();
                    context.StatusTypesReferencesDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid aid, Guid oid, string Name, string Description, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.StatusTypesReferencesDB where i.ID == id && i.API == aid && i.OwnerID == oid select i).FirstOrDefault();
                    query.OwnerID = oid;
                    query.Name = Name;
                    query.Description = Description;
                    query.CategoryID = cid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<StatusTypesReferences> GetByOIDAID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.StatusTypesReferencesDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        }
        public static List<StatusTypesReferences> GetByOIDAIDCID(Guid oid, Guid aid, Guid cid) {
            using (var context = new CentralProcessContext()){
                var query = (from i in context.StatusTypesReferencesDB where i.OwnerID == oid && i.API == aid && i.CategoryID == cid select i).ToList();
                return query;
            }
        }
        public static List<StatusTypesReferences> GetAll() {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.StatusTypesReferencesDB select i).ToList();
                return query;
            }
        }
        public static StatusTypesReferences GetByID(Guid id){
            using (var context = new CentralProcessContext()){
                var query = (from i in context.StatusTypesReferencesDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static StatusTypesReferences GetByIDAID(Guid id, Guid aid)
        {
            using (var context = new CentralProcessContext())
            {
                var query = (from i in context.StatusTypesReferencesDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                return query;
            }
        }

        #endregion

        #region util 
        public static StatusTypesReferencesVM SetSubData(StatusTypesReferences model, Guid cid) {
            var data = StatusTypesReferencesVM.MToVM(model);
            data.SubStatus = StatusTypesReferencesService.SetSubDatas(StatusTypesReferencesService.GetByOIDAIDCID(model.ID, model.API, cid), cid);
            return data;
        }
        public static List<StatusTypesReferencesVM> SetSubDatas(List<StatusTypesReferences> models, Guid cid) {
            var list = new List<StatusTypesReferencesVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model, cid));
            }
            return list;
        }
        public static StatusTypesReferencesVM SetSubDataAdmin(StatusTypesReferences model) {
            var data = StatusTypesReferencesVM.MToVM(model);
            data.CategoryID = model.CategoryID.ToString();
            data.OwnerID = model.OwnerID.ToString();
            data.API = model.API.ToString();
            data.Category = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(model.CategoryID));
            return data;
        }
        public static List<StatusTypesReferencesVM> SetSubDatasAdmin(List<StatusTypesReferences> models)
        {
            var list = new List<StatusTypesReferencesVM>();
            foreach (var model in models)
            {
                list.Add(SetSubDataAdmin(model));
            }
            return list;
        }

        #endregion
    }
}