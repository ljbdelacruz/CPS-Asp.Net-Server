using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.ImageLinkStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.QuizMakerS
{
    public static class QuizQuestionsService
    {
        #region database queries
        public static bool Insert(Guid id, string ques, Guid qiid, int order, int points, Guid statID) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = QuizQuestionsVM.set(id, ques, qiid, order, points, statID);
                    context.QuizQuestionsDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid qiid, Guid aid, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizQuestionsDB where i.ID == id && i.QuizInfoID == qiid select i).FirstOrDefault();
                    context.QuizQuestionsDB.Remove(query);
                    context.SaveChanges();
                    if (QuizQuestionAnswerService.RemoveByQQID(id, aid, cid) && ImageLinkStorageService.RemoveByOID(id, aid)) {
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }
        public static bool RemoveByQIID(Guid id, Guid aid, Guid cid) {
            try {
                var data = GetByQIID(id);
                foreach (var model in data) {
                    Remove(model.ID, id, aid, cid);
                }
                return true;
            } catch { return false; }
        }

        public static bool Update(Guid id, Guid qiid, string ques, int order, int points, Guid sid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizQuestionsDB where i.ID == id && i.QuizInfoID == qiid select i).FirstOrDefault();
                    query.Questions = ques;
                    query.Order = order;
                    query.Points = points;
                    query.Status = sid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizQuestions> GetByQIID(Guid qiid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizQuestionsDB where i.QuizInfoID == qiid select i).ToList();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static QuizQuestionsVM SetSubData(QuizQuestions data, Guid aid, bool isSurvey)
        {
            var model = QuizQuestionsVM.MToVM(data);
            model.Images = ImageLinkStorageVM.MsToVMs(ImageLinkStorageService.GetByOIDAPI(data.ID, aid));
            model.Status = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.Status));
            model.Choices = isSurvey? QuizQuestionAnswerService.SetSurveyFormats(QuizQuestionAnswerService.GetByQQID(data.ID), aid, data.QuizInfoID) : QuizQuestionAnswerService.SetSubDatas(QuizQuestionAnswerService.GetByQQID(data.ID), aid);
            return model;
        }
        public static List<QuizQuestionsVM> SetSubDatas(List<QuizQuestions> list, Guid aid)
        {
            var nlist = new List<QuizQuestionsVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid, false);
                nlist.Add(model);
            }
            return nlist;
        }
        public static List<QuizQuestionsVM> SetBySurveyFormat(List<QuizQuestions> models, Guid aid) {
            var list = new List<QuizQuestionsVM>();
            foreach (var model in models) {
                list.Add(SetSubData(model, aid, true));
            }
            return list;
        }

        public static List<QuizQuestionsVM> SetUserAnswers(Guid qtid, List<QuizQuestionsVM> list) {
            foreach (var item in list) {
                item.UserAnswers = QuizUserAnswerVM.MsToVMs(QuizUserAnswerService.GetByQQIDQTID(Guid.Parse(item.ID), qtid));
            }
            return list;
        }
        public static List<QuizQuestions> SortByOrder(List<QuizQuestions> list, bool isDesc) {
            if (isDesc) {
                return list.OrderByDescending(x => x.Order).ToList();
            }
            return list.OrderBy(x => x.Order).ToList();
        }

        #endregion
    }
}