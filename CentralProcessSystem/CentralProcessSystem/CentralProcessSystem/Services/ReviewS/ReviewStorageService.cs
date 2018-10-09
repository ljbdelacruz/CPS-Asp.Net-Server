using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.ReviewS
{
    public static class ReviewStorageService
    {
        #region db request
        public static bool Insert(Guid id, Guid sid, Guid rid, Guid aid, string title, string comment, int stars, Guid dtsID, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = ReviewStoragesVM.set(id, sid, rid, aid, title, comment, stars, dtsID, ia);
                    context.ReviewStoragesDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ReviewStoragesDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                    context.ReviewStoragesDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid aid, string title, string comment, int stars, Guid dtsid, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ReviewStoragesDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                    query.Title = title;
                    query.Comment = comment;
                    query.Stars = stars;
                    query.DateTimeStorageID = dtsid;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<ReviewStorages> GetByAIDSID(Guid aid, Guid sid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ReviewStoragesDB where i.API == aid && i.SenderID == sid select i).ToList();
                return query;
            }
        }
        public static List<ReviewStorages> GetByAIDRID(Guid aid, Guid rid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ReviewStoragesDB where i.API == aid && i.ReviewedID == rid select i).ToList();
                return query;
            }
        }
        public static List<ReviewStorages> GetByAID(Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ReviewStoragesDB where i.API == aid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static ReviewStoragesVM SetSubData(ReviewStorages data, Guid aid)
        {
            var model = ReviewStoragesVM.MToVM(data);
            model.DateTime = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            return model;
        }
        public static List<ReviewStoragesVM> SetSubDatas(List<ReviewStorages> list, Guid aid)
        {
            var nlist = new List<ReviewStoragesVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }

        //orders the reviews by updatedAt
        public static List<ReviewStoragesVM> SortByMyDateUA(List<ReviewStoragesVM> list) {
            return list.OrderByDescending(x => x.DateTime.UpdatedAt).ToList();
        }
        //orders the reviews by createdAt
        public static List<ReviewStoragesVM> SortByMyDateCA(List<ReviewStoragesVM> list)
        {
            return list.OrderByDescending(x => x.DateTime.CreatedAt).ToList();
        }
        #endregion
    }
}