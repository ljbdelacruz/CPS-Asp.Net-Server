using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.UploadS
{
    public static class UploadService
    {
        public static byte[] WriteBase64Image(string source, string path) {
            var byteData = Convert.FromBase64String(source);
            System.IO.File.WriteAllBytes(path, byteData);
            return byteData;
        }

    }
}