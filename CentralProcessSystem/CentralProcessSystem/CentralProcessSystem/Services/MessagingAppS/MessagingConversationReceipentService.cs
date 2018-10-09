using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.MessagingAppS
{
    public static class MessagingConversationReceipentService
    {
        #region db query
        public static bool Insert(Guid id, Guid rid, Guid mcid, bool ir, Guid recID) {
            var data = MessagingConversationReceipentVM.set(id, rid, mcid, ir, recID);
            try {
                using (var context = new CentralProcessContext()) {
                    context.MessagingConversationReceipentDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool RemoveAdmin(Guid id) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MessagingConversationDB where i.ID == id select i).FirstOrDefault();
                    context.MessagingConversationDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateAdmin(Guid id, bool ir) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MessagingConversationReceipentDB where i.ID == id select i).FirstOrDefault();
                    query.isRead = ir;
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateList(List<MessagingConversationReceipent> models, int mode, bool ir) {
            try {
                foreach (var model in models) {
                    if (mode == 0) {
                        if (!UpdateAdmin(model.ID, ir))
                        {
                            return false;
                        }
                    }
                }
                return true;
            } catch { return false; }
        }

        public static List<MessagingConversationReceipent> GetByReceiverIDRoomID(Guid rid, Guid rmid, bool ir) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MessagingConversationReceipentDB where i.ReceiverID == rid && i.RoomID == rmid && i.isRead == ir select i).ToList();
                return query;
            }
        }
        #endregion

        #region util
        public static MessagingConversationReceipentVM SetSubData(MessagingConversationReceipent model) {
            var data = MessagingConversationReceipentVM.MToVM(model);
            data.MessagingConversation = MessagingConversationService.SetSubData(MessagingConversationService.GetByID(model.MessagingConversationID));
            return data;
        }
        public static List<MessagingConversationReceipentVM> SetSubDatas(List<MessagingConversationReceipent> models, int mode) {
            var list = new List<MessagingConversationReceipentVM>();
            foreach (var model in models) {
                list.Add(mode==0?SetSubData(model):SetSubData(model));
            }
            return list;
        }

        #endregion

        #region sorting
        public static List<MessagingConversationReceipentVM> SortByDateTime(List<MessagingConversationReceipentVM> list, bool isDesc) {
            return isDesc ? list.OrderByDescending(x => x.MessagingConversation.DateTime.UpdatedAt).ToList() : list.OrderBy(x => x.MessagingConversation.DateTime.UpdatedAt).ToList();
        }
        #endregion


    }
}