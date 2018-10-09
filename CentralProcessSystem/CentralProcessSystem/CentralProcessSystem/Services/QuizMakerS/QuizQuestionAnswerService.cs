using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.QuizMakerS
{
    public static class QuizQuestionAnswerService
    {
        #region db queries
        public static bool Insert(Guid id, string desc, float points, bool ic, Guid qqid) {
            try {
                var data = QuizQuestionAnswerVM.set(id, desc, points, ic, qqid);
                using (var context = new CentralProcessContext()) {
                    context.QuizQuestionAnswerDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid qqid, Guid aid, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizQuestionAnswerDB where i.ID == id && i.QuizQuestionsID == qqid select i).FirstOrDefault();
                    context.QuizQuestionAnswerDB.Remove(query);
                    context.SaveChanges();
                    //removes images associated with this data
                    if (ImageLinkStorageService.RemoveByOID(query.ID, aid)) {
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }
        public static bool RemoveByQQID(Guid qqid, Guid aid, Guid cid) {
            try {
                var data = GetByQQID(qqid);
                foreach (var model in data) {
                    Remove(model.ID, qqid, aid, cid);
                }
                return true;
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid qqid, string desc, float points, bool ic) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizQuestionAnswerDB where i.ID == id && i.QuizQuestionsID == qqid select i).FirstOrDefault();
                    query.Description = desc;
                    query.Points = points;
                    query.isCorrect = ic;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizQuestionAnswer> GetByQQID(Guid qqid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizQuestionAnswerDB where i.QuizQuestionsID == qqid select i).ToList();
                return query;
            }
        }
        public static QuizQuestionAnswer GetByID(Guid id, Guid qqid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizQuestionAnswerDB where i.ID == id && i.QuizQuestionsID == qqid select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static QuizQuestionAnswerVM SetSubData(QuizQuestionAnswer data, Guid aid)
        {
            var model = QuizQuestionAnswerVM.MToVM(data);
            model.Images = ImageLinkStorageVM.MsToVMs(ImageLinkStorageService.GetByOIDAPI(data.ID, aid));
            return model;
        }
        public static List<QuizQuestionAnswerVM> SetSubDatas(List<QuizQuestionAnswer> list, Guid aid)
        {
            var nlist = new List<QuizQuestionAnswerVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }
        public static QuizQuestionAnswerVM SetSurveyFormat(QuizQuestionAnswer model, Guid qiid, int takerCount, Guid aid) {
            var temp = SetSubData(model, aid);
            var answered = QuizUserAnswerService.GetByQQIDQAID(model.QuizQuestionsID, model.ID).Count();
            temp.Percent = takerCount>0? (answered / takerCount) * 100:0;
            return temp;
        }
        public static List<QuizQuestionAnswerVM> SetSurveyFormats(List<QuizQuestionAnswer> models, Guid aid, Guid qiid) {
            var list = new List<QuizQuestionAnswerVM>();
            foreach (var model in models) {
                list.Add(SetSurveyFormat(model, qiid, QuizTakerService.GetByQIID(qiid).Count, aid));
            }
            return list;
        }


        #endregion
    }
}