using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.StoreManagementS
{
    public static class MyStoreService
    {
        #region db queries
        public static bool Insert(Guid id, Guid uid, string name, Guid api, Guid scid, Guid sbid, Guid slid, bool ia, Guid dtid) {
            try {
                var data = MyStoreVM.set(id, uid, name, api, scid, sbid, slid, ia, dtid);
                using (var context = new CentralProcessContext()) {
                    context.MyStoreDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid uid, Guid api) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MyStoreDB where i.ID == id && i.UserID == uid && i.ApplicationID == api select i).FirstOrDefault();
                    context.MyStoreDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid uid, Guid api, string name, Guid scid, Guid sbid, Guid slid, bool ia, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MyStoreDB where i.ID == id && i.UserID == uid && i.ApplicationID == api select i).FirstOrDefault();
                    query.Name = name;
                    query.StoreCategoryID = scid;
                    query.StoreBackgroundImageID = sbid;
                    query.StoreLogoID = slid;
                    query.isApproved = ia;
                    query.DateTimeStorageID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<MyStore> GetByUIDAPI(Guid uid, Guid api, bool ia) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MyStoreDB where i.UserID == uid && i.ApplicationID == api && i.isApproved == ia select i).ToList();
                return query;
            }
        }
        public static List<MyStore> GetBySCIDAID(Guid scid, Guid aid, bool ia) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MyStoreDB where i.StoreCategoryID == scid && i.ApplicationID == aid && i.isApproved == ia select i).ToList();
                return query;
            }
        }
        public static List<MyStore> GetByStoreName(string storeName) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MyStoreDB where i.Name.Equals(storeName) select i).ToList();
                return query;
            }
        }

        #endregion
        #region functionalities
        public static MyStoreVM SetSubData(MyStore data, Guid aid)
        {
            var model = MyStoreVM.MToVM(data);
            model.StoreCategory = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.StoreCategoryID));
            model.StoreLogo = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByID(data.StoreLogoID, data.ID, aid));
            model.StoreBackgroundImage = ImageLinkStorageVM.MToVM(ImageLinkStorageService.GetByID(data.StoreBackgroundImageID, data.ID, aid));
            return model;
        }
        public static List<MyStoreVM> SetSubDatas(List<MyStore> list, Guid aid)
        {
            var nlist = new List<MyStoreVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        #endregion

        #region securityModule
        public static bool IsStoreNameAllowUse(string name) {
            try {
                //if it has same name then do not use
                return GetByStoreName(name).Count > 0 ? false : true;
            } catch { return false;}
        }
        #endregion

    }
}