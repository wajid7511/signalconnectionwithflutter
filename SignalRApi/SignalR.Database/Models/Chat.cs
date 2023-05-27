using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace SignalR.Database.Models
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string ChatId { get; set; } = null!;

        [BsonElement("senderUserId")]
        public int SenderUserId { get; set; }

        [BsonElement("receiverUserId")]
        public int ReceiverUserId { get; set; }

        [BsonElement("messages")]
        public List<ChatMessage> Messages { get; set; } = new();
    }
}

