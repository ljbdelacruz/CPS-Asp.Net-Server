using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.SystemLogsS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.SystemLogAPI
{
    public class SystemLogController : Controller
    {
        #region SystemLogs
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SLInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var desc = Request.Form["desc"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (SystemLogsService.Insert(id, desc, oid, aid, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Logs"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SLRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (SystemLogsService.Remove(id, oid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Logs"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SLUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var desc = Request.Form["desc"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (SystemLogsService.Update(id, oid, aid, desc, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Logs"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SLGetByOwner(string id, string aid) {
            try {
                var data = SystemLogsService.GetByOIDAPI(Guid.Parse(id), Guid.Parse(aid));
                return Success(SystemLogsVM.MsToVMs(data));
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