using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SignalR.Api.Model.Chat
{
    [DisplayName("Notify Model")]
    public class ChatNotifyPostModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("clientId")]
        public string? ClientId { get; set; }
    }
}

