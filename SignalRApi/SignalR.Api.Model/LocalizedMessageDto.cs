using System;
using System.Diagnostics.CodeAnalysis;

namespace SignalR.Api.Model
{

    public class LocalizedMessagesDto
    {
        public string StatusCode { get; }
        [NotNull]
        public string EnglishErrorMessage { get; }
        [NotNull]
        public string UdruErrorMessage { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="englishErrorMessage"></param>
        /// <param name="urduErrorMessage"></param>
        public LocalizedMessagesDto(string statusCode, string englishErrorMessage, string urduErrorMessage)
        {
            this.StatusCode = statusCode;
            this.EnglishErrorMessage = englishErrorMessage;
            this.UdruErrorMessage = urduErrorMessage;
        }
    }
}

