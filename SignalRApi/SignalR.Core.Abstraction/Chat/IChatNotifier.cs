using System;
namespace SignalR.Core.Abstraction
{
    public interface IChatNotifier
    {
        public ValueTask<bool> NotifyAll(ChatDto chatDto);
        public ValueTask<bool> NotifyOther(ChatDto chatDto);
    }
}

