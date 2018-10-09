using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.LocationStorageS
{
    public static class LocationStorageService
    {
        #region db queries
        public static bool Insert(Guid id, Guid oid, Guid lcid, float longi, float lat, string desc, Guid dtID, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = LocationStorageVM.set(id, oid, lcid, longi, lat, desc, dtID, ia);
                    context.LocationStorageDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.LocationStorageDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                    context.LocationStorageDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, float longi, float lat, string description) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.LocationStorageDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                    query.Longitude = longi;
                    query.Latitude = lat;
                    query.Description = description;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static LocationStorage GetByIDOID(Guid id, Guid oid, Guid lcid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.LocationStorageDB where i.ID == id && i.OwnerID == oid && i.LocationCategoryID == lcid select i).FirstOrDefault();
                return query;
            }
        }
        public static List<LocationStorage> GetByOID(Guid oid, Guid lcid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.LocationStorageDB where i.OwnerID == oid && i.LocationCategoryID == lcid select i).ToList();
                return query;
            }
        }
        public static List<LocationStorage> GetByLCID(Guid lcid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.LocationStorageDB where i.LocationCategoryID == lcid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static LocationStorageVM SetSubData(LocationStorage data, Guid aid)
        {
            var model = LocationStorageVM.MToVM(data);
            return model;
        }
        public static List<LocationStorageVM> SetSubDatas(List<LocationStorage> list, Guid aid)
        {
            var nlist = new List<LocationStorageVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        public static bool IsWithinRange(LocationStorageVM vm, float radius, float longitude, float latitude)
        {
            try
            {
                var nlong = radius + longitude;
                var nlat = radius + latitude;
                if ((vm.Longitude <= nlong || vm.Longitude >= -nlong) && (vm.Latitude <= nlat || vm.Latitude >= -nlat))
                {
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        #endregion
    }
}