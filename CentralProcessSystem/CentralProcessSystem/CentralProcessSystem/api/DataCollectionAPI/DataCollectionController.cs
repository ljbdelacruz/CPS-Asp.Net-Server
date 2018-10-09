using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DataCollectionS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.DataCollectionAPI
{
    public class DataCollectionController : Controller
    {
        #region EmailList
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ELInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var email = Request.Form["email"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var cnum = Request.Form["cnum"];
                if (EmailListService.Insert(id, name, email, dtid, cnum)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Email"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ELRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (EmailListService.Remove(id)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Email"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ELUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var email = Request.Form["email"];
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var cnum = Request.Form["cnum"];
                if (EmailListService.Update(id, name, email, dtid, cnum)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Email"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> ELIsEmailExist() {
            try {
                //checks if email exist in the database else create one
                var name = Request.Form["name"];
                var email = Request.Form["email"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var cnum = Request.Form["cnum"];
                var data = EmailListService.GetByEAdd(email);
                var tz = Request.Form["tz"];
                if (data != null)
                {
                    if (InsertNewEmailList(Guid.NewGuid(), name, email, aid, cnum))
                    {

                        return Success(true);
                    }
                    return Failed(MessageUtilityService.ServerError());
                }
                else {
                    //update time updated data
                    DateTimeStorageService.Update(data.DateTimeID, data.ID, DateTimeStorageService.GetByTZ(tz));
                    EmailListService.Update(data.ID, name, data.Email, data.DateTimeID, cnum);
                }
                //email is already registered in the database
                return Success(false);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        private bool InsertNewEmailList(Guid id, string name, string email, Guid aid, string cnum) {
            try {
                var dtid = Guid.NewGuid();
                var time = DateTime.Now;
                DateTimeStorageService.Insert(dtid, id, aid, time, time, Guid.Parse("931b5cca-e652-49cf-bc6c-ed96cf053604"));
                EmailListService.Insert(id, name, email, dtid, cnum);
                return true;
            } catch { return false; }
        }

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> ELGet(string id) {
            try {
                var data = EmailListService.GetByID(Guid.Parse(Request.Form["id"]));
                return Success(EmailListVM.MToVM(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> ELGetAll() {
            try {
                var data = EmailListService.GetAll();
                return Success(EmailListVM.MsToVMs(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #endregion
        #region SearchInput
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SIInsert(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sinput = Request.Form["input"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                if (SearchInputService.Insert(id, sinput, oid, dtid, api)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Input"));
            }catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SIRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (SearchInputService.Remove(id, oid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Input"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SIUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var sinput = Request.Form["input"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                if (SearchInputService.Update(id, sinput, oid, dtid, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Input"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SIGetOwner(string id, string aid) {
            try {
                var data = SearchInputService.GetByOIDAPI(Guid.Parse(id), Guid.Parse(aid));
                return Success(SearchInputsDataVM.MsToVMs(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SIGet(string id) {
            try {
                var data = SearchInputService.GetByOID(Guid.Parse(id));
                return Success(SearchInputsDataVM.MsToVMs(data));
;            } catch { return Failed(MessageUtilityService.ServerError()); }
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