using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Owin;
using CityParkWeb.ClassUtils;

namespace CityParkWeb.Server.Hubs
{
    public class recived : Hub
    {
        public void SendPosition(LivePositionRequest livePositionRequest)
        {
            

            if (true)
            {

            }

            Clients.All.MessageReceived(livePositionRequest);
        }
    }
}
