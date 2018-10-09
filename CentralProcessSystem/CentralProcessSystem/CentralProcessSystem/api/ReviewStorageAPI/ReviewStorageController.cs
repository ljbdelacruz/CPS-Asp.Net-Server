using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.ReviewS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.ReviewStorageAPI
{
    public class ReviewStorageController : Controller
    {
        #region ReviewStorage
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RSInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var rid = Guid.Parse(Request.Form["rid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var title = Request.Form["title"];
                var comment = Request.Form["comment"];
                var stars = int.Parse(Request.Form["s"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (ReviewStorageService.Insert(id, sid, rid, aid, title, comment, stars, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Review"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RSRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (ReviewStorageService.Remove(id, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Review"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> RSUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var title = Request.Form["title"];
                var comment = Request.Form["comment"];
                var stars = int.Parse(Request.Form["s"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (ReviewStorageService.Update(id, aid, title, comment, stars, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Review"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> RSGetBySender(string id, string aid) {
            try {
                var data = ReviewStorageService.GetByAIDSID(Guid.Parse(aid), Guid.Parse(id));
                var vms = ReviewStorageService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> RSGetByReviewed(string id, string aid) {
            try
            {
                var data = ReviewStorageService.GetByAIDRID(Guid.Parse(aid), Guid.Parse(id));
                var vms = ReviewStorageService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> RSGet(string id)
        {
            try
            {
                var data = ReviewStorageService.GetByAID(Guid.Parse(id));
                var vms = ReviewStorageService.SetSubDatas(data, Guid.Parse(id));
                return Success(vms);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
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