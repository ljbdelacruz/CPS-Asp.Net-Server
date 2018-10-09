using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.InventorySystemS
{
    public static class IS_ItemStockService
    {
        #region db queries
        public static bool Insert(Guid id, string bcode, string sku, string serNum, Guid iid, Guid ssid, Guid dtid, Guid icid) {
            try {
                var data = IS_ItemStockVM.set(id, bcode,sku,serNum, iid, ssid, dtid, icid);
                using (var context = new CentralProcessContext()){
                    context.IS_ItemStockDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid iid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.IS_ItemStockDB where i.ID == id && i.ItemID == iid select i).FirstOrDefault();
                    context.IS_ItemStockDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid iid, string bcode, Guid ssid, Guid dtid, string sku, string serNum, Guid icid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.IS_ItemStockDB where i.ID == id && i.ItemID == iid select i).FirstOrDefault();
                    query.BarcodeNumber = bcode;
                    query.StockStatusID = ssid;
                    query.DateTimeStorageID = dtid;
                    query.SKU = sku;
                    query.SerialNumber = serNum;
                    query.ItemColorID = icid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<IS_ItemStock> GetByIID(Guid iid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemStockDB where i.ItemID == iid select i).ToList();
                return query;
            }
        }
        public static IS_ItemStock GetByID(Guid id, Guid iid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemStockDB where i.ID == id && i.ItemID == iid select i).FirstOrDefault();
                return query;
            }
        }
        public static IS_ItemStock GetBySKUSerialNum(string sku, string ser) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemStockDB where i.SKU.Equals(sku) && i.SerialNumber.Equals(ser) select i).FirstOrDefault();
                return query;
            }
        }
        public static List<IS_ItemStock> GetByItemColorID(Guid icid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.IS_ItemStockDB where i.ItemColorID == icid select i).ToList();
                return query;
            }
        }


        #endregion
        #region functionalities
        public static IS_ItemStockVM SetSubData(IS_ItemStock data, Guid aid)
        {
            var model = IS_ItemStockVM.MToVM(data);
            model.DateTimeStorage = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            model.StockStatus = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.StockStatusID));
            return model;
        }
        public static List<IS_ItemStockVM> SetSubDatas(List<IS_ItemStock> list, Guid aid)
        {
            var nlist = new List<IS_ItemStockVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        #endregion
    }
}