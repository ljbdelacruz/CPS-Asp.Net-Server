using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.ImageLinkStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.ImageLinkStorageAPI
{
    public class ImageLinkStorageController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ILSInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var source = Request.Form["src"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var order = int.Parse(Request.Form["o"]);
                if (ImageLinkStorageService.Insert(id, oid, source, aid, dtid, false, cid, order)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Image"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ILSRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (ImageLinkStorageService.Remove(id, oid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Image"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ILSRemoveID() {
            try {
                var oid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (ImageLinkStorageService.RemoveByOID(oid, aid)) {
                    return Success("");
                }
                return Failed(MessageUtilityService.FailedRemove("Image"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ILSUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var source = Request.Form["source"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (ImageLinkStorageService.Update(id, oid, aid, source, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Image"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> ILSGet(string id, string oid, string aid) {
            try {
                var data = ImageLinkStorageService.GetByID(Guid.Parse(id), Guid.Parse(oid), Guid.Parse(aid));
                return Success(ImageLinkStorageVM.MToVM(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> ILSGetOwner(string id, string aid) {
            try {
                var data = ImageLinkStorageService.GetByOIDAPI(Guid.Parse(id), Guid.Parse(aid));
                return Success(ImageLinkStorageVM.MsToVMs(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
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