using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.InventorySystemS
{
    public static class IS_ItemColorService
    {
        #region db queries
        public static bool Insert(Guid id, string color, string desc, Guid oid, Guid aid, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = IS_ItemColorVM.set(id, color, desc, oid, aid, cid);
                    context.IS_ItemColorDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.IS_ItemColorDB where i.ID == id && i.OwnerID == oid && i.API == aid && i.CategoryID == cid select i).FirstOrDefault();
                    context.IS_ItemColorDB.Remove(query);
                    context.SaveChanges();
                    if (ImageLinkStorageService.RemoveByOID(query.ID, aid)) {
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }
        public static bool RemoveByOwnerID(Guid oid, Guid aid) {
            try {
                var data = GetByOwnerID(oid, aid);
                foreach (var model in data)
                {
                    Remove(model.ID, model.OwnerID, model.API, model.CategoryID);
                }
                return true;
            } catch { return false; }
        }

        public static IS_ItemColor GetByID(Guid id) {
            using (var context = new CentralProcessContext())
            {
                var query = (from i in context.IS_ItemColorDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }

        public static List<IS_ItemColor> GetByOwnerID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext())
            {
                var query = (from i in context.IS_ItemColorDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        }
        public static List<IS_ItemColor> GetByOIDCID(Guid oid, Guid cid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemColorDB where i.OwnerID == oid && i.CategoryID == cid && i.API == aid select i).ToList();
                return query;
            }
        }

        #endregion

        #region functionalities
        public static IS_ItemColorVM SetSubData(IS_ItemColor model) {
            var data = IS_ItemColorVM.MToVM(model);
            data.Category = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(model.CategoryID));
            data.Images = ImageLinkStorageVM.MsToVMs(ImageLinkStorageService.GetByOIDAPI(model.ID, model.API));
            return data;
        }
        public static List<IS_ItemColorVM> SetSubDatas(List<IS_ItemColor> models) {
            var list = new List<IS_ItemColorVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model));
            }
            return list;
        }
        #endregion
    }
}