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
    public class ClientLinkageStage : Entity<Guid>
    {
        public string Facility { get; set; }
        public string HealthWorker { get; set; }
        public string Carde { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string CccNumber { get; set; }
        public string Remarks { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }


        public ClientLinkageStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

      

        public static ClientLinkageStage Create(Encounter linkageEncounter, SubscriberSystem subscriber)
        {
            var clientStage=new ClientLinkageStage();

            if (linkageEncounter.ObsLinkages.Any())
            {
                var linkage = linkageEncounter.ObsLinkages.First();
                clientStage.Id = linkage.Id;
                clientStage.Facility = linkage.FacilityHandedTo;
                clientStage.HealthWorker = linkage.HandedTo;
                clientStage.Carde = linkage.WorkerCarde;
                clientStage.DateEnrolled = linkage.DateEnrolled;
                clientStage.CccNumber = linkage.EnrollmentId;
                clientStage.Remarks = linkage.Remarks;
                clientStage.ClientId = linkageEncounter.ClientId;
            }

            return clientStage;
        }

        public override string ToString()
        {
            return $"{Facility} {HealthWorker} [{ClientId}]";
        }
    }
}