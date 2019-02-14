using System;
using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Messages.Familiy;
using LiveHAPI.Sync.Core.Exchange.Partner;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Partner
{
    public class PartnerScreening:ClientMessage
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; } = new List<INTERNAL_PATIENT_ID>();
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PARTNER_SCREENING PARTNER_SCREENING { get; set; }
        
        public PartnerScreening()
        {
        }

        public PartnerScreening(MESSAGE_HEADER messageHeader, Guid clientId,List<INTERNAL_PATIENT_ID> patientIds, PLACER_DETAIL placerDetail, PARTNER_SCREENING familyScreening) : base(messageHeader, clientId)
        {
            INTERNAL_PATIENT_ID = patientIds;
            PLACER_DETAIL = placerDetail;
            PARTNER_SCREENING = familyScreening;
        }

        public static PartnerScreening Create(PartnerClientMessage partnerClientMessage)
        {
            if(null==partnerClientMessage)
                throw new ArgumentException("message cannot be null");

            partnerClientMessage.ValidatePartnerScreening();

            return new PartnerScreening(
                partnerClientMessage.MESSAGE_HEADER,
                partnerClientMessage.ClientId,
                partnerClientMessage.CurrentClient().PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID,
                partnerClientMessage.CurrentClient().ENCOUNTER.PLACER_DETAIL,
                partnerClientMessage.CurrentClient().ENCOUNTER.PARTNER_SCREENING);
        }
    }
}
