using System;
using Newtonsoft.Json;

namespace ApplicationName.Core.Extensions
{
    public class ErrorResult
    {
        public ErrorResult(string message,
            int code,
            Exception exception = null)
        {
            Code = code;
            Message = message;
            Exception = exception;
        }

        [JsonIgnore]
        public Exception Exception { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}