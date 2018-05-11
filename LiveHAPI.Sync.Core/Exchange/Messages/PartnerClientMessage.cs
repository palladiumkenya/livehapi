using System.Collections.Generic;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class PartnerClientMessage
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; private set; }
        public List<NEWCLIENT> CLIENTS { get; private set; }
    }
}