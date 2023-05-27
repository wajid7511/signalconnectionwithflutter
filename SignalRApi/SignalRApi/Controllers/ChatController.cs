using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SignalR.Api.Model;
using SignalR.Api.Model.Chat;
using SignalR.Core.Abstraction;
using SignalR.Core.Abstraction.Chat.Dto;
using SignalR.Shared;
using SignalRApi.Factory;

namespace SignalRApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatManager _chatManager;
        public ChatController(IChatManager notifyHub)
        {
            _chatManager = notifyHub ?? throw new ArgumentNullException(nameof(notifyHub));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> NotifyClient([FromBody] ChatNotifyPostModel chatNotifyPostModel)
        {
            await _chatManager.NotifyAllAsync(new ChatDto()
            {
                Message = chatNotifyPostModel.Message
            });

            return ApiResponseFactory.CreateSuccessResponse(ErrorKeys.Success, true);
        }
        [HttpPost("NotifyOthers")]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> NotifyOtherClient([FromBody] ChatNotifyPostModel chatNotifyPostModel)
        {
            await _chatManager.NotifyOtherAsync(new ChatDto()
            {
                Message = chatNotifyPostModel.Message,
                ClientId = chatNotifyPostModel.ClientId
            });

            return ApiResponseFactory.CreateSuccessResponse(ErrorKeys.Success, true);
        }
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> CreateChat([FromBody] CreateChatPostModel createChatPostModel)
        {
            await _chatManager.CreateChatAsync(new CreateChatDto()
            {
                SenderUserId = createChatPostModel.SenderUserId,
                ReceiverUserId = createChatPostModel.ReceiverUserId
            });

            return ApiResponseFactory.CreateSuccessResponse(ErrorKeys.Success, true);
        }

        [HttpPost("CreateMessage")]
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> CreateChat([StringLength(24, MinimumLength = 24)][FromQuery] string chatId, CreateChatMessagePostModel createChatMessagePostModel)
        {
            await _chatManager.CreateChatMessageAsync(new CreateChatMessageDto()
            {
                ChatId = chatId,
                SenderUserId = createChatMessagePostModel.SenderUserId,
                Message = createChatMessagePostModel.Message
            });

            return ApiResponseFactory.CreateSuccessResponse(ErrorKeys.Success, true);
        }
    }
}

