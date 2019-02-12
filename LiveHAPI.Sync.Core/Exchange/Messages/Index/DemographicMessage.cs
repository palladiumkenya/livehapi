using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Index
{
    public class DemographicMessage:ClientMessage
    {
        public List<NEWAFYAMOBILECLIENT> CLIENTS { get; private set; }=new List<NEWAFYAMOBILECLIENT>();

        public DemographicMessage()
        {
        }
        
        private DemographicMessage(MESSAGE_HEADER messageHeader, Guid clientId,List<NEWAFYAMOBILECLIENT> clients) : base(messageHeader, clientId)
        {
            CLIENTS = clients;
        }

        public static DemographicMessage Create(IndexClientMessage indexClientMessage)
        {
            if(null==indexClientMessage)
                throw new ArgumentException("message cannot be null message");
            
            indexClientMessage.ValidateDemographics();
            
            return new DemographicMessage(
                indexClientMessage.MESSAGE_HEADER,
                indexClientMessage.ClientId,
                NEWAFYAMOBILECLIENT.Create(indexClientMessage.CLIENTS));
        }
        
    }
}