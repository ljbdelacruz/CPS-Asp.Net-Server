using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.InventorySystemS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.ShopManagementS
{
    public static class ShopManagementService
    {
        #region db query
        public static bool Insert(Guid id, string name, string desc, Guid oid, Guid aid, Guid pi, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = ShopManagementVM.set(id, name, desc, oid, aid, pi, cid);
                    context.ShopManagementDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ShopManagementDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    context.ShopManagementDB.Remove(query);
                    context.SaveChanges();
                    //remove profile image
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdatePI(Guid id, Guid oid, Guid aid, Guid pi) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ShopManagementDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    query.ProfileImage = pi;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static ShopManagement GetByID(Guid id, Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ShopManagementDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                return query;
            }
        }

        public static List<ShopManagement> GetByOID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ShopManagementDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        }


        #endregion
        #region util
        public static ShopManagementVM SetSubData(ShopManagement model, bool isSetItem) {
            var data = ShopManagementVM.MToVM(model);
            data.ProfileImage = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByID(model.ProfileImage, model.ID, model.API));
            data.Items = isSetItem ? IS_ItemService.SetSubDatas(IS_ItemService.GetByOID(model.ID, model.API), model.API) : new List<IS_ItemVM>();
            return data;
        }
        public static List<ShopManagementVM> SetSubDatas(List<ShopManagement> models, bool isSetItems) {
            var list = new List<ShopManagementVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model, isSetItems));
            }
            return list;
        }

        #endregion
    }
}