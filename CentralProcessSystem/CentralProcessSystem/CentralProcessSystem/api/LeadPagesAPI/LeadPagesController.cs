using CentralProcessSystem.Services.ContentManagementS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.LeadPagesAPI
{
    public class LeadPagesController : Controller
    {
        #region LeadPages
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> LPInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var title = Request.Form["title"];
                var desc = Request.Form["desc"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var bgid = Guid.Parse(Request.Form["bgid"]);
                var mid = Guid.Parse(Request.Form["mid"]);
                var tdid = Guid.Parse(Request.Form["tdid"]);
                if (LeadPageService.Insert(id, title, desc, oid, bgid, mid, tdid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Lead Pages"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion

        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> LPGetByID() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var data = LeadPageService.GetByID(id, oid);
                var api = Guid.Parse(Request.Form["aid"]);
                var vms = LeadPageService.SetSubdata(data, api);
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