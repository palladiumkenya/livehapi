using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Partner;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class PartnerClientMessage:ClientMessage
    {
        public List<PARTNER> PARTNERS { get; set; }

        public PartnerClientMessage()
        {
        }

        public PartnerClientMessage(MESSAGE_HEADER messageHeader, List<PARTNER> partners)
        {
            MESSAGE_HEADER = messageHeader;
            PARTNERS = partners;
        }
    }
}