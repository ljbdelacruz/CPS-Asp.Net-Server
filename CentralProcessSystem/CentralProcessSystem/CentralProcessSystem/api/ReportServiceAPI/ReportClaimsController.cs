using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.ReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.ReportServiceAPI
{
    public class ReportClaimsController : Controller
    {
        #region ReportClaims
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RCInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var suid = Guid.Parse(Request.Form["suid"]);
                var rtid = Guid.Parse(Request.Form["rtid"]);
                var reason = Request.Form["reason"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (ReportClaimsService.Insert(id, uid, suid, rtid, reason, aid, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert(" Report"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RCRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (ReportClaimsService.Remove(id, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Report"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RCUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var suid = Guid.Parse(Request.Form["suid"]);
                var rtid = Guid.Parse(Request.Form["rtid"]);
                var reason = Request.Form["reason"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (ReportClaimsService.Update(id, uid, suid, rtid, reason, dtid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Report"));

            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region Get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RCGet(string id) {
            try{
                var data = ReportClaimsService.GetByAID(Guid.Parse(id));
                var vms = ReportClaimsService.SetSubDatas(data, Guid.Parse(id));
                return Success(vms);
            }catch {return Failed(MessageUtilityService.ServerError());}
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