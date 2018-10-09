using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.ShopManagementS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.ShopManagementAPI
{
    public class ShopManagementController : Controller
    {
        #region ShopManagement
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SMInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var desc = Request.Form["desc"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var pi = Guid.Parse(Request.Form["pi"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                if (ShopManagementService.Insert(id, name, desc, oid, aid, pi, cid))
                {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Shop"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SMUpdatePI(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var pi = Guid.Parse(Request.Form["pi"]);
                if (ShopManagementService.UpdatePI(id, oid, aid, pi)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.ServerError());
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SMGetStores(){
            try {
                var oid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = ShopManagementService.GetByOID(oid, aid);
                var vms = ShopManagementService.SetSubDatas(data, false);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SMGetByID() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = ShopManagementService.GetByID(id, oid, aid);
                var vm = ShopManagementService.SetSubData(data, true);
                return Success(vm);
            } catch { return Failed(MessageUtilityService.ServerError()); }
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