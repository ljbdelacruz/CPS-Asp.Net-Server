using CentralProcessSystem.Services.DescriptionStorageS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.DescriptionSAPI
{
    public class DescriptionStorageController : Controller
    {
        #region DescriptionStorage
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var title = Request.Form["title"];
                var desc = Request.Form["desc"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var order = int.Parse(Request.Form["o"]);
                if (DescriptionStorageService.Insert(id, title, desc, oid, aid, cid, order)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Description"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSGet() {
            try {
                var id = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var data = DescriptionStorageService.GetByOIDCID(id, aid, cid);
                var vms = DescriptionStorageService.SetSubDatas(data);
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