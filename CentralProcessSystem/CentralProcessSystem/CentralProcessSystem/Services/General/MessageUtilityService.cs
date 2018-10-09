using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.General
{
    public static class MessageUtilityService
    {
        public static string ServerError()
        {
            return "Server error occured please try again later";
        }
        #region failed insert data template
        public static string FailedInsert(string addon) {
            return "Error Inserting Data " + addon;
        }
        public static string FailedRemove(string addon)
        {
            return "Error Removing Data " + addon;
        }
        public static string FailedUpdate(string addon)
        {
            return "Error Updating Data " + addon;
        }
        #endregion

        public static string AlreadyInRecord(string addon) {
            return "This data Already in the record please contact the "+addon;
        }
        #region template
        public static string InUse(string addons) {
            return addons + " Already in use please use another";
        }
        #endregion
        public static string AuthenticationFailed() {
            return "Authentication failed please use correct email and password";
        }
        public static string ContactAdmin(string add) {
            return "There seems to be a problem accessing this material please contact the administrator of this "+add;
        }

    }
}