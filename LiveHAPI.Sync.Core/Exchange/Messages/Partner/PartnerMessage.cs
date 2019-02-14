using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Partner;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Partner
{
    public class PartnerMessage:ClientMessage
    {
        public List<NEWPARTNER> PARTNERS { get; private set; }=new List<NEWPARTNER>();

        public PartnerMessage()
        {
        }

        private PartnerMessage(MESSAGE_HEADER messageHeader, Guid clientId,List<NEWPARTNER> clients) : base(messageHeader, clientId)
        {
            PARTNERS = clients;
        }

        public static PartnerMessage Create(PartnerClientMessage partnerMessage)
        {
            if(null==partnerMessage)
                throw new ArgumentException("message cannot be null message");

            partnerMessage.ValidateDemographics();

            return new PartnerMessage(
                partnerMessage.MESSAGE_HEADER,
                partnerMessage.ClientId,
                NEWPARTNER.Create(partnerMessage.PARTNERS));
        }

    }
}
