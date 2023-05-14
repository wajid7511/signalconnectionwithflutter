using System;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Core
{
    public class NotifyHub : Hub
    {
        public NotifyHub()
        {
        }
        public async ValueTask<bool> NotifyClient()
        {
            if (Clients != null)
            {
                await Clients.All.SendAsync("NotifyClient", "This is Asp.Net Core connections");
                return true;
            }
            return false;
        }
    }
}

