using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using CentralProcessSystem.Services.StatusReferenceS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.QuizMakerS
{
    public static class QuizInfoService
    {
        #region db queries
        public static bool Insert(Guid id, string name, Guid oid, Guid aid, string qc, bool htl, Guid sid, Guid qsid, Guid dtid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var data = QuizInfoVM.set(id, name, oid, aid, qc, htl, sid, qsid, dtid);
                    context.QuizInfoDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid, Guid oid, Guid cid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizInfoDB where i.ID == id && i.ApplicationID == aid && i.OwnerID == oid select i).FirstOrDefault();
                    context.QuizInfoDB.Remove(query);
                    context.SaveChanges();
                    //removes quizQuestions and datetime associated with this tables
                    if (QuizQuestionsService.RemoveByQIID(id, aid, cid) && DateTimeStorageService.Remove(query.DateTimeStorageID, aid, query.ID)){
                        return true;
                    }
                    return false;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid aid, Guid oid, string name, string qc, bool htl, Guid status, Guid qs, Guid dtsid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.QuizInfoDB where i.ID == id && i.ApplicationID == aid && i.OwnerID == oid select i).FirstOrDefault();
                    query.Name = name;
                    query.QuizCode = qc;
                    query.hasTimeLimit = htl;
                    query.Status = status;
                    query.QuizStatus = qs;
                    query.DateTimeStorageID = dtsid;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizInfo> GetByOIDAID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizInfoDB where i.OwnerID == oid && i.ApplicationID == aid select i).ToList();
                return query;
            }
        }
        public static QuizInfo GetByID(Guid id, Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizInfoDB where i.ID == id && i.OwnerID == oid && i.ApplicationID == aid select i).FirstOrDefault();
                return query;
            }
        }
        public static QuizInfo GetByQuizCode(string qc, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.QuizInfoDB where i.QuizCode.Equals(qc) && i.ApplicationID == aid select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static QuizInfoVM SetSubData(QuizInfo data, Guid aid)
        {
            var model = QuizInfoVM.MToVM(data);
            model.QuizStatus = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.QuizStatus));
            model.Status = StatusTypesReferencesVM.MToVM(StatusTypesReferencesService.GetByID(data.Status));
            model.DateTimeStorage = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            return model;
        }
        public static List<QuizInfoVM> SetSubDatas(List<QuizInfo> list, Guid aid)
        {
            var nlist = new List<QuizInfoVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item, aid);
                nlist.Add(model);
            }
            return nlist;
        }

        #endregion

    }
}