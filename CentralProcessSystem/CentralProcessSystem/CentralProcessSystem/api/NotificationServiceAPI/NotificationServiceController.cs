using CentralProcessSystem.Hubs;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.GroupingsDataS;
using CentralProcessSystem.Services.NotificationS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.NotificationServiceAPI
{
    public class NotificationServiceController : Controller
    {
        #region NotificationManager
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NMInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var title = Request.Form["title"];
                var message = Request.Form["message"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ir = Boolean.Parse(Request.Form["ir"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (NotificationManagerService.Insert(id, title, message, oid, aid, dtid, ir, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Notification"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NMRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (NotificationManagerService.Remove(id, oid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Notification"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NMUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var title = Request.Form["title"];
                var message = Request.Form["message"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ir = Boolean.Parse(Request.Form["ir"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (NotificationManagerService.Update(id, oid, aid, title, message, dtid, ir, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Notification"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> NMGetByOwner(string id, string aid) {
            try {
                var data = NotificationManagerService.GetByOIDAID(Guid.Parse(id), Guid.Parse(aid));
                var vms = NotificationManagerService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region signalR
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NMCheckUserNotification() {
            try {
                var uid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var gdCatID = Guid.Parse(Request.Form["gdcid"]);
                var notificationsReceipent = GroupingsDataService.GetBySIDCIDAID(uid, gdCatID, aid, false);
                foreach (var notif in notificationsReceipent){
                    var sid = SignalRDataService.GetByOIDAPI(notif.SourceID, aid);
                    NotificationManagerHub.NewNotification(notif.OwnerID.ToString(), sid.SignalRID.ToString());
                    //remove the receipent from database after it sents the notification to the receiver
                    GroupingsDataService.Remove(notif.ID, notif.OwnerID, notif.API);
                }
                return Success("");
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