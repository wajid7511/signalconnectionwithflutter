using System;
using System.Text.Json.Serialization;

namespace SignalR.Core.Abstraction
{
    public class ChatDto
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("clientId")]
        public string? ClientId { get; set; }
        [JsonPropertyName("dateTimeOffset")]
        public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.UtcNow;
    }
}

