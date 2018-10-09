using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.General;
using CentralProcessSystem.Services.QuizMakerS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralProcessSystem.api.QuizMakerAPI
{
    public class QuizMakerController : Controller
    {
        #region quizInfo
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var qc = Request.Form["qc"];
                var htl = Boolean.Parse(Request.Form["htl"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var qsid = Guid.Parse(Request.Form["qsid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (QuizInfoService.Insert(id, name, oid, aid, qc, htl, sid, qsid, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Quiz"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                if (QuizInfoService.Remove(id, aid, oid, cid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Quiz"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var qc = Request.Form["qc"];
                var htl = Boolean.Parse(Request.Form["htl"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                var qsid = Guid.Parse(Request.Form["qsid"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (QuizInfoService.Update(id, aid, oid, name, qc, htl, sid, qsid, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Quiz"));
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIInputCode() {
            try {
                var qc = Request.Form["qc"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = QuizInfoService.GetByQuizCode(qc, aid);
                var vm = QuizInfoService.SetSubData(data, aid);
                return Success(vm);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QIGetByOwner(string id, string aid) {
            try {
                var data = QuizInfoService.GetByOIDAID(Guid.Parse(id), Guid.Parse(aid));
                var vms = QuizInfoService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QIGetByID(string id, string oid, string aid) {
            try {
                var data = QuizInfoService.GetByID(Guid.Parse(id), Guid.Parse(oid), Guid.Parse(aid));
                var vm = QuizInfoService.SetSubData(data, Guid.Parse(aid));
                return Success(vm);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIGetByTakers() {
            try {
                var uid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = QuizTakerService.GetByUserID(uid, aid);
                var vms = QuizTakerService.SetSubDatas(data, aid);
                var vmList = new List<QuizInfoVM>();
                foreach (var vm in vms) {
                    vmList.Add(vm.QuizInfo);
                }
                return Success(vmList);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIEnterCode() {
            try {
                var code = Request.Form["qc"];
                var aid = Guid.Parse(Request.Form["aid"]);
                var uid = Guid.Parse(Request.Form["uid"]);

                var data = QuizInfoService.GetByQuizCode(code, aid);
                var vms = QuizInfoService.SetSubData(data, aid);
                if (vms.QuizStatus.Name.Equals("Closed")) {
                    return Failed(MessageUtilityService.ContactAdmin("Quiz"));
                }
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #endregion
        #region QuizQuestion
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var ques = Request.Form["ques"];
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var order = int.Parse(Request.Form["order"]);
                var points = int.Parse(Request.Form["p"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                if (QuizQuestionsService.Insert(id, ques, qiid, order, points, sid)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Question"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                if (QuizQuestionsService.Remove(id, qiid, aid, cid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Question"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var ques = Request.Form["ques"];
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var order = int.Parse(Request.Form["order"]);
                var points = int.Parse(Request.Form["p"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                if (QuizQuestionsService.Update(id, qiid, ques, order, points, sid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.ServerError());
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QQGet(string id, string aid) {
            try {
                var data = QuizQuestionsService.GetByQIID(Guid.Parse(id));
                var sorted = QuizQuestionsService.SortByOrder(data, false);
                var vms = QuizQuestionsService.SetSubDatas(sorted, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //get questions and user answers
        
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQGetQUA()
        {
            try
            {
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var qtid = Guid.Parse(Request.Form["qtid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var questions = QuizQuestionsService.GetByQIID(qiid);
                var vmQuestions = QuizQuestionsService.SetSubDatas(questions, aid);
                vmQuestions = QuizQuestionsService.SetUserAnswers(qtid, vmQuestions);
                return Success(vmQuestions);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        /*get question in survey format
            param: quizInfoID
        */
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQGetBySurvey() {
            try {
                var qiid = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = QuizQuestionsService.GetByQIID(qiid);
                var vms = QuizQuestionsService.SetBySurveyFormat(data, aid);
                return Success(vms);

            } catch {
                return Failed(MessageUtilityService.ServerError());
            }
        }
        #endregion
        #endregion
        #region QuizQuestionAnswer
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQAInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var desc = Request.Form["desc"];
                var point = float.Parse(Request.Form["p"]);
                var ic = Boolean.Parse(Request.Form["ic"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                if (QuizQuestionAnswerService.Insert(id, desc, point, ic, qqid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Choice"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQARemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (QuizQuestionAnswerService.Remove(id, qqid, aid, cid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Choice"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQAUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var desc = Request.Form["desc"];
                var point = float.Parse(Request.Form["p"]);
                var ic = Boolean.Parse(Request.Form["ic"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                if (QuizQuestionAnswerService.Update(id, qqid, desc, point, ic)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Choice"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get

        [AllowCrossSiteJson]
        [HttpGet]
        //nid=qqid
        public async Task<JsonResult> QQAGet(string id, string aid, string nid) {
            try {
                var data = QuizQuestionAnswerService.GetByID(Guid.Parse(id), Guid.Parse(nid));
                var vms = QuizQuestionAnswerService.SetSubData(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //qqid, aid
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QQAGetByQuestion(string id, string aid) {
            try {
                var data = QuizQuestionAnswerService.GetByQQID(Guid.Parse(id));
                var vms = QuizQuestionAnswerService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }

        #endregion
        #endregion
        #region QuizTakers
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var tp = float.Parse(Request.Form["tp"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                //check if user already in quizTaker then inform them
                if(QuizTakerService.GetByUQA(uid, qiid, aid) !=null){
                    return Failed(MessageUtilityService.AlreadyInRecord("owner of this quiz"));
                }
                if (QuizTakerService.Insert(id, qiid, uid, tp, dtid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Takers"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (QuizTakerService.Remove(id, qiid, aid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Takers"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var tp = int.Parse(Request.Form["tp"]);
                var dtid = Guid.Parse(Request.Form["dtid"]);
                if (QuizTakerService.Update(id, qiid, uid, tp, dtid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Takers"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //check test and calculate score
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTCheckTestScore()
        {
            try
            {
                var qtid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var answers = QuizUserAnswerService.GetByQTID(qtid);
                var ts = 0f;
                foreach (var answer in answers){
                    //if multiple choice
                    if (answer.OtherAnswer.Length <= 0) {
                        var choiceSelected = QuizQuestionAnswerService.GetByID(answer.QuizAnswerID, answer.QuizQuestionID);
                        answer.PointsEarned = choiceSelected.Points;
                    }
                    //if essay retain the assigned score
                    QuizUserAnswerService.Update(answer.ID, answer.QuizTakersID, answer.QuizQuestionID, answer.QuizAnswerID, answer.OtherAnswer, answer.PointsEarned);
                    ts += answer.PointsEarned;
                }
                //update total score
                var qtModel = QuizTakerService.GetByID(qtid);
                QuizTakerService.Update(qtid, qtModel.QuizInfoID, qtModel.UserID, ts, qtModel.DateTimeStorageID);
                return Success(true);
            }
            catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get


        //qiid, aid
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QTGetByQuiz(string id, string aid) {
            try {
                var data = QuizTakerService.GetByQIID(Guid.Parse(id));
                var vms = QuizTakerService.SetSubDatas(data, Guid.Parse(aid));
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //userID, aid
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTGet() {
            try {
                var oid = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = QuizTakerService.GetByUserID(oid, aid);
                var vms = QuizTakerService.SetSubDatas(data, aid);
                vms = QuizTakerService.SortByDate(vms, false);
                return Success(vms);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //userID, qiid, aid
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTGetUQA() {
            try {
                var uid = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = QuizTakerService.GetByUQA(uid, qiid, aid);
                var vm = QuizTakerService.SetSubData(data, aid);
                return Success(vm);

            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        //qiid, aid with subdata of users
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTGetQAU() {
            try {
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var data = QuizTakerService.GetByQIID(qiid);
                var vms = QuizTakerService.SetSubDatas(data, aid);
                vms = QuizTakerService.SetUser(vms);
                return Success(vms);

            } catch { return Failed(MessageUtilityService.ServerError()); }
        }


        #endregion
        #endregion
        #region QuizUserAnswer
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUAInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qtid = Guid.Parse(Request.Form["qtid"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                var qaid = Guid.Parse(Request.Form["qaid"]);
                var oa = Request.Form["oa"];
                var pe = float.Parse(Request.Form["pe"]);
                if (QuizUserAnswerService.Insert(id, qtid, qqid, qaid, oa, pe)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedInsert("Answer"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUARemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qtid = Guid.Parse(Request.Form["qtid"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                var qaid = Guid.Parse(Request.Form["qaid"]);
                if (QuizUserAnswerService.Remove(id, qtid, qqid, qaid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedRemove("Answer"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUAUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qtid = Guid.Parse(Request.Form["qtid"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                var qaid = Guid.Parse(Request.Form["qaid"]);
                var oa = Request.Form["oa"];
                var pe = float.Parse(Request.Form["pe"]);
                if (QuizUserAnswerService.Update(id, qtid, qqid, qaid, oa, pe)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtilityService.FailedUpdate("Answer"));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        //questionID, quizTakerID
        public async Task<JsonResult> QUAGet(string id, string qtid) {
            try {
                var data = QuizUserAnswerService.GetByQQIDQTID(Guid.Parse(id), Guid.Parse(qtid));
                return Success(QuizUserAnswerVM.MsToVMs(data));
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUAGet() {
            try {
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                var qtid = Guid.Parse(Request.Form["qtid"]);
                var questions = QuizQuestionsService.GetByQIID(qiid);
                questions= QuizQuestionsService.SortByOrder(questions, false);
                var vmQues = QuizQuestionsService.SetSubDatas(questions, aid);
                var uanswers = QuizUserAnswerVM.MsToVMs(QuizUserAnswerService.GetByQTID(qtid));
                foreach (var model in vmQues) {
                    model.UserAnswers = uanswers.Where(x => x.QuizQuestionID == model.ID).ToList();
                }
                return Success(vmQues);
            } catch { return Failed(MessageUtilityService.ServerError()); }
        }
        #endregion

        #endregion
        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}