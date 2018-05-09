using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientPretestStage : Entity<Guid>
    {
        public HtsEncounterType EncounterType { get; set; }
        public string EncounterDate { get; set; }
        public int? ServicePoint { get; set; }
        public int? EverTested { get; set; }
        public int? MonthsSinceLastTest { get; set; }
        public int? SelfTest12Months { get; set; }
        public int? DisabilityIndicator { get; set; }
        public int? Consent { get; set; }
        public int? TestedAs { get; set; }
        public int? Strategy { get; set; }
        public int? TbScreening { get; set; }
        public string Remarks { get; set; }
        public Guid ClientId { get; set; }
        public List<ClientPretestDisabilityStage> Disabilities { get; set; }=new List<ClientPretestDisabilityStage>();
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
      

        public ClientPretestStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static ClientPretestStage Create(Encounter obsEncounter, SubscriberSystem subscriber)
        {
            var clientStage=new ClientPretestStage();

            
           
            return clientStage;
        }

        public override string ToString()
        {
            return $" [{ClientId} {Id}]";
        }
    }
}