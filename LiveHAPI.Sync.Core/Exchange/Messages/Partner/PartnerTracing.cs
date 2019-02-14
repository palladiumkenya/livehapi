using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Exchange.Partner;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Partner
{
    public class PartnerTracing : ClientMessage
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; } = new List<INTERNAL_PATIENT_ID>();
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public List<PARTNER_TRACING> TRACING { get; set; }
        
        public PartnerTracing()
        {
        }

        public PartnerTracing(MESSAGE_HEADER messageHeader, Guid clientId,List<INTERNAL_PATIENT_ID> patientIds, PLACER_DETAIL placerDetail, List<PARTNER_TRACING> tracings) : base(messageHeader, clientId)
        {
            INTERNAL_PATIENT_ID = patientIds;
            PLACER_DETAIL = placerDetail;
            TRACING = tracings;
        }
        
        public static PartnerTracing Create(PartnerClientMessage partnerClientMessage)
        {
            if(null==partnerClientMessage)
                throw new ArgumentException("message cannot be null");
            
            partnerClientMessage.ValidateTracing();
            
            return new PartnerTracing(
                partnerClientMessage.MESSAGE_HEADER,
                partnerClientMessage.ClientId,
                partnerClientMessage.CurrentClient().PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID,
                partnerClientMessage.CurrentClient().ENCOUNTER.PLACER_DETAIL,
                partnerClientMessage.CurrentClient().ENCOUNTER.TRACING);
        }
    }
}
