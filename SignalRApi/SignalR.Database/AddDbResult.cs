using System;
namespace SignalR.Database
{
    public class AddDbResult
    {
        public bool IsSucess { get; private set; }
        public Exception? Exception { get; private set; }
        public bool IsError => Exception is not null;
        public AddDbResult(bool isSuccess)
        {
            IsSucess = isSuccess;
        }
        public AddDbResult(Exception exception)
        {
            Exception = exception;
        }
    }
}

