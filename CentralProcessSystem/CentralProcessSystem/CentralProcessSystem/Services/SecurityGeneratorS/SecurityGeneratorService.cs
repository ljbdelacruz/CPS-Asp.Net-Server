using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.SecurityGeneratorS
{
    public static class SecurityGeneratorService
    {

        public static string GetNCode(int limit) {
            var code = GetCode();
            return code.Substring(0, limit);
        }
        public static string GetCode() {
            var code = StringUtil.ReplaceString(Guid.NewGuid().ToString(), "-", "");
            return code;
        }

    }
}