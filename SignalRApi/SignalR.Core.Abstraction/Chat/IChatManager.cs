using System;

namespace SignalR.Core.Abstraction
{
    public interface IChatManager
    {
        public ValueTask<bool> NotifyAllAsync(ChatDto chatDto);
        public ValueTask<bool> NotifyOtherAsync(ChatDto chatDto);
    }
}

