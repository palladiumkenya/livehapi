using System;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            try
            {
                return $"{Message} (Code:{Code})";
            }
            catch 
            {
                return string.Empty;
            }
        }
    }
}