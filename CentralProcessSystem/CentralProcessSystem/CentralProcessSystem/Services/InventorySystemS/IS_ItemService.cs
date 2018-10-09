using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.InventorySystemS
{
    public static class IS_ItemService
    {
        #region db queries
        public static bool Insert(Guid id, string title, string desc, float price, Guid aid, Guid oid, Guid icid, bool ic, int q, Guid dtid, Guid cid) {
            try {
                using (var context = new CentralProcessContext()){
                    var data = IS_ItemVM.set(id, title, desc, price, aid, oid, icid, ic, q, dtid, cid);
                    context.IS_ItemDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid, Guid oid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.IS_ItemDB where i.ID == id && i.API == aid && i.OwnerID == oid select i).FirstOrDefault();
                    context.IS_ItemDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid aid, Guid icid, string title, string desc, float price, bool ic, int q) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.IS_ItemDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    query.ItemCategoryID = icid;
                    query.Title = title;
                    query.Description = desc;
                    query.Price = price;
                    query.isCount = ic;
                    query.Quantity = q;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static IS_Item GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<IS_Item> GetByOID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        } 
        public static List<IS_Item> GetByOIDICID(Guid oid, Guid icid, Guid aid) {
            using (var context = new CentralProcessContext()){
                var query = (from i in context.IS_ItemDB where i.OwnerID == oid && i.ItemCategoryID == icid  && i.API == aid select i).ToList();
                return query;
            }
        }
        public static List<IS_Item> GetByICID(Guid icid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemDB where i.ItemCategoryID == icid && i.API == aid select i).ToList();
                return query;
            }
        }
        public static List<IS_Item> GetByContains(string input, Guid aid, int top) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemDB where i.Title.Contains(input) && i.API == aid select i).Take(top).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static IS_ItemVM SetSubData(IS_Item data, Guid aid)
        {
            var model = IS_ItemVM.MToVM(data);
            model.DateTimeStorage = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            model.ItemCategory = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.ItemCategoryID));
            model.ItemColors = IS_ItemColorService.SetSubDatas(IS_ItemColorService.GetByOwnerID(data.ID, aid));
            model.Images = ImageLinkStorageVM.MsToVMs(ImageLinkStorageService.GetByOIDAPI(data.ID, aid));
            model.Condition = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.ConditionID));
            return model;
        }
        public static List<IS_ItemVM> SetSubDatas(List<IS_Item> list, Guid aid)
        {
            var nlist = new List<IS_ItemVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        public static List<IS_ItemVM> SortByDateTime(List<IS_ItemVM> models, bool isDesc){
            return isDesc ? models.OrderByDescending(x => x.DateTimeStorage.UpdatedAt).ToList() : models.OrderBy(x => x.DateTimeStorage.UpdatedAt).ToList();
        }
        #endregion
    }
}