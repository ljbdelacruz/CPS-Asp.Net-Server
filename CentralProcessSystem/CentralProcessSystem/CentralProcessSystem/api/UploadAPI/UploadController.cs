using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.UploadAPI
{
    public class UploadController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UUploadImage() {
            try
            {
                var path = Request.Form["path"];
                var fileName = Guid.NewGuid().ToString();
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //before uploading image make sure it does not exist in path
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        if (!Directory.Exists(Path.Combine(Server.MapPath("~/UPLOADS/" + path))))
                        {
                            Directory.CreateDirectory(Path.Combine(Server.MapPath("~/UPLOADS/" + path)));
                        }
                        var uploadPath = Path.Combine(Server.MapPath("~/UPLOADS/" + path), fileName + ".png");
                        using (var fileStream = System.IO.File.Create(uploadPath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                return Success("UPLOADS/" + path + "/" + fileName + ".png");
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UUploadBase64(){
            try{
                var company = Request.Form["company"];
                var source = Request.Form["image"];
                var fname = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/UPLOADS/" + company), fname.ToString() + ".png");
                var byteData = Convert.FromBase64String(source);
                System.IO.File.WriteAllBytes(path, byteData);
                return Success("/UPLOADS/" + company + "/" + fname.ToString() + ".png");
            }catch{ return Failed(MessageUtilityService.ServerError()); }
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