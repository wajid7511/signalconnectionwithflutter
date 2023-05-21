using System;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Core
{
    public class NotifyHub : Hub
    {
        public NotifyHub()
        {
        }
        public async ValueTask<bool> NotifyClients(string message)
        {
            if (Clients != null)
            {
                await Clients.All.SendAsync("NotifyClient", message);
                return true;
            }
            return false;
        }
        public void GetMessageFromClient(object messgaeFromClient)
        {
            Console.WriteLine("MessgaeFromClient => " + messgaeFromClient.ToString());
        }
    }
}

