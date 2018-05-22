using System;
using Newtonsoft.Json;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public abstract class ClientMessage
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        [JsonIgnore] 
        public virtual Guid ClientId { get; set; }
    }
}