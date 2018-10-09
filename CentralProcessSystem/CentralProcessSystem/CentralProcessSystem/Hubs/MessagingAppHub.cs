using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace CentralProcessSystem.Hubs
{
    public class MessagingAppHub : Hub
    {
        private readonly IHubContext _uptimeHub;
        public MessagingAppHub()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<MessagingAppHub>();
        }
        public static void NewMessage(string roomID, string mcid, string api, string signalRID){
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessagingAppHub>();
            hubContext.Clients.Client(signalRID).newmessage(roomID, mcid, api);
        }


    }
}