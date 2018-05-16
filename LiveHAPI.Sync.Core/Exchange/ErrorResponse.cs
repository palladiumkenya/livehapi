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
    }
}