using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.DateTimeAPI
{
    public class DateTimeStorageController : Controller
    {
        #region datetimestorage
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSGetTZ() {
            try {
                var tz = Request.Form["tz"];
                var time = DateTimeUtil.GetTimeNowByUTC(tz);
                return Success(DateTimeUtil.DateTimeToString(time));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //insert new datetime by timeZone
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSInsertTZ(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var tz = Request.Form["tz"];
                var cid = Guid.Parse(Request.Form["cid"]);
                var time = DateTimeUtil.GetTimeNowByUTC(tz);
                if (DateTimeStorageService.Insert(id, oid, aid, time, time, cid)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("DateTime"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var ca = DateTime.Parse(Request.Form["ca"]);
                var ua = DateTime.Parse(Request.Form["ua"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                if (DateTimeStorageService.Insert(id, oid, aid, ca, ua, cid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Date Time"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                if (DateTimeStorageService.Remove(id, aid, oid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("DateTime"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var ca = DateTime.Parse(Request.Form["ca"]);
                var ua = DateTime.Parse(Request.Form["ua"]);
                if (DateTimeStorageService.Update(id, oid, ua)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("DateTime"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> DSGetTimeByTZ() {
            try {
                var tz = Request.Form["tz"];
                var dtime=DateTimeUtil.GetTimeNowByUTC(tz);
                return Success(dtime);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> DSGetByOwner(string id, string aid) {
            try {
                var data = DateTimeStorageService.GetByOID(Guid.Parse(id), Guid.Parse(aid));
                return Success(DateTimeStorageVM.MsToVMs(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> DSGet(string id, string aid) {
            try {
                var data = DateTimeStorageService.GetByID(Guid.Parse(id));
                return Success(DateTimeStorageVM.MToVM(data));
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