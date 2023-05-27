using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SignalR.Api.Model.Chat
{
    [DisplayName("Create Chat Post Model")]
    public class CreateChatPostModel
    {
        [JsonPropertyName("senderUserId")]
        public int SenderUserId { get; set; }

        [JsonPropertyName("receiverUserId")]
        public int ReceiverUserId { get; set; }
    }
}

