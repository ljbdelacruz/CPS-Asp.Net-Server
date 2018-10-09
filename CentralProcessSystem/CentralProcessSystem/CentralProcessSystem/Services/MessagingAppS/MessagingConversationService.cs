using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using CentralProcessSystem.Services.DateTimeStorageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.MessagingAppS
{
    public static class MessagingConversationService
    {
        #region db queries
        public static bool Insert(Guid id, string text, Guid mtid, Guid sid, Guid rid, Guid dtid, bool ia){
            try {
                using (var context = new CentralProcessContext()) {
                    var data = MessagingConversationVM.set(id, text, mtid, sid, rid, dtid, ia);
                    context.MessagingConversationDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid rid) {
            try
            {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MessagingConversationDB where i.ID == id && i.RoomID == rid select i).FirstOrDefault();
                    context.MessagingConversationDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string text, Guid rid, Guid dtid, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MessagingConversationDB where i.ID == id && i.RoomID == rid select i).FirstOrDefault();
                    query.Text = text;
                    query.DateTimeStorageID = dtid;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<MessagingConversation> GetByRID(Guid rid, bool ia) {
            using (var context = new CentralProcessContext()){
                var query = (from i in context.MessagingConversationDB where i.RoomID == rid && i.isArchived==ia select i).ToList();
                return query;
            }
        }
        public static MessagingConversation GetByID(Guid id) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MessagingConversationDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        #endregion
        #region functionalities
        public static MessagingConversationVM SetSubData(MessagingConversation data)
        {
            var model = MessagingConversationVM.MToVM(data);
            model.DateTime = DateTimeStorageVM.MToVM(DateTimeStorageService.GetByID(data.DateTimeStorageID));
            return model;
        }
        public static List<MessagingConversationVM> SetSubDatas(List<MessagingConversation> list)
        {
            var nlist = new List<MessagingConversationVM>();
            foreach (var item in list)
            {
                var model = SetSubData(item);
                nlist.Add(model);
            }
            return nlist;
        }
        public static List<MessagingConversationVM> OrderByDateTime(List<MessagingConversationVM> models, bool isDesc) {
            return isDesc ? models.OrderByDescending(x => x.DateTime.UpdatedAt).ToList() : models.OrderBy(x => x.DateTime.UpdatedAt).ToList();
        }
        #endregion
    }
}