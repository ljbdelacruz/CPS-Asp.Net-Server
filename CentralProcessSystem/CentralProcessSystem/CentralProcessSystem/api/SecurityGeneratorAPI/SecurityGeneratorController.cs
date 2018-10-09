using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.SecurityGeneratorS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.SecurityGeneratorAPI
{
    public class SecurityGeneratorController : Controller
    {
        #region SecurityGenerator
        #region request post

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SGCGenerateID() {
            try {
                return Success(Guid.NewGuid().ToString());
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> SGCGenerateCode1(){
            try{
                var code = SecurityGeneratorService.GetCode();
                return Success(code);
            } catch{ return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GetNCode() {
            try {
                var n = int.Parse(Request.Form["n"]);
                var code = SecurityGeneratorService.GetNCode(n);
                return Success(code);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion

        #endregion

        #region SecurityLinks
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SLInsert() {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var code = Request.Form["code"];
                //get url using asp.net
                var url = Request.Form["url"];
                var oid = Guid.Parse(Request.Form["oid"]);
                //check if emailAddress user.ID == oid
                if (SecurityLinksService.Insert(id, cid, url + code, oid))
                {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.ContactAdmin("updating this content"));

            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SLAuthUser() {
            try {
                var url = Request.Form["url"];
                var email = Request.Form["email"];
                var newPassword = Request.Form["npass"];
                var data = UsersService.GetByEmailAddress(email);
                var dataLink = SecurityLinksService.GetByURL(url);
                if (data!=null && dataLink!=null && dataLink.OwnerID == data.ID) {
                    if (UsersService.UpdatePassword(data.ID, UsersService.GenerateHashPassword(newPassword))) {
                        SecurityLinksService.Remove(dataLink.ID);
                        return Success("Success Updating Password");
                    }
                }
                return Failed(MessageUtilityService.ContactAdmin("content"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SLAuthIsAllowAccess() {
            try {
                var uemail = Request.Form["email"];
                var url = Request.Form["url"];
                var data = UsersService.GetByEmailAddress(uemail);
                var dataLink = SecurityLinksService.GetByURL(url);
                if (data != null && dataLink != null && dataLink.OwnerID == data.ID) {
                    //update access into true
                    SecurityLinksService.Remove(dataLink.ID);
                    UsersService.UpdateIsAllowAccess(data.ID, true);
                    return Success("Success Authorizing this account!");
                }
                return Failed(MessageUtilityService.ContactAdmin("content"));
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