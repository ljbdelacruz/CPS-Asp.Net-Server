using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.Services.General
{
    #region Util
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }
    public static class DateTimeUtil {
        public static string DateTimeToString(DateTime date)
        {
            return date.Month + "/" + date.Day + "/" + date.Year;
        }
        public static DateTime GetTimeNowByUTC(string tz) {
            //ex. 
            //this will determine which time to use based on the timezone preference
            var timeZoneTime = TimeZoneInfo.FindSystemTimeZoneById(tz);
            //get the time of that timezone
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneTime);
        }
    }
    public static class StringUtil{
        public static string ReplaceString(string data, string strReplace, string replacement)
        {
            data = Regex.Replace(data, strReplace, replacement);
            return data;
        }

    }
    



    #endregion



}