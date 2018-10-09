using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.StoreManagementS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.StoreManagementAPI
{
    public class StoreManagementController : Controller
    {
        #region MyStore
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MSInsert() {
            var id = Guid.Parse(Request.Form["id"]);
            var uid = Guid.Parse(Request.Form["uid"]);
            var name = Request.Form["name"];
            var aid = Guid.Parse(Request.Form["aid"]);
            var scid = Guid.Parse(Request.Form["scid"]);
            var sbid = Guid.Parse(Request.Form["sbid"]);
            var slid = Guid.Parse(Request.Form["slid"]);
            var dtid = Guid.Parse(Request.Form["dtid"]);
            try {
                if (MyStoreService.IsStoreNameAllowUse(name)) {
                    //remove dtid
                    DateTimeStorageService.RemoveAdmin(dtid);
                    return Failed(MessageUtilityService.InUse("Store Name"));
                }
                if (MyStoreService.Insert(id, uid, name, aid, scid, sbid, slid, false, dtid))
                {
                    return Success(id.ToString());
                }
                //removing datetimeID
                DateTimeStorageService.RemoveAdmin(dtid);
                return Failed(MessageUtilityService.FailedInsert("MyStore"));
            } catch {
                DateTimeStorageService.RemoveAdmin(dtid);
                return Failed(MessageUtilityService.ServerError());
            }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MSRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (MyStoreService.Remove(id, uid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Store"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MSUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var name = Request.Form["name"];
                var scid = Guid.Parse(Request.Form["scid"]);
                var sbid = Guid.Parse(Request.Form["sbid"]);
                var slid = Guid.Parse(Request.Form["slid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (MyStoreService.Update(id, uid, aid, name, scid, sbid, slid, ia, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Store"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> MSGetByUser(string id, string aid, bool ia) {
            try {
                var data = MyStoreService.GetByUIDAPI(Guid.Parse(id), Guid.Parse(aid), ia);
                var vms = MyStoreService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        public async Task<JsonResult> MSGetByCategory(string id, string aid, bool ia) {
            try {
                var data = MyStoreService.GetBySCIDAID(Guid.Parse(id), Guid.Parse(aid), ia);
                var vms = MyStoreService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #endregion
        #region StoreBranch
        #region Request Post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SBInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var gid = Guid.Parse(Request.Form["gid"]);
                var address = Request.Form["address"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (StoreBranchService.Insert(id, sid, aid, gid, address, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.ServerError());
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SBRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                if (StoreBranchService.Remove(id, sid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Store Branch"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SBUpdate()
        {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var gid = Guid.Parse(Request.Form["gid"]);
                var address = Request.Form["add"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (StoreBranchService.Update(id, sid, aid, gid, address, dtid)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.ServerError());
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SBGet(string id, string aid, string lcid) {
            try {
                var data = StoreBranchService.GetBySID(Guid.Parse(id));
                var vms = StoreBranchService.SetSubDatas(data, Guid.Parse(aid), Guid.Parse(lcid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SBGetByRadius(string id, string aid, string lcid, string rad, string longi, string lat) {
            try {
                var data = StoreBranchService.GetNearestBranchByLocation(Guid.Parse(id), Guid.Parse(aid), Guid.Parse(lcid), float.Parse(rad), float.Parse(longi), float.Parse(lat));
                return Success(data);
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