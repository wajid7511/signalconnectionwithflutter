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
        private readonly IChatManager _chatNotifier;
        public ChatController(IChatManager notifyHub)
        {
            _chatNotifier = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> NotifyClient([FromBody] ChatNotifyPostModel chatNotifyPostModel)
        {
            await _chatNotifier.NotifyAllAsync(new ChatDto()
            {
                Message = chatNotifyPostModel.Message
            });

            return ApiResponseFactory.CreateSuccessResponse(LocalizedConsts.SUSSCESS, true);
        }
        [HttpPost("NotifyOthers")]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> NotifyOtherClient([FromBody] ChatNotifyPostModel chatNotifyPostModel)
        {
            await _chatNotifier.NotifyOtherAsync(new ChatDto()
            {
                Message = chatNotifyPostModel.Message,
                ClientId = chatNotifyPostModel.ClientId
            });

            return ApiResponseFactory.CreateSuccessResponse(LocalizedConsts.SUSSCESS, true);
        }
    }
}

