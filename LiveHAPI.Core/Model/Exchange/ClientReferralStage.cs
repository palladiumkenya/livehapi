using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientReferralStage : Entity<Guid>
    {
        public string ReferredTo { get; set; }
        public string DateToBeEnrolled { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
      

        public ClientReferralStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

      

        public static ClientReferralStage Create(Encounter linkageEncounter, SubscriberSystem subscriber)
        {
            var clientStage=new ClientReferralStage();

            if (linkageEncounter.ObsLinkages.Any())
            {
                var linkage = linkageEncounter.ObsLinkages.First();
                clientStage.Id = linkage.Id;
                clientStage.ReferredTo = linkage.ReferredTo;
                clientStage.DateToBeEnrolled = linkage.DateEnrolled.ToIqDateOnly();
                clientStage.ClientId = linkageEncounter.ClientId;
            }

            return clientStage;
        }

        public override string ToString()
        {
            return $" [{ClientId} {Id}]";
        }
    }
}