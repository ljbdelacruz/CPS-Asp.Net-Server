using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.ReportService
{
    public static class ReportClaimsService
    {
        #region db queries
        public static bool Insert(Guid id, Guid uid, Guid suid, Guid rtid, string reason, Guid aid, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()){
                    var data = ReportClaimsVM.set(id, uid, suid, rtid, reason, aid, dtid);
                    context.ReportClaimsDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ReportClaimsDB where i.ID == id && i.ApplicationID == aid select i).FirstOrDefault();
                    context.ReportClaimsDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid uid, Guid suid, Guid rtid, string reason, Guid dtid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ReportClaimsDB where i.ID == id && i.ApplicationID == aid select i).FirstOrDefault();
                    query.UserID = uid;
                    query.SenderUserID = suid;
                    query.ReportTypeID = rtid;
                    query.Reason = reason;
                    query.DateTimeStorageID = dtid;
                    context.SaveChanges();
                    return true;                    
                }
            } catch { return false; }
        }
        public static List<ReportClaims> GetByAID(Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ReportClaimsDB where i.ApplicationID == aid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static ReportClaimsVM SetSubData(ReportClaims data, Guid aid)
        {
            var model = ReportClaimsVM.MToVM(data);
            model.DateTime = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            model.ReportType = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.ReportTypeID));
            return model;
        }
        public static List<ReportClaimsVM> SetSubDatas(List<ReportClaims> list, Guid aid)
        {
            var nlist = new List<ReportClaimsVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }

        #endregion
    }
}