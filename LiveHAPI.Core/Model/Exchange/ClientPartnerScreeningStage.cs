using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientPartnerScreeningStage : Entity<Guid>
    {
        public int PnsAccepted { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int? IpvScreeningDone { get; set; }
        public int? HurtByPartner { get; set; }
        public int? ThreatByPartner { get; set; }
        public int? SexualAbuseByPartner { get; set; }
        public int? IpvOutcome { get; set; }
        public string PartnerOccupation { get; set; }
        public int? PartnerRelationship { get; set; }
        public int? LivingWithClient { get; set; }
        public int? HivStatus { get; set; }
        public int? PnsApproach { get; set; }
        public int? EligibleForHts { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }

        public ClientPartnerScreeningStage()
        {
            Id = LiveGuid.NewGuid();
            SyncStatus = SyncStatus.Staged;
            StatusDate = DateTime.Now;
        }
        public static List<ClientPartnerScreeningStage> Create(Encounter encounter, SubscriberSystem subscriber)
        {
            var clientStages = new List<ClientPartnerScreeningStage>();

            if (encounter.ObsPartnerScreenings.Any())
            {
                foreach (var screening in encounter.ObsPartnerScreenings)
                {
                    var clientStage = new ClientPartnerScreeningStage();
                    clientStage.Id = screening.Id;
                    clientStage.ScreeningDate = screening.ScreeningDate;
                    clientStage.PnsAccepted = subscriber.GetTranslation(screening.PnsAccepted, "YesNo", "ObsPartnerScreening.PnsAccepted", "0").SafeConvert<int>();
                    clientStage.IpvScreeningDone = subscriber.GetTranslation(screening.IPVScreening, "YesNoNA", "ObsPartnerScreening.IPVScreening", "0").SafeConvert<int>();
                    clientStage.HurtByPartner = subscriber.GetTranslation(screening.PhysicalAssult, "YesNo", "ObsPartnerScreening.PhysicalAssult", "0").SafeConvert<int>();
                    clientStage.ThreatByPartner = subscriber.GetTranslation(screening.Threatened, "YesNo", "ObsPartnerScreening.Threatened", "0").SafeConvert<int>();
                    clientStage.SexualAbuseByPartner = subscriber.GetTranslation(screening.SexuallyUncomfortable, "YesNo", "ObsPartnerScreening.SexuallyUncomfortable", "0").SafeConvert<int>();
                    clientStage.IpvOutcome = subscriber.GetTranslation(screening.IPVOutcome, "IpvOutcome", "ObsPartnerScreening.IPVOutcome", "0").SafeConvert<int>();
                    clientStage.PartnerOccupation = screening.Occupation;
                    clientStage.PartnerRelationship = subscriber.GetTranslation(screening.PNSRealtionship, "PNSRelationship", "ObsPartnerScreening.PNSRealtionship", "0").SafeConvert<int>();
                    clientStage.LivingWithClient = subscriber.GetTranslation(screening.LivingWithClient, "YesNoDeclined", "ObsPartnerScreening.LivingWithClient", "0").SafeConvert<int>();
                    clientStage.HivStatus = subscriber.GetTranslation(screening.HivStatus, "HivStatus", "ObsPartnerScreening.HivStatus", "0").SafeConvert<int>();
                    clientStage.EligibleForHts = subscriber.GetTranslation(screening.Eligibility, "YesNo", "ObsPartnerScreening.Eligibility", "0").SafeConvert<int>();
                    clientStage.PnsApproach = subscriber.GetTranslation(screening.PNSApproach, "PnsApproach", "ObsPartnerScreening.PNSApproach", "0").SafeConvert<int>();
                    clientStage.BookingDate = screening.BookingDate;
                    clientStage.ClientId = encounter.ClientId;
                    clientStages.Add(clientStage);
                }

            }

            return clientStages;
        }
        public override string ToString()
        {
            return $"{ClientId} ";
        }
    }
}