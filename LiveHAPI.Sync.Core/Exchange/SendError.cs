using System;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class SendError
    {
        public Guid ClientId { get; set; }
        public Guid RecordId { get; set; }
        public string ErrorMessage { get; set; }
    }
}