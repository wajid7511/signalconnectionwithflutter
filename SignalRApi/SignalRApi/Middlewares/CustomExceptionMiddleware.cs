using System;
using System.Text.Json;
using SignalR.Api.Model;
using SignalR.Core.Abstraction;
using SignalR.Shared;
using SignalRApi.Factory;

namespace SignalRApi.Middlewares
{
    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //Continue processing
                if (_next != null)
                    await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                //Log the exception if you want 
                if (!context.Response.HasStarted)
                {
                    ApiResponseModel? response;

                    if (ex is ManagerProcessException managerProcessException)
                    {
                        response = ApiResponseFactory.CreateManageProcessErrorResponse(managerProcessException.StatusCode,
                             ex.Message, stackTrace: managerProcessException.IsInternal ? null : ex.StackTrace);
                    }
                    else
                    {
                        response = ApiResponseFactory.CreateErrorResponse(ErrorKeys.InternalError, ex.Message, stackTrace: ex.StackTrace);
                    }
                    var json = JsonSerializer.Serialize(response);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;

                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}

