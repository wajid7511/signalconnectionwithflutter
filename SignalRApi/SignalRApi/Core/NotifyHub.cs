using System;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Core
{
    public class NotifyHub : Hub
    {
        private readonly IHubClients _contexts;
        public NotifyHub(IHubClients contexts)
        {
            _contexts = contexts ?? throw new ArgumentNullException(nameof(contexts));
        }
        public async ValueTask<bool> NotifyClient()
        {
            await _contexts.Clients.All.SendAsync("NotifyClient", "This is Asp.Net Core connections");
            return true;
        }
    }
}

