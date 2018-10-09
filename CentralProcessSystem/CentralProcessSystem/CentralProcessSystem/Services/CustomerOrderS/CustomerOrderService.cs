using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.CustomerOrderS
{
    public static class CustomerOrderService
    {

        #region db queries
        public static bool Insert(Guid id, Guid uid, Guid oid, Guid aid, Guid dtid, bool iS, float tc) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = CustomerOrderVM.set(id, uid, oid, aid, dtid, iS, tc);
                    context.CustomerOrderDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid uid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()){
                    var query = (from i in context.CustomerOrderDB where i.ID == id && i.UserID == uid && i.API == aid select i).FirstOrDefault();
                    context.CustomerOrderDB.Remove(query);
                    context.SaveChanges();
                    //remove customerOrderItem
                    //remove date time
                    if (DateTimeStorageService.Remove(query.DateTimeID, aid, id) && CustomerOrderItemService.RemoveByCOID(id, aid)){
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }
        public static bool UpdateTC(Guid id, Guid oid, Guid uid, float tc) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.CustomerOrderDB where i.ID == id && i.OwnerID == oid && i.UserID == uid select i).FirstOrDefault();
                    query.TotalCost = tc;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false;}
        }

        public static List<CustomerOrder> GetByOwnerID(Guid oid, bool iS) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.CustomerOrderDB where i.OwnerID == oid && i.isSubmit == iS select i).ToList();
                return query;
            }
        }
        public static List<CustomerOrder> GetByOIDUID(Guid oid, Guid uid, bool iS) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.CustomerOrderDB where i.OwnerID == oid && i.UserID == uid && i.isSubmit == iS select i).ToList();
                return query;
            }
        }
        #endregion

        #region util
        public static CustomerOrderVM SetSubData(CustomerOrder model) {
            var data = CustomerOrderVM.MToVM(model);
            data.DateTime=DateTimeStorageVM.MToVM(DateTimeStorageService.GetByOID(model.ID, model.API).FirstOrDefault());
            data.OrderItem = CustomerOrderItemService.SetSubDatas(CustomerOrderItemService.GetByCOID(model.ID, model.API));
            return data;
        }
        public static List<CustomerOrderVM> SetSubDatas(List<CustomerOrder> models) {
            var list = new List<CustomerOrderVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model));
            }
            return list;
        }
        #endregion

    }
}