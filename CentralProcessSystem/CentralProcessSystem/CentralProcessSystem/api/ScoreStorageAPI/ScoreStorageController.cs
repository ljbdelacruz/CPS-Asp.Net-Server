using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.ScoreStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.ScoreStorageAPI
{
    public class ScoreStorageController : Controller
    {


        #region scoreStorage


        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SSInsert(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var name = Request.Form["name"];
                var score = int.Parse(Request.Form["score"]);
                var tz = Request.Form["tz"];
                var dtid = Guid.NewGuid();
                var cid = Guid.Parse(Request.Form["cid"]);
                if (DateTimeStorageService.InsertByTZ(dtid, id, aid, tz, cid)) {
                    if (ScoreStorageService.Insert(id, aid, name, score, dtid)) {
                        return Success(id.ToString());
                    }
                }
                return Failed(MessageUtilityService.ContactAdmin("content"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SSUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                return Success("");
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
        #endregion


    }
}