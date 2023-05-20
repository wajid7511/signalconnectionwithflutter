using System;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Core
{
    public class NotifyHub : Hub
    {
        public NotifyHub()
        {
        }
        public async ValueTask<bool> NotifyClient(string messag)
        {
            if (Clients != null)
            {
                await Clients.All.SendAsync("NotifyClient", $"This is Asp.Net Core connections {messag}");
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

