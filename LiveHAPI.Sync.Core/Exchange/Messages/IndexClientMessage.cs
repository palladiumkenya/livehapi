using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class IndexClientMessage:ClientMessage
    {
        public List<NEWCLIENT> CLIENTS { get; private set; }
        
        public IndexClientMessage(MESSAGE_HEADER messageHeader, List<NEWCLIENT> clients,Guid clientId)
        {
            ClientId = clientId;
            MESSAGE_HEADER = messageHeader;
            CLIENTS = clients;
        }
    }
}