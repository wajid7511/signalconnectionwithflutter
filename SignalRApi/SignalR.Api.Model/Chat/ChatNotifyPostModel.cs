using System;
using System.ComponentModel;

namespace SignalR.Api.Model.Chat
{
    [DisplayName("Notify Model")]
    public class ChatNotifyPostModel
    {
        public string Message { get; set; } = string.Empty;
    }
}

