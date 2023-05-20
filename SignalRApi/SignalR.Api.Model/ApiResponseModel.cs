using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SignalR.Api.Model;
public class ApiResponseModel
{
    [JsonPropertyName("isSuccess")]
    [NotNull]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("statusCode")]
    [NotNull]
    public int StatusCode { get; set; }

    [JsonPropertyName("message")]
    [MaybeNull]
    public string Message { get; set; }

    [JsonPropertyName("data")]
    [MaybeNull]
    public object? Data { get; set; }

    [JsonPropertyName("error")]
    [MaybeNull]
    public ApiResponseErrorModel? Error { get; set; }

    [JsonPropertyName("totalPages")]
    [NotNull]
    public int TotalPages { get; set; }

    public ApiResponseModel(int statusCode, string messageKey, object data, int id = 0, int totalPage = 0)
    {
        var messageDto = LocalizedErrorCodes.GetErrorMessage(messageKey);
        this.StatusCode = statusCode;
        this.Message = messageDto.EnglishErrorMessage;
        this.Data = data;
        this.IsSuccess = true;
        this.TotalPages = totalPage;
    }

    public ApiResponseModel(int htppStatusCode, bool isSuccess, string messageKey, string message = "", string? stackTrace = null, int id = 0, int totalPage = 0)
    {
        var messageDto = LocalizedErrorCodes.GetErrorMessage(messageKey);
        this.IsSuccess = isSuccess;
        this.StatusCode = htppStatusCode;
        this.Error = new ApiResponseErrorModel(messageKey, messageDto.EnglishErrorMessage, messageDto.UdruErrorMessage);
        this.TotalPages = totalPage;
        this.Message = string.IsNullOrEmpty(message) ? messageDto.EnglishErrorMessage : message;
    }
}


