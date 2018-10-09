using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.ScoreStorageS
{
    public static class ScoreStorageService
    {
        #region db query
        public static bool Insert(Guid id, Guid aid, string name, int score, Guid dtid) {
            try {
                var data = ScoreStorageVM.set(id, aid, name, score, dtid);
                using (var context = new CentralProcessContext()) {
                    context.ScoreStorageDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string name, int score) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.ScoreStorageDB where i.ID == id && i.Name.Equals(name) select i).FirstOrDefault();
                    query.Score = score;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<ScoreStorage> GetByAID(Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.ScoreStorageDB where i.API == aid select i).ToList();
                return query;
            }
        }

        #endregion
        #region sorting
        public static List<ScoreStorage> SortByScore(bool isDesc, List<ScoreStorage> list) {
            return isDesc ? list.OrderByDescending(x => x.Score).ToList() : list.OrderBy(x => x.Score).ToList();
        }
        #endregion

    }
}