using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.ContentManagementS
{
    public static class LeadPageService
    {
        #region db query
        public static bool Insert(Guid id, string title, string desc, Guid oid, Guid bgid, Guid mid, Guid tdid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = LeadPagesVM.set(id, title, desc, oid, bgid, mid, tdid);
                    context.LeadPagesDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.LeadPagesDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                    context.LeadPagesDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string title, string desc, Guid oid, Guid bgid, Guid mid){
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.LeadPagesDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                    query.Title = title;
                    query.Description = desc;
                    query.BackgroundImageID = bgid;
                    query.MainImageID = mid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static LeadPages GetByID(Guid id, Guid oid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.LeadPagesDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                return query;
            }
        }
        public static List<LeadPages> GetByOID(Guid oid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.LeadPagesDB where i.OwnerID == oid select i).ToList();
                return query;
            }
        }

        #endregion

        #region util

        public static LeadPagesVM SetSubdata(LeadPages model, Guid api) {
            var data = LeadPagesVM.MToVM(model);
            data.BackgroundImage = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByID(model.BackgroundImageID, model.ID, api));
            data.MainImage = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByID(model.MainImageID, model.ID, api));
            data.TemplateDesign = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(model.TemplateDesignID));
            return data;
        }
        public static List<LeadPagesVM> SetSubDatas(List<LeadPages> models, Guid api) {
            var list = new List<LeadPagesVM>();
            foreach (var model in models) {
                list.Add(SetSubdata(model, api));
            }
            return list;
        }

        #endregion
    }
}