using SignalR.Core.Abstraction;
using SignalRApi.Core;

namespace SignalR.Core;
public class ChatManager : IChatManager
{
    private readonly NotifyHub _notifyHub;
    public ChatManager(NotifyHub notifyHub)
    {
        _notifyHub = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
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

