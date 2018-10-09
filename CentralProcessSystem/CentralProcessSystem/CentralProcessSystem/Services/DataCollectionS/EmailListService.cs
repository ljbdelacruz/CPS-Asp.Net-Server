using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.DataCollectionS
{
    public static class EmailListService
    {
        #region db queries
        public static bool Insert(Guid id, string name, string email, Guid dtid, string cnum) {
            try {
                var data = EmailListVM.set(id, name, email, dtid, cnum);
                using (var context = new CentralProcessContext()){
                    context.EmailListDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.EmailListDB where i.ID == id select i).FirstOrDefault();
                    context.EmailListDB.Add(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string name, string email, Guid dtid, string cnum) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.EmailListDB where i.ID == id select i).FirstOrDefault();
                    query.Name = name;
                    query.Email = email;
                    query.DateTimeID = dtid;
                    query.ContactNumber = cnum;
                    context.SaveChanges();

                    return true;
                }
            } catch { return false; }
        }
        public static EmailList GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.EmailListDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<EmailList> GetAll() {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.EmailListDB select i).ToList();
                return query;
            }
        }
        public static EmailList GetByEAdd(string email) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.EmailListDB where i.Email.Equals(email) select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
    }
}