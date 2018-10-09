using CentralProcessSystem.Hubs;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.GroupingsDataS;
using CentralProcessSystem.Services.MessagingAppS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.MessagingServicesAPI
{
    public class MessagingServicesController : Controller
    {
        #region myVars
        private Guid MCID = Guid.Parse("");
        
        
        #endregion

        #region MessagingRoom
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MRInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (MessagingRoomService.Insert(id, name, oid, aid, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Room"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MRRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (MessagingRoomService.Remove(id, oid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Room"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MRUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var name = Request.Form["name"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (MessagingRoomService.Update(id, oid, aid, name, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Room"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        //get by room participants
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MRGetRoom(){
            try {
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                //category can be userToUser, storeToUser
                var data = GroupingsDataService.GetByOIDCIDAID(oid, cid, aid, false);
                var vms = GroupingsDataService.SetSubDatas(data, aid, 1);
                return Success(vms);

            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #endregion
        #region MessagingConversation
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MCInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var text = Request.Form["text"];
                var mtid = Guid.Parse(Request.Form["mtid"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var rid = Guid.Parse(Request.Form["rid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (MessagingConversationService.Insert(id, text, mtid, sid, rid, dtid, ia)) {
                    if (MCRInsert(rid, id, this.MCID)) {
                        return Success(id.ToString());
                    }
                    return Failed(MessageUtilityService.FailedInsert("Not sent to the members please try again"));
                }
                return Failed(MessageUtilityService.FailedInsert("Conversation"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MCRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var rid = Guid.Parse(Request.Form["rid"]);
                if (MessagingConversationService.Remove(id, rid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Conversation"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MCUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var text = Request.Form["text"];
                var rid = Guid.Parse(Request.Form["rid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                if (MessagingConversationService.Update(id, text, rid, dtid, ia)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Conversation"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MSGetByRoom() {
            try {
                var rid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var ia = Boolean.Parse(Request.Form["ia"]);
                var id = Boolean.Parse(Request.Form["isDesc"]);

                var data = MessagingConversationService.GetByRID(rid, ia);
                var vms = MessagingConversationService.SetSubDatas(data);
                return Success(MessagingConversationService.OrderByDateTime(vms, id));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }



        #endregion
        #endregion
        #region messagingConversationReceipent

        #region get
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> MCRGetUnread() {
            try {
                var rid = Guid.Parse(Request.Form["rid"]);
                var rmid = Guid.Parse(Request.Form["rmid"]);
                var data = MessagingConversationReceipentService.GetByReceiverIDRoomID(rid, rmid, false);
                return Success(MessagingConversationReceipentService.SetSubDatas(data, 0));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion

        private static bool MCRInsert(Guid rid, Guid mcid, Guid cid) {
            //every time you send insert MC please invoke this after that item has been insert
            var members=GroupingsDataService.GetBySIDCID(rid, cid);
            foreach (var member in members) {
                if (!MessagingConversationReceipentService.Insert(Guid.NewGuid(), rid, mcid, false, member.OwnerID)) {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region signalRInvoke
        [AllowCrossSiteJson]
        [HttpPost]
        //receiverID, MessagingConversationID
        //receiver=userID find its signalRConnection
        //mcid=messageConversationID
        public async Task<JsonResult> NewMessageSent()
        {
            try{
                var rmid = Guid.Parse(Request.Form["rmid"]);
                var mcid = Guid.Parse(Request.Form["mcid"]);
                var rid = Guid.Parse(Request.Form["rid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var sid = SignalRDataService.GetByOIDAPI(rid, aid);
                MessagingAppHub.NewMessage(rmid.ToString(), mcid.ToString(), aid.ToString(), sid.SignalRID.ToString());
                return Success(sid.SignalRID.ToString());
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
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