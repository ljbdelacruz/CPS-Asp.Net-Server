using CentralProcessSystem.Services.CustomerOrderS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.CustomerOrderAPI
{
    public class CustomerOrderController : Controller
    {

        #region CustomerOrder
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> COnsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var iS = Boolean.Parse(Request.Form["iS"]);
                var tc = float.Parse(Request.Form["tc"]);
                if (CustomerOrderService.Insert(id, uid, oid, aid, dtid, iS, tc)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Customer Order"));

            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> COGet(){
            try {
                var oid = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var iS = Boolean.Parse(Request.Form["iS"]);
                var data = CustomerOrderService.GetByOIDUID(oid, uid, iS);
                var vms = CustomerOrderService.SetSubDatas(data);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #endregion

        #region CustomerOrderItems
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> COIInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var iid = Guid.Parse(Request.Form["iid"]);
                var icid = Guid.Parse(Request.Form["icid"]);
                var sc = float.Parse(Request.Form["sc"]);
                var q = int.Parse(Request.Form["q"]);
                var coid = Guid.Parse(Request.Form["coid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (CustomerOrderItemService.Insert(id, iid, icid, sc, q, coid, aid, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Customer Items"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> COIRemove(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var coid = Guid.Parse(Request.Form["coid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (CustomerOrderItemService.Remove(id, coid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Customer Item"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> COIGet(){
            try{
                var coid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = CustomerOrderItemService.GetByCOID(coid, aid);
                var vms = CustomerOrderItemService.SetSubDatas(data);
                return Success(vms);
            }catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #endregion

        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}