using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.InventorySystemS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.CustomerOrderS
{
    public static class CustomerOrderItemService
    {
        #region db query
        public static bool Insert(Guid id, Guid iid, Guid icid, float sc, int q, Guid coid, Guid aid, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = CustomerOrderItemVM.set(id, iid, icid, sc, q, coid, aid, dtid);
                    context.CustomerOrderItemDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid coid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.CustomerOrderItemDB where i.ID == id && i.CustomerOrderID == coid && i.API == aid select i).FirstOrDefault();
                    context.CustomerOrderItemDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool RemoveByCOID(Guid coid, Guid aid) {
            try {
                var data = GetByCOID(coid, aid);
                foreach (var model in data) {
                    Remove(model.ID, coid, aid);
                }
                return true;
            } catch { return false; }
        }

        public static List<CustomerOrderItem> GetByCOID(Guid id, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.CustomerOrderItemDB where i.CustomerOrderID == id && i.API == aid select i).ToList();
                return query;
            }
        }
        #endregion

        #region util
        public static CustomerOrderItemVM SetSubData(CustomerOrderItem model) {
            var data = CustomerOrderItemVM.MToVM(model);
            data.DateTime = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByOID(model.ID, model.API).FirstOrDefault());
            data.Item = IS_ItemService.SetSubData(IS_ItemService.GetByID(model.ItemID), model.API);
            data.ItemColor = IS_ItemColorService.SetSubData(IS_ItemColorService.GetByID(model.ItemColorID));
            return data;
        }
        public static List<CustomerOrderItemVM> SetSubDatas(List<CustomerOrderItem> models) {
            var list = new List<CustomerOrderItemVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model));
            }
            return list;
        }
        #endregion
    }
}