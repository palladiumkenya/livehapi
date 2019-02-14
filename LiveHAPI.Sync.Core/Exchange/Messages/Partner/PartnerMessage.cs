using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Messages.Index;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Familiy
{
    public class PartnerMessage:ClientMessage
    {
        public List<NEWFAMILY> CLIENTS { get; private set; }=new List<NEWFAMILY>();

        public FamilyMessage()
        {
        }

        private FamilyMessage(MESSAGE_HEADER messageHeader, Guid clientId,List<NEWFAMILY> clients) : base(messageHeader, clientId)
        {
            CLIENTS = clients;
        }

        public static FamilyMessage Create(FamilyClientMessage familyMessage)
        {
            if(null==familyMessage)
                throw new ArgumentException("message cannot be null message");

            familyMessage.ValidateDemographics();

            return new FamilyMessage(
                familyMessage.MESSAGE_HEADER,
                familyMessage.ClientId,
                NEWFAMILY.Create(familyMessage.FAMILY));
        }

    }
}
