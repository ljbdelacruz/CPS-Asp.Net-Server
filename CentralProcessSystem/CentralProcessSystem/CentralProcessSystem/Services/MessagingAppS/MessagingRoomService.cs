using CentralProcessSystem.Context;
using CentralProcessSystem.Models.CPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Services.MessagingAppS
{
    public static class MessagingRoomService
    {
        public static bool Insert(Guid id, string name, Guid oid, Guid aid, Guid dtid, bool ia) {
            try {
                var data = MessagingRoomVM.set(id, name, oid, aid, dtid, ia);
                using (var context = new CentralProcessContext()) {
                    context.MessagingRoomDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid aid, string name, Guid dtid, bool ia) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MessagingRoomDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    query.Name = name;
                    query.DateTimeStorageID = dtid;
                    query.isArchived = ia;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid aid) {
            try {
                using (var context = new CentralProcessContext()) {
                    var query = (from i in context.MessagingRoomDB where i.ID == id && i.OwnerID == oid && i.API == aid select i).FirstOrDefault();
                    context.MessagingRoomDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<MessagingRoom> GetByOIDAID(Guid oid, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MessagingRoomDB where i.OwnerID == oid && i.API == aid select i).ToList();
                return query;
            }
        }
        public static MessagingRoom GetByID(Guid id, Guid aid) {
            using (var context = new CentralProcessContext()) {
                var query = (from i in context.MessagingRoomDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                return query;
            }
        }

    }
}