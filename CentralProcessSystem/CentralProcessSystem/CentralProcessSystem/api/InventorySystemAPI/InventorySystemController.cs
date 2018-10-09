using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.InventorySystemS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.InventorySystemAPI
{
    public class InventorySystemController : Controller
    {
        #region item
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var title = Request.Form["title"];
                var desc = Request.Form["desc"];
                var price = float.Parse(Request.Form["price"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var icid = Guid.Parse(Request.Form["icid"]);
                var ic = Boolean.Parse(Request.Form["ic"]);
                var quantity = int.Parse(Request.Form["q"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var condition = Guid.Parse(Request.Form["cid"]);
                if (IS_ItemService.Insert(id, title, desc, price, aid, oid, icid, ic, quantity, dtid, condition)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Item"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                if (IS_ItemService.Remove(id, aid, oid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Item"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var title = Request.Form["title"];
                var desc = Request.Form["desc"];
                var price = float.Parse(Request.Form["price"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var icid = Guid.Parse(Request.Form["icid"]);
                var ic = Boolean.Parse(Request.Form["ic"]);
                var quantity = int.Parse(Request.Form["q"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (IS_ItemService.Update(id, oid, aid, icid, title, desc, price, ic, quantity)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Item"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIGetByOwner()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var icid = Guid.Parse(Request.Form["icid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = IS_ItemService.GetByOID(id, aid);
                var vms = IS_ItemService.SetSubDatas(data, aid);
                return Success(vms);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIGetByOwnerC()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var icid = Guid.Parse(Request.Form["icid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = IS_ItemService.GetByOIDICID(id, icid, aid);
                var vms = IS_ItemService.SetSubDatas(data, aid);
                return Success(vms);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIGetByCategory() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = IS_ItemService.GetByICID(id, aid);
                var vms = IS_ItemService.SetSubDatas(data, aid);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIGetContains(){
            try{
                var input = Request.Form["input"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var lmt = int.Parse(Request.Form["lmt"]);
                var data = IS_ItemService.GetByContains(input, aid, lmt);
                var vms = IS_ItemService.SetSubDatas(data, aid);
                return Success(vms);
            }catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #endregion

        #region itemStock
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ISInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var bcode = Request.Form["bcode"];
                var iid = Guid.Parse(Request.Form["iid"]);
                var ssid = Guid.Parse(Request.Form["ssid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var sku = Request.Form["sku"];
                var serNum = Request.Form["serNum"];
                var icid = Guid.Parse(Request.Form["icid"]);
                if (IS_ItemStockService.Insert(id, bcode, sku, serNum, iid, ssid, dtid, icid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Stock"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ISRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var iid = Guid.Parse(Request.Form["iid"]);
                if (IS_ItemStockService.Remove(id, iid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Stock"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ISUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var bcode = Request.Form["bcode"];
                var iid = Guid.Parse(Request.Form["iid"]);
                var ssid = Guid.Parse(Request.Form["ssid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var sku = Request.Form["sku"];
                var serNum = Request.Form["serNum"];
                var icid = Guid.Parse(Request.Form["icid"]);

                if (IS_ItemStockService.Update(id, iid, bcode, ssid, dtid, sku, serNum, icid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Stock"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ISGetByItem()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = IS_ItemStockService.GetByIID(id);
                var vms = IS_ItemStockService.SetSubDatas(data, aid);
                return Success(vms);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ISGetByID()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var iid = Guid.Parse(Request.Form["iid"]);
                var data = IS_ItemStockService.GetByID(id, iid);
                var vm = IS_ItemStockService.SetSubData(data, aid);
                return Success(vm);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #region get by

        #endregion
        #endregion

        #region itemColor
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ICInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var color = Request.Form["color"];
                var desc = Request.Form["desc"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                if (IS_ItemColorService.Insert(id, color, desc, oid, aid, cid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("ItemColor"));
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