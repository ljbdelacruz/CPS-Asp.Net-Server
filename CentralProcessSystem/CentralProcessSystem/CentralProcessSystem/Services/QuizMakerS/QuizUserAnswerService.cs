using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.QuizMakerS
{
    public static class QuizUserAnswerService
    {
        #region db query
        public static bool Insert(Guid id, Guid qtid, Guid qqid, Guid qaid, string oa, float pe) {
            try {
                var data = QuizUserAnswerVM.set(id, qtid, qqid, qaid, oa, pe);
                using (var context = new CentralProcessContext()) {
                    context.QuizUserAnswerDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid qtid, Guid qqid, Guid qaid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizUserAnswerDB where i.ID == id && i.QuizTakersID == qtid && i.QuizQuestionID == qqid && i.QuizAnswerID == qaid select i).FirstOrDefault();
                    context.QuizUserAnswerDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool RemoveByQTID(Guid qtid) {
            try {
                var data = GetByQTID(qtid);
                foreach (var model in data) {
                    Remove(model.ID, qtid, model.QuizQuestionID, model.QuizAnswerID);
                }
                return true;
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid qtid, Guid qqid, Guid qaid, string oa, float pe) {
            try {
                using (var context = new CentralProcessContext())
                {
                    var query = (from i in context.QuizUserAnswerDB where i.ID == id && i.QuizTakersID == qtid && i.QuizQuestionID == qqid && i.QuizAnswerID == qaid select i).FirstOrDefault();
                    query.QuizAnswerID = qaid;
                    query.OtherAnswer = oa;
                    query.PointsEarned = pe;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizUserAnswer> GetByQQIDQTID(Guid qqid, Guid qtid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizUserAnswerDB where i.QuizQuestionID == qqid && i.QuizTakersID == qtid select i).ToList();
                return query;
            }
        }
        public static List<QuizUserAnswer> GetByQQIDQAID(Guid qqid, Guid qaid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizUserAnswerDB where i.QuizAnswerID == qaid && i.QuizQuestionID == qqid select i).ToList();
                return query;
            }
        }
        public static List<QuizUserAnswer> GetByQTID(Guid qtid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizUserAnswerDB where i.QuizTakersID == qtid select i).ToList();
                return query;
            }
        }


        #endregion

    }
}