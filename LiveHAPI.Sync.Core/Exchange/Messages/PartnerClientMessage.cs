using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Messages.Partner;
using LiveHAPI.Sync.Core.Exchange.Partner;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class PartnerClientMessage:ClientMessage
    {
        public List<PARTNER> PARTNERS { get; set; }
        public PARTNER CurrentClient() => PARTNERS.FirstOrDefault();

        public PartnerClientMessage()
        {
        }

        public PartnerClientMessage(MESSAGE_HEADER messageHeader, List<PARTNER> partners, Guid clientId)
        {
            MESSAGE_HEADER = messageHeader;
            PARTNERS = partners;
            ClientId = clientId;
        }

        public PartnerMessage GetDemographicMessage()
        {
            return PartnerMessage.Create(this);
        }

        public PartnerScreening GetScreeningMessage()
        {
            return PartnerScreening.Create(this);
        }

        public PartnerTracing GetTracingMessage()
        {
            return PartnerTracing.Create(this);
        }

        public void ValidateDemographics()
        {
            if (!PARTNERS.Any())
                throw new ArgumentException("no clients found");

            var partner = PARTNERS.First();

            if (null == partner.PATIENT_IDENTIFICATION)
                throw new ArgumentException("missing patient Identification");

            if (!partner.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Any())
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
        public void ValidatePartnerScreening()
        {
            ValidateEncounter();

            if (null == CurrentClient().ENCOUNTER.PARTNER_SCREENING)
                throw new ArgumentException("missing screening Encounter");
        }

        public void ValidateTracing()
        {
            ValidateEncounter();

            if (!CurrentClient().ENCOUNTER.TRACING.Any())
                throw new ArgumentException("missing Tracing Encounter");
        }


    }
}
