using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.GroupingsDataS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.GroupingsDataAPI
{
    public class GroupingsDataController : Controller
    {
        #region groupings Data
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GDInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var order = int.Parse(Request.Form["order"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia=Boolean.Parse(Request.Form["ia"]);
                var catID = Guid.Parse(Request.Form["cid"]);
                if (GroupingsDataService.Insert(id, sid, oid, aid, order, dtid, ia, catID)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert(""));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GDRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (GroupingsDataService.Remove(id, oid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove(""));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GDUpdate(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var order = int.Parse(Request.Form["order"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (GroupingsDataService.Update(id, oid, aid, sid, order, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate(""));
            } catch{return Failed(MessageUtilityService.ServerError());}
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GDGetByOwner(string id, string aid) {
            try {
                var data = GroupingsDataService.GetByOIDAID(Guid.Parse(id), Guid.Parse(aid), false);
                var vms = GroupingsDataService.SetSubDatas(data, Guid.Parse(aid), 0);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GDGetByCategory(string id, string cid, string aid) {
            try {
                var data = GroupingsDataService.GetByOIDCIDAID(Guid.Parse(id), Guid.Parse(cid), Guid.Parse(aid), false);
                var vms = GroupingsDataService.SetSubDatas(data, Guid.Parse(aid), 0);
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