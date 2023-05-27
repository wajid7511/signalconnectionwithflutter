using System;
using System.Text.Json.Serialization;

namespace SignalR.Core.Abstraction.Chat.Dto
{
    public class CreateChatDto
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
    }
}

