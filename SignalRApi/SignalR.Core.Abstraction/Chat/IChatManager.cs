using System;
using SignalR.Core.Abstraction.Chat.Dto;

namespace SignalR.Core.Abstraction
{
    public interface IChatManager
    {
        public ValueTask<bool> NotifyAllAsync(ChatDto chatDto);
        public ValueTask<bool> NotifyOtherAsync(ChatDto chatDto);
        public ValueTask<bool> CreateChatAsync(CreateChatDto createChatDto);
        public ValueTask<bool> CreateChatMessageAsync(CreateChatMessageDto createChatMessageDto);
    }
}

