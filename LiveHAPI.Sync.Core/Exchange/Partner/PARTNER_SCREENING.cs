using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Exchange.Family;

namespace LiveHAPI.Sync.Core.Exchange.Partner
{
    public class PARTNER_SCREENING
    {
        public int PNS_ACCEPTED { get; set; }

        public string SCREENING_DATE { get; set; }

        public int IPV_SCREENING_DONE { get; set; }

        public int HURT_BY_PARTNER { get; set; }

        public int THREAT_BY_PARTNER { get; set; }

        public int SEXUAL_ABUSE_BY_PARTNER { get; set; }

        public int IPV_OUTCOME { get; set; }

        public string PARTNER_OCCUPATION { get; set; }

        public int PARTNER_RELATIONSHIP { get; set; }

        public int LIVING_WITH_CLIENT { get; set; }

        public int HIV_STATUS { get; set; }

        public int PNS_APPROACH { get; set; }

        public int ELIGIBLE_FOR_HTS { get; set; }

        public string BOOKING_DATE { get; set; }

        public PARTNER_SCREENING()
        {
        }

        public PARTNER_SCREENING(int pnsAccepted, string screeningDate, int ipvScreeningDone, int hurtByPartner, int threatByPartner, int sexualAbuseByPartner, int ipvOutcome, string partnerOccupation, int partnerRelationship, int livingWithClient, int hivStatus, int pnsApproach, int eligibleForHts, string bookingDate)
        {
            PNS_ACCEPTED = pnsAccepted;
            SCREENING_DATE = screeningDate;
            IPV_SCREENING_DONE = ipvScreeningDone;
            HURT_BY_PARTNER = hurtByPartner;
            THREAT_BY_PARTNER = threatByPartner;
            SEXUAL_ABUSE_BY_PARTNER = sexualAbuseByPartner;
            IPV_OUTCOME = ipvOutcome;
            PARTNER_OCCUPATION = partnerOccupation;
            PARTNER_RELATIONSHIP = partnerRelationship;
            LIVING_WITH_CLIENT = livingWithClient;
            HIV_STATUS = hivStatus;
            PNS_APPROACH = pnsApproach;
            ELIGIBLE_FOR_HTS = eligibleForHts;
            BOOKING_DATE = bookingDate;
        }

        public static PARTNER_SCREENING Create(ClientPartnerScreeningStage clientTracingStage)
        {
            return new PARTNER_SCREENING(
                clientTracingStage.PnsAccepted,
                clientTracingStage.ScreeningDate.ToIqDateOnly(),
                clientTracingStage.IpvScreeningDone.Value,
                clientTracingStage.HurtByPartner.Value,
                clientTracingStage.ThreatByPartner.Value,
                clientTracingStage.SexualAbuseByPartner.Value,
                clientTracingStage.IpvOutcome.Value,
                clientTracingStage.PartnerOccupation,
                clientTracingStage.PartnerRelationship.Value,
                clientTracingStage.LivingWithClient.Value,
                clientTracingStage.HivStatus.Value,
                clientTracingStage.PnsApproach.Value,
                clientTracingStage.EligibleForHts.Value,
                clientTracingStage.BookingDate.ToIqDateOnly());
        }

        public static List<PARTNER_SCREENING> Create(List<ClientPartnerScreeningStage> stage)
        {
            var list = new List<PARTNER_SCREENING>();
            foreach (var clientTracingStage in stage)
            {
                list.Add(Create(clientTracingStage));
            }
            return list;
        }
    }
}