using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Messages.Index;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Familiy
{
    public class FamilyMessage:ClientMessage
    {
        public List<NEWFAMILY> FAMILY { get; private set; }=new List<NEWFAMILY>();

        public FamilyMessage()
        {
        }

        private FamilyMessage(MESSAGE_HEADER messageHeader, Guid clientId,List<NEWFAMILY> clients) : base(messageHeader, clientId)
        {
            FAMILY = clients;
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
