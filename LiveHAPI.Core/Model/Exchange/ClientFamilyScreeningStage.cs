using System;
using System.Linq;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientFamilyScreeningStage : Entity<Guid>
    {
        public string ScreeningDate { get; set; }
        public int HivStatus { get; set; }
        public int EligibleForHts { get; set; }
        public DateTime? BookingDate { get; set; }
        public string Remarks { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }

        public ClientFamilyScreeningStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate = DateTime.Now;
        }

        public static ClientFamilyScreeningStage Create(Encounter encounter, SubscriberSystem subscriber)
        {
            var clientStage = new ClientFamilyScreeningStage();

            if (encounter.ObsMemberScreenings.Any())
            {
                var screening = encounter.ObsMemberScreenings.First();
                clientStage.Id = screening.Id;
                clientStage.HivStatus = subscriber.GetTranslation(screening.HivStatus, "ScreeningHivStatus", "ObsMemberScreening.HivStatus", "0").SafeConvert<int>();
                clientStage.EligibleForHts = subscriber.GetTranslation(screening.Eligibility, "YesNo", "ObsMemberScreening.Eligibility", "0").SafeConvert<int>();
                clientStage.BookingDate = screening.BookingDate;
                clientStage.Remarks = screening.Remarks;
                clientStage.ClientId = encounter.ClientId;
            }

            return clientStage;
        }
        public override string ToString()
        {
            return $"{ClientId}";
        }
    }
}