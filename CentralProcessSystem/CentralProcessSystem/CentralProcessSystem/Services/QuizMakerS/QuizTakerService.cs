using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.UserInfoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.QuizMakerS
{
    public static class QuizTakerService
    {
        #region db queries
        public static bool Insert(Guid id, Guid qiid, Guid uid, float tp, Guid dtid, Guid aid) {
            try {
                var data = QuizTakersVM.set(id, qiid, uid, tp, dtid, aid);
                using (var context = new CentralProcessContext()) {
                    context.QuizTakersDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid qiid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizTakersDB where i.ID == id && i.QuizInfoID == qiid select i).FirstOrDefault();
                    context.QuizTakersDB.Remove(query);
                    context.SaveChanges();
                    //remove quizUserAnswers
                    if (QuizUserAnswerService.RemoveByQTID(id) && DateTimeStorageService.Remove(query.DateTimeStorageID, aid, query.ID)) {
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }
        

        public static bool Update(Guid id, Guid qiid, Guid uid, float tp, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizTakersDB where i.ID == id && i.QuizInfoID == qiid select i).FirstOrDefault();
                    query.TotalPoints = tp;
                    query.DateTimeStorageID = dtid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static QuizTakers GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizTakersDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static QuizTakers GetByUQA(Guid id, Guid qiid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizTakersDB where i.UserID == id && i.QuizInfoID == qiid && i.API == aid select i).FirstOrDefault();
                return query;
            }
        }
        public static List<QuizTakers> GetByQIID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizTakersDB where i.QuizInfoID == id select i).ToList();
                return query;
            }
        }
        public static List<QuizTakers> GetByUserID(Guid id, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizTakersDB where i.UserID == id && i.API == aid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static QuizTakersVM SetSubData(QuizTakers data, Guid aid)
        {
            var model = QuizTakersVM.MToVM(data);
            model.QuizInfo = QuizInfoService.SetSubData(QuizInfoService.GetByID(data.QuizInfoID, data.UserID, aid), aid);
            model.DateTime = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            return model;
        }
        public static List<QuizTakersVM> SetSubDatas(List<QuizTakers> list, Guid aid)
        {
            var nlist = new List<QuizTakersVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        public static List<QuizTakersVM> SortByTotalPoint(List<QuizTakersVM> list, bool isDesc) {
            return isDesc ? list.OrderByDescending(x => x.TotalPoints).ToList() : list.OrderBy(x => x.TotalPoints).ToList();
        }
        public static List<QuizTakersVM> SetUser(List<QuizTakersVM> list) {
            foreach (var model in list) {
                model.User = UsersVM.MToVM(UsersService.GetByID(Guid.Parse(model.UserID)));
            }
            return list;
        }
        public static List<QuizTakersVM> SortByDate(List<QuizTakersVM> list, bool isDesc)
        {
            return isDesc ? list.OrderByDescending(x => x.DateTime.CreatedAt).ToList() : list.OrderBy(x => x.DateTime.CreatedAt).ToList();
        }
        #endregion
    }
}