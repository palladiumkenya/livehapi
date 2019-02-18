using System;
using Newtonsoft.Json;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public abstract class ClientMessage
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        [JsonIgnore] 
        public virtual Guid ClientId { get; set; }

        protected ClientMessage()
        {
        }

        protected ClientMessage(MESSAGE_HEADER messageHeader, Guid clientId)
        {
            MESSAGE_HEADER = messageHeader;
            ClientId = clientId;
        }
    }
}