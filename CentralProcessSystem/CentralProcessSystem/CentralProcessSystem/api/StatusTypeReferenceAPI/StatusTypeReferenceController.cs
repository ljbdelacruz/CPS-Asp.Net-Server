using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.StatusReferenceS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.StatusTypeReferenceAPI
{
    public class StatusTypeReferenceController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> STInsert() {
            var id = Guid.Parse(Request.Form["id"]);
            var name = Request.Form["name"];
            var desc = Request.Form["desc"];
            var oid = Guid.Parse(Request.Form["oid"]);
            var aid = Guid.Parse(Request.Form["aid"]);
            var dtid = Guid.Parse(Request.Form["dtid"]);
            var catID = Guid.Parse(Request.Form["cid"]);
            var uid = Guid.Parse(Request.Form["uid"]);
            var sadmin = Guid.Parse("3c35cccc-d48d-4721-9283-d58faeac6cc1");
            try {
                if (!UserAccessLevelService.HasAccess(uid, sadmin)) {
                    return Failed(MessageUtilityService.ContactAdmin("Content"));
                }
                if (StatusTypesReferencesService.Insert(id, name, desc, oid, aid, dtid, catID)) {
                    return Success(id.ToString());
                }
                DateTimeStorageService.RemoveAdmin(dtid);
                return Failed(MessageUtilityService.FailedInsert(""));
            } catch {
                DateTimeStorageService.RemoveAdmin(dtid);
                return Failed(MessageUtilityService.ServerError());
            }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> STRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                if (StatusTypesReferencesService.Remove(id, aid, oid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove(""));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> STUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var name = Request.Form["name"];
                var desc = Request.Form["desc"];
                var category = Guid.Parse(Request.Form["cid"]);
                if (StatusTypesReferencesService.Update(id, aid, oid, name, desc, category)) {
                    return Success(id.ToString());
                }
                return Success(MessageUtilityService.FailedUpdate(""));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> STGetSubData() {
            try {
                var cid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["cid"]);
                var data = StatusTypesReferencesService.GetByOIDAID(oid, aid);
                var vms = StatusTypesReferencesService.SetSubDatas(data, cid);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> STGetByID(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = StatusTypesReferencesService.GetByIDAID(id, aid);
                return Success(StatusTypesReferencesVM.MToVM(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> STGet() {
            //user id to determine accessLevel
            var uid = Guid.Parse(Request.Form["id"]);
            var sadmin = Guid.Parse("3c35cccc-d48d-4721-9283-d58faeac6cc1");
            try {
                //check if uid has super admin access
                if (UserAccessLevelService.HasAccess(uid, sadmin)) {
                    var data = StatusTypesReferencesService.GetAll();
                    return Success(StatusTypesReferencesService.SetSubDatasAdmin(data));
                }
                return Failed(MessageUtilityService.ContactAdmin("Error"));
            } catch {
                return Failed(MessageUtilityService.ServerError());
            }
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