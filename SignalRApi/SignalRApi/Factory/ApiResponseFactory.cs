using System;
using SignalR.Api.Model;

namespace SignalRApi.Factory
{
    public static class ApiResponseFactory
    {
        public static ApiResponseModel CreateSuccessResponse(string messageKey, object data, int id = 0, int totalPage = 0)
        {
            return new ApiResponseModel(200, messageKey, data, id, totalPage);
        }
        public static ApiResponseModel CreateManageProcessErrorResponse(string messageKey, string message = "", string? stackTrace = null, int id = 0, int totalPage = 0)
        {
            return new ApiResponseModel(200, false, messageKey, message, stackTrace: stackTrace, id: id, totalPage: totalPage);
        }
        public static ApiResponseModel CreateErrorResponse(string messageKey, string message = "", string? stackTrace = null, int id = 0, int totalPage = 0)
        {
            return new ApiResponseModel(500, false, messageKey, message, stackTrace: stackTrace, id: id, totalPage: totalPage);
        }
        public static ApiResponseModel CreateBadRequestResponse(string messageKey, string message = "", string? stackTrace = null, int id = 0, int totalPage = 0)
        {
            return new ApiResponseModel(400, false, messageKey, message, stackTrace: stackTrace, id: id, totalPage: totalPage);
        }
    }
}

