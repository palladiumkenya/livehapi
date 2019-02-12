using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using LiveHAPI.Sync.Core.Exchange.Messages.Index;
using Newtonsoft.Json;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class IndexClientMessage:ClientMessage
    {
        public List<NEWCLIENT> CLIENTS { get; private set; }=new List<NEWCLIENT>();
        public NEWCLIENT CurrentClient() => CLIENTS.FirstOrDefault();
        public IndexClientMessage(MESSAGE_HEADER messageHeader, List<NEWCLIENT> clients,Guid clientId)
        {
            ClientId = clientId;
            MESSAGE_HEADER = messageHeader;
            CLIENTS = clients;
        }

        public DemographicMessage GetDemographicMessage()
        {
            return DemographicMessage.Create(this);
        }
        public void ValidateDemographics()
        {
            if(!CLIENTS.Any())
                throw new ArgumentException("no clients found");
              
            var client = CLIENTS.First();
            
            if(null==client.PATIENT_IDENTIFICATION)
                throw new ArgumentException("missing patient Identification");
            
            if(!client.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Any())
                throw new ArgumentException("client has no identification");

        }
        
        public void ValidateEncounter()
        {
            ValidateDemographics();
            
            if(null==CurrentClient().ENCOUNTER)
                throw new ArgumentException("missing Encounter");
            
            if(null==CurrentClient().ENCOUNTER.PLACER_DETAIL)
                throw new ArgumentException("missing Encounter user");
        }

        public void ValidatePretest()
        {
            ValidateEncounter();

            if (null == CurrentClient().ENCOUNTER.PRE_TEST)
                throw new ArgumentException("missing pretest Encounter");

        }

        public void ValidateTests()
        {
            ValidateEncounter();

            if (null == CurrentClient().ENCOUNTER.HIV_TESTS)
                throw new ArgumentException("missing tests Encounter");
        }

        public void ValidateReferral()
        {
            ValidateEncounter();

            if (null == CurrentClient().ENCOUNTER.REFERRAL)
                throw new ArgumentException("missing referral Encounter");

        }
        
        public void ValidateLinkage()
        {
            ValidateEncounter();

            if (null == CurrentClient().ENCOUNTER.LINKAGE)
                throw new ArgumentException("missing linkage Encounter");

        }
        
        public void ValidateTracing()
        {
            ValidateEncounter();

            if (!CurrentClient().ENCOUNTER.TRACING.Any())
                throw new ArgumentException("missing Tracing Encounter");

        }
    }
}