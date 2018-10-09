using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CentralProcessSystem.Hubs
{
    public class NotificationManagerHub : Hub
    {
        private readonly IHubContext _uptimeHub;
        public NotificationManagerHub()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<MessagingAppHub>();
        }
        //nid=notificationManagerID
        public static void NewNotification(string nid, string signalRID)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationManagerHub>();
            hubContext.Clients.Client(signalRID).newnotif(nid);
        }
    }
}