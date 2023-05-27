using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SignalR.Api.Model.Chat
{
    public class CreateChatMessagePostModel
    {
        [Required]
        [JsonPropertyName("senderUserId")]
        public int SenderUserId { get; set; }
        [Required]
        [MaxLength(1000)]
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}

