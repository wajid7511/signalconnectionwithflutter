using SignalR.Core.Abstraction;
using SignalR.Core.Abstraction.Chat.Dto;
using SignalR.Database;
using SignalR.Database.DAL;
using SignalR.Database.Models;
using SignalR.Shared;
using SignalRApi.Core;

namespace SignalR.Core;
public class ChatManager : IChatManager
{
    private readonly NotifyHub _notifyHub;
    private readonly ChatDAL _chatDAL;
    public ChatManager(NotifyHub notifyHub, ChatDAL chatDAL)
    {
        _notifyHub = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
        _chatDAL = chatDAL ?? throw new ArgumentNullException(nameof(chatDAL));
    }

    public async ValueTask<bool> CreateChatAsync(CreateChatDto createChatDto)
    {

        var result = await _chatDAL.CreateChatAsync(new Chat()
        {
            ChatId = Helper.GetId(),
            ReceiverUserId = createChatDto.ReceiverUserId,
            SenderUserId = createChatDto.SenderUserId
        });
        if (result.IsError)
        {
            throw new ManagerProcessException(ErrorKeys.UnableToCreateChat, result.Exception, result.IsError);
        }
        return result.IsSucess;
    }

    public async ValueTask<bool> CreateChatMessageAsync(CreateChatMessageDto createChatMessageDto)
    {
        var result = await _chatDAL.CreateChatMessageAsync(createChatMessageDto.ChatId, new ChatMessage()
        {
            ChatMessageId = Helper.GetId(),
            Message = createChatMessageDto.Message,
            SenderUserId = createChatMessageDto.SenderUserId,
        });
        if (result.IsError)
        {
            throw new ManagerProcessException(ErrorKeys.UnableToCreateChatMessage, result.Exception, result.IsError);
        }
        return result.IsSucess;
    }

    public async ValueTask<bool> NotifyAllAsync(ChatDto chatDto)
    {
        return await _notifyHub.NotifyClients(chatDto.Message);
    }

    public async ValueTask<bool> NotifyOtherAsync(ChatDto chatDto)
    {
        return await _notifyHub.NotifyOtherClients(chatDto);
    }
}

