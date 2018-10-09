using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.GroupingsDataS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.MyCompanyInformationS
{
    public static class AdInformationService
    {
        #region db query

        public static bool Insert(Guid id, string ai, string url, Guid cid, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = AdsInformationVM.set(id, ai, url, cid, dtid);
                    context.AdsInformationDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.AdsInformationDB where i.ID == id select i).FirstOrDefault();
                    context.AdsInformationDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string ai, string url, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.AdsInformationDB where i.ID == id select i).FirstOrDefault();
                    query.AdInformation = ai;
                    query.URL = url;
                    query.DateTimeID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static AdsInformation GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.AdsInformationDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<AdsInformation> GetByAdCategoryID(Guid acid, Guid cid) {
            using (var context = new CentralProcessContext()) {
                var gds = GroupingsDataService.GetBySIDCID(acid, cid);
                var list = new List<AdsInformation>();
                foreach (var gd in gds){
                    list.Add(GetByID(gd.OwnerID));
                }
                return list;
            }
        }
        #endregion
        #region util
        public static AdsInformationVM SetSubData(AdsInformation model, Guid cid, Guid aid) {
            var data = AdsInformationVM.MToVM(model);
            data.Images = ImageLinkStorageVM.MsToVMs(ImageLinkStorageService.GetByOIDCID(model.ID, aid, cid));
            return data;
        }

        #endregion

    }
}