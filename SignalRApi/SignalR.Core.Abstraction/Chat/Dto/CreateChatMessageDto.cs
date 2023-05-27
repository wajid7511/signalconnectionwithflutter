using System;
using System.Text.Json.Serialization;

namespace SignalR.Core.Abstraction.Chat.Dto
{
    public class CreateChatMessageDto
    {
        public string ChatId { get; set; } = string.Empty;
        public int SenderUserId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

