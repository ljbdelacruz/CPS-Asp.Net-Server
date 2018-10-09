using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.LocationStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.LocationStorageAPI
{
    public class LocationStorageController : Controller
    {
        #region location storage
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> LSInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var lcid = Guid.Parse(Request.Form["lcid"]);
                var longi = float.Parse(Request.Form["longi"]);
                var lat = float.Parse(Request.Form["lat"]);
                var desc = Request.Form["desc"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (LocationStorageService.Insert(id, oid, lcid, longi, lat, desc, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Location"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> LSRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                if (LocationStorageService.Remove(id, oid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Location"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> LSUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var longi = float.Parse(Request.Form["longi"]);
                var lat = float.Parse(Request.Form["lat"]);
                var desc = Request.Form["desc"];
                if (LocationStorageService.Update(id, oid, longi, lat, desc)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Location"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> LSGetByID(string id, string oid, string lcid, string aid) {
            try {
                var data = LocationStorageService.GetByIDOID(Guid.Parse(id), Guid.Parse(oid), Guid.Parse(lcid));
                var vm = LocationStorageService.SetSubData(data, Guid.Parse(aid));
                return Success(vm);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> LSGetByOwner(string id, string lcid, string aid) {
            try {
                var data = LocationStorageService.GetByOID(Guid.Parse(id), Guid.Parse(lcid));
                var vms = LocationStorageService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> LSGetByCategory(string id, string aid) {
            try {
                var data = LocationStorageService.GetByLCID(Guid.Parse(id));
                var vms = LocationStorageService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
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