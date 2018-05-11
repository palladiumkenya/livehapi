using System.Collections.Generic;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class IndexClientMessage
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; private set; }
        public List<NEWCLIENT> CLIENTS { get; private set; }

        public IndexClientMessage(MESSAGE_HEADER messageHeader, List<NEWCLIENT> clients)
        {
            MESSAGE_HEADER = messageHeader;
            CLIENTS = clients;
        }
    }
}