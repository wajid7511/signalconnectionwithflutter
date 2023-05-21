using SignalR.Core.Abstraction;
using SignalRApi.Core;

namespace SignalR.Core;
public class ChatNotifier : IChatNotifier
{
    private readonly NotifyHub _notifyHub;
    public ChatNotifier(NotifyHub notifyHub)
    {
        _notifyHub = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
    }
    public async ValueTask<bool> NotifyAll(ChatDto chatDto)
    {
        return await _notifyHub.NotifyClients(chatDto.Message);
    }

    public async ValueTask<bool> NotifyOther(ChatDto chatDto)
    {
        return await _notifyHub.NotifyOtherClients(chatDto);
    }
}

