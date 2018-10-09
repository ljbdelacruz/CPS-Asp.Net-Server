using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.MyCompanyInformationS
{
    public static class ApplicationInformationService
    {
        #region db queries
        public static bool Insert(Guid id, string name, string desc) {
            try {
                var data = ApplicationInformationVM.set(id, name, desc);
                using (var context = new CentralProcessContext()) {
                    context.ApplicationInformationDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static ApplicationInformation GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ApplicationInformationDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
        #region util
        public static bool IsApplicationExist(Guid aid) {
            try {
                if (GetByID(aid) != null) {
                    //application exist
                    return true;
                }
                return false;
            } catch { return false; }
        }
        #endregion


    }
}