using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Exchange.Family;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Familiy
{
    public class FamilyTracing : ClientMessage
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; } = new List<INTERNAL_PATIENT_ID>();
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public List<FAMILY_TRACING> TRACING { get; set; }

        public FamilyTracing()
        {
        }

        public FamilyTracing(MESSAGE_HEADER messageHeader, Guid clientId,List<INTERNAL_PATIENT_ID> patientIds, PLACER_DETAIL placerDetail, List<FAMILY_TRACING> tracings) : base(messageHeader, clientId)
        {
            INTERNAL_PATIENT_ID = patientIds;
            PLACER_DETAIL = placerDetail;
            TRACING = tracings;
        }
        
        public static FamilyTracing Create(FamilyClientMessage familyClientMessage)
        {
            if(null==familyClientMessage)
                throw new ArgumentException("message cannot be null");
            
            familyClientMessage.ValidateTracing();
            
            return new FamilyTracing(
                familyClientMessage.MESSAGE_HEADER,
                familyClientMessage.ClientId,
                familyClientMessage.CurrentClient().PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID,
                familyClientMessage.CurrentClient().ENCOUNTER.PLACER_DETAIL,
                familyClientMessage.CurrentClient().ENCOUNTER.TRACING);
        }
    }
}
