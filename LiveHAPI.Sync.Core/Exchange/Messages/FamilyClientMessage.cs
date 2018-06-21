using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Family;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class FamilyClientMessage:ClientMessage
    {
        public List<FAMILY> FAMILY { get; set; }

        public FamilyClientMessage()
        {
        }

        public FamilyClientMessage(MESSAGE_HEADER messageHeader, List<FAMILY> family, Guid clientId)
        {
            MESSAGE_HEADER = messageHeader;
            FAMILY = family;
            ClientId = clientId;
        }
    }
}