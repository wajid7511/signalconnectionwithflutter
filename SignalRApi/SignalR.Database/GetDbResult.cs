using System;
namespace SignalR.Database
{
    public class GetDbResult<T> where T : class
    {
        public bool IsSucess { get; private set; }
        public Exception? Exception { get; private set; }
        public bool IsError => Exception is not null;
        public T? Data { get; private set; }
        public GetDbResult(T? data)
        {
            IsSucess = data != null;
            Data = data;
        }
        public GetDbResult(Exception exception)
        {
            Exception = exception;
        }
    }
}

