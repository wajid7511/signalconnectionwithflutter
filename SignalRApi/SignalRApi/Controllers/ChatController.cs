using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SignalR.Api.Model;
using SignalR.Core.Abstraction;
using SignalRApi.Core;
using SignalRApi.Factory;

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
        [ProducesResponseType(typeof(ApiResponseModel), 200)]
        public async ValueTask<ApiResponseModel> NotifyClient()
        {
            await _notifyHub.NotifyClient("from other Mobile");

            return ApiResponseFactory.CreateSuccessResponse(LocalizedConsts.SUSSCESS, true);
        }
        [HttpGet]
        public ValueTask<string> IsActive()
        {
            return ValueTask.FromResult("ChatHub");
        }
    }
}

