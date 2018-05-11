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
    public class ClientTracingStage : Entity<Guid>
    {
        public DateTime TracingDate { get; set; }
        public int TracingMode { get; set; }
        public int TracingOutcome { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
      
        public ClientTracingStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static List <ClientTracingStage> Create(Encounter tracingEncounter, SubscriberSystem subscriber)
        {
            var stages=new List<ClientTracingStage>();

            if (tracingEncounter.ObsTraceResults.Any())
            {
                var clientStage = new ClientTracingStage();
                var traceResult = tracingEncounter.ObsTraceResults.First();
                clientStage.Id = traceResult.Id;
                clientStage.TracingDate = traceResult.Date;
                clientStage.TracingMode = subscriber.GetTranslation(traceResult.Mode, "TracingMode", "ObsTraceResult.Mode", "0").SafeConvert<int>();
                clientStage.TracingOutcome = subscriber.GetTranslation(traceResult.Outcome, "TracingOutcome", "ObsTraceResult.Outcome","0").SafeConvert<int>();
                clientStage.ClientId = tracingEncounter.ClientId;
                stages.Add(clientStage);
            }

            return stages;
        }

        public override string ToString()
        {
            return $"{TracingDate} |{TracingMode} |{TracingOutcome} [{ClientId}]";
        }
    }
}