using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SignalR.Database.Models;

namespace SignalR.Database.DAL
{
    public class ChatDAL : MongoContext<ChatDAL>
    {
        private readonly IMongoCollection<Chat> _chatCollection = null!;
        public readonly ILogger<ChatDAL>? _logger = null!;
        public ChatDAL(IOptions<MongoDatabaseOptions> chatDatabaseOptions, ILogger<ChatDAL>? logger) : base(chatDatabaseOptions, logger)
        {
            _chatCollection = base.CreateOrGetCollection<Chat>(chatDatabaseOptions.Value.ChatCollectionName) ?? throw new NullReferenceException(nameof(_chatCollection));

            _logger = logger;
        }
        public async ValueTask<AddDbResult> CreateChatAsync(Chat chat)
        {
            try
            {
                if (chat.ReceiverUserId == chat.SenderUserId)
                {
                    throw new ArgumentException($"{nameof(chat.ReceiverUserId)} and {nameof(chat.SenderUserId)} should not be be same");
                }
                var result = await _chatCollection.FindAsync(
                    f => (f.ReceiverUserId == chat.ReceiverUserId || f.ReceiverUserId == chat.SenderUserId)
                    && (f.SenderUserId == chat.ReceiverUserId || f.SenderUserId == chat.SenderUserId));
                if (result.Any())
                {
                    return new AddDbResult(true);
                }
                await _chatCollection.InsertOneAsync(chat);
                return new AddDbResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to add new Chat message to chat exception {0}", ex);

                return new AddDbResult(ex);
            }
        }
        public async ValueTask<AddDbResult> CreateChatMessageAsync(string chatId, ChatMessage chatMessage)
        {
            try
            {
                var chat = _chatCollection.Find(f => f.ChatId == chatId && (f.SenderUserId == chatMessage.SenderUserId || f.ReceiverUserId == chatMessage.SenderUserId)).FirstOrDefault();

                if (chat != null)
                {
                    chat.Messages.Add(chatMessage);
                    var filter = Builders<Chat>.Filter.Eq(s => s.ChatId, chatId);
                    var update = Builders<Chat>.Update.Set(s => s.Messages, chat.Messages);
                    var updateResult = await _chatCollection.UpdateOneAsync(filter, update);
                    if (updateResult is not null && updateResult.IsAcknowledged)
                    {
                        return new AddDbResult(true);
                    }

                }
                return new AddDbResult(new Exception($"Invalid {nameof(chatId)} or {nameof(chatMessage.SenderUserId)}"));
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to add new Chat message to chat exception {0}", ex);

                return new AddDbResult(ex);
            }
        }
    }
}

