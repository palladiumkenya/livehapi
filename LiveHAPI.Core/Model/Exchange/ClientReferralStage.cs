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
        public DateTime? DateToBeEnrolled { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }


        public ClientReferralStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static List<ClientReferralStage> Create(Encounter linkageEncounter, SubscriberSystem subscriber)
        {
            var clientStages=new List<ClientReferralStage>();
            if (linkageEncounter.ObsLinkages.Any())
            {
                foreach (var linkage in linkageEncounter.ObsLinkages)
                {
                    var clientStage = new ClientReferralStage();
                    clientStage.Id = linkage.Id;
                    clientStage.ReferredTo = linkage.ReferredTo;
                    clientStage.DateToBeEnrolled = linkage.DatePromised;
                    clientStage.ClientId = linkageEncounter.ClientId;
                    clientStages.Add(clientStage);
                }
            }
            return clientStages;
        }

        public override string ToString()
        {
            return $" {ReferredTo} | {DateToBeEnrolled} | {ClientId}";
        }
    }
}