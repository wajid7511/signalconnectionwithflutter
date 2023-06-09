﻿using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SignalR.Database.Models
{
    public class ChatMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string ChatMessageId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("chatId")]
        public string ChatId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("senderUserId")]
        public int SenderUserId { get; set; }

        [BsonRequired]
        [BsonElement("message")]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.String)]
        public DateTimeOffset CreatedDateTime = DateTimeOffset.UtcNow;
    }
}

