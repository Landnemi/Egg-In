using System;
using Newtonsoft.Json;

namespace FuglariApi.Infrastructure
{
    public class ErrorCodeException : Exception
    {
        public int statusCode;
        public string errorCode;
        public string details;
        public ErrorCodeException(int code, string err, string det) : base()
        {
            statusCode = code;
            errorCode = err;
            details = det;

        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(new { errorCode = errorCode, details = details });
        }

    }
}