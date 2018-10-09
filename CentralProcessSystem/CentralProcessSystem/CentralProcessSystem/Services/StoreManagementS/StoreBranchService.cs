using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.LocationStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.StoreManagementS
{
    public static class StoreBranchService
    {
        #region db queries
        public static bool Insert(Guid id, Guid sid, Guid aid, Guid gid, string address, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = StoreBranchVM.set(id, sid, aid, gid, address, dtid);
                    context.StoreBranchDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid sid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.StoreBranchDB where i.ID == id && i.StoreID == sid select i).FirstOrDefault();
                    context.StoreBranchDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid sid, Guid aid, Guid gid, string address, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.StoreBranchDB where i.ID == id && i.StoreID == sid select i).FirstOrDefault();
                    query.AttendantID = aid;
                    query.GeoLocationID = gid;
                    query.Address = address;
                    query.DateTimeStorageID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<StoreBranch> GetBySID(Guid sid) {
            using (var context = new CentralProcessContext()){
                var query = (from i in context.StoreBranchDB where i.StoreID == sid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static StoreBranchVM SetSubData(StoreBranch data, Guid aid, Guid lcid)
        {
            var model = StoreBranchVM.MToVM(data);
            model.Geolocation = LocationStorageVM.MToVM(LocationStorageService.GetByIDOID(data.GeoLocationID, data.ID, lcid));
            return model;
        }
        public static List<StoreBranchVM> SetSubDatas(List<StoreBranch> list, Guid aid, Guid lcid)
        {
            var nlist = new List<StoreBranchVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid, lcid);
                nlist.Add(model);
            }
            return nlist;
        }
        //get branches within the radius of the location of the user
        public static List<StoreBranchVM> GetNearestBranchByLocation(Guid storeID, Guid aid, Guid lcid, float radius, float longi, float lat) {
            var data = GetBySID(storeID);
            var vms = SetSubDatas(data, aid, lcid);
            var nbranch = new List<StoreBranchVM>();
            foreach (var vm in vms) {
                //check branches within range based on location
                if (LocationStorageService.IsWithinRange(vm.Geolocation, radius, longi, lat)) {
                    nbranch.Add(vm);
                }
            }
            return nbranch;
        }

        #endregion
    }
}