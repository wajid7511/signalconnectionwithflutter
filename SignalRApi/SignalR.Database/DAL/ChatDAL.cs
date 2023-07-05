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
        private readonly IMongoCollection<ChatMessage> _chatMessagesCollection = null!;
        public ChatDAL(IOptions<MongoDatabaseOptions> chatDatabaseOptions, ILogger<ChatDAL>? logger) : base(chatDatabaseOptions, logger)
        {
            _chatCollection = base.CreateOrGetCollection<Chat>(chatDatabaseOptions.Value.ChatCollectionName) ?? throw new NullReferenceException(nameof(_chatCollection));
            _chatMessagesCollection = base.CreateOrGetCollection<ChatMessage>(chatDatabaseOptions.Value.ChatMessageCollectionName) ?? throw new NullReferenceException(nameof(_chatMessagesCollection));
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
        public async ValueTask<AddDbResult> InsertChatMessageAsync(ChatMessage chatMessage)
        {
            try
            {
                await _chatMessagesCollection.InsertOneAsync(new ChatMessage()
                {
                    ChatMessageId = chatMessage.ChatMessageId,
                    ChatId = chatMessage.ChatId,
                    Message = chatMessage.Message,
                    SenderUserId = chatMessage.SenderUserId
                });
                return new AddDbResult(true);

            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to add new Chat message to chat exception {0}", ex);

                return new AddDbResult(ex);
            }
        }
        public async ValueTask<GetDbResult<Chat>> GetChatAsync(string chatId, int senderUserId)
        {
            try
            {
                var result = await _chatCollection.Find(f => f.ChatId == chatId && (f.SenderUserId == senderUserId || f.ReceiverUserId == senderUserId)).FirstOrDefaultAsync();
                if (result == null)
                {
                    return new GetDbResult<Chat>(new Exception($"Unable to find chat with {nameof(chatId)} and {nameof(senderUserId)}")); ;
                }
                return new GetDbResult<Chat>(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to get chat with chatId {0} senderUserId {1} exception {2}", chatId, senderUserId, ex);

                return new GetDbResult<Chat>(ex);
            }
        }
    }
}

