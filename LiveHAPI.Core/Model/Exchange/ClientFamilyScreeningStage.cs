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

        public static List<ClientFamilyScreeningStage> Create(Encounter encounter, SubscriberSystem subscriber)
        {
            var clientStages = new List<ClientFamilyScreeningStage>();

            if (encounter.ObsMemberScreenings.Any())
            {
                foreach (var screening in encounter.ObsMemberScreenings)
                {
                    var clientStage = new ClientFamilyScreeningStage();
                    clientStage.Id = screening.Id;
                    clientStage.HivStatus = subscriber.GetTranslation(screening.HivStatus, "ScreeningHivStatus", "ObsMemberScreening.HivStatus", "0").SafeConvert<int>();
                    clientStage.EligibleForHts = subscriber.GetTranslation(screening.Eligibility, "YesNo", "ObsMemberScreening.Eligibility", "0").SafeConvert<int>();
                    clientStage.BookingDate = screening.BookingDate;
                    clientStage.Remarks = screening.Remarks;
                    clientStage.ClientId = encounter.ClientId;
                    clientStages.Add(clientStage);
                }
              
            }

            return clientStages;
        }
        public override string ToString()
        {
            return $"{ClientId}";
        }
    }
}