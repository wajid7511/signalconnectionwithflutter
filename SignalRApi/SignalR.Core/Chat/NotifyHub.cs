using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SignalR.Core.Abstraction;

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
                await Clients.All.SendAsync("NotifyClients", message);
                return true;
            }
            return false;
        }
        public async ValueTask<bool> NotifyOtherClients(ChatDto chatDto)
        {
            if (Clients != null)
            {
                IReadOnlyList<string> excludedClientIds = new List<string>() { chatDto.ClientId ?? string.Empty };

                await Clients.AllExcept(excludedClientIds).SendAsync("NotifyOtherClients", JsonConvert.SerializeObject(chatDto, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
                return true;
            }
            return false;
        }
        public void NewMessageToServer(Object? messageFromClient)
        {
            if (messageFromClient == null)
            {
                Console.WriteLine("Message is null");
            }
            else
            {
                Console.WriteLine("MessgaeFromClient => " + messageFromClient.ToString());
            }
        }
    }
}

