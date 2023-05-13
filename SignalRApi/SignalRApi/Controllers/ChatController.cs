using System;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.Core;

namespace SignalRApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly NotifyHub _notifyHub;
        public ChatController(NotifyHub notifyHub)
        {
            _notifyHub = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
        }
        [HttpPost]
        public async ValueTask<bool> NotifyClient()
        {
            await _notifyHub.NotifyClient();
            return true;
        }
        [HttpGet]
        public ValueTask<string> IsActive()
        {
            return ValueTask.FromResult("ChatHub");
        }
    }
}

