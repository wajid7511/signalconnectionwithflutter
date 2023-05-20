using System;
namespace SignalR.Core.Abstraction
{
    public class ManagerProcessException : Exception
    {
        public string StatusCode { get; }
        public bool IsInternal { get; }

        /// <summary>
        /// Create the Exception
        /// Should be only in service level 
        /// </summary> 
        /// <param name="exception"></param>
        /// <param name="isInternal">will be used in middle to return the stack or not.</param>
        public ManagerProcessException(string statusCode, Exception? exception = null, bool isInternal = false) : base(exception?.Message, exception)
        {
            StatusCode = statusCode;
            IsInternal = isInternal;
        }
    }
}