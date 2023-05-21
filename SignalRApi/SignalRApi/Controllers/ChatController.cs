using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SignalR.Api.Model;
using SignalR.Api.Model.Chat;
using SignalR.Core.Abstraction;
using SignalRApi.Factory;

namespace SignalRApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatNotifier _chatNotifier;
        public ChatController(IChatNotifier notifyHub)
        {
            _chatNotifier = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> NotifyClient([FromBody] ChatNotifyPostModel chatNotifyPostModel)
        {
            await _chatNotifier.NotifyAll(new ChatDto()
            {
                Message = chatNotifyPostModel.Message
            });

            return ApiResponseFactory.CreateSuccessResponse(LocalizedConsts.SUSSCESS, true);
        }
        [HttpGet]
        public ValueTask<string> IsActive()
        {
            return ValueTask.FromResult("ChatHub");
        }
    }
}

