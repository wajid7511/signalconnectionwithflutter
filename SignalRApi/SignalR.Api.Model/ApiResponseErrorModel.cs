using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SignalR.Api.Model
{
    [DisplayName("Generic Response of each API")]
    public class ApiResponseErrorModel
    {
        [JsonPropertyName("errorCode")]
        [NotNull]
        public string ErrorCode { get; }

        [JsonPropertyName("englishMessage")]
        [NotNull]
        public string EnglishMessage { get; }

        [JsonPropertyName("urduMessage")]
        public string? UrduMessage { get; set; }

        [JsonPropertyName("stackTrace")]
        public string? StackTrace { get; set; }

        public ApiResponseErrorModel(string errorCode, string englishMessage, string urduMessage, string? stackTrace = null)
        {
            ErrorCode = errorCode;
            EnglishMessage = englishMessage;
            UrduMessage = urduMessage;
            StackTrace = stackTrace;
        }
    }
}

