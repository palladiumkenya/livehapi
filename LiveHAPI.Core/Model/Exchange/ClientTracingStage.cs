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
        public  int? ReasonNotContacted { get; set; }
        public string ReasonNotContactedOther { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }

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
                foreach (var traceResult in tracingEncounter.ObsTraceResults)
                {
                    var clientStage = new ClientTracingStage();
                    clientStage.Id = traceResult.Id;
                    clientStage.TracingDate = traceResult.Date;
                    clientStage.TracingMode = subscriber.GetTranslation(traceResult.Mode, "TracingMode", "ObsTraceResult.Mode", "0").SafeConvert<int>();
                    clientStage.TracingOutcome = subscriber.GetTranslation(traceResult.Outcome, "TracingOutcome", "c.Outcome", "0").SafeConvert<int>();
                    clientStage.ReasonNotContacted = subscriber.GetTranslation(traceResult.ReasonNotContacted, "TracingReasonNotContacted", "ObsTraceResult.ReasonNotContacted", "0").SafeConvert<int>();
                    clientStage.ReasonNotContactedOther = traceResult.ReasonNotContactedOther;
                    clientStage.ClientId = tracingEncounter.ClientId;
                    stages.Add(clientStage);
                }
            }
            return stages;
        }

        public override string ToString()
        {
            return $"{TracingDate} |{TracingMode} |{TracingOutcome} [{ClientId}]";
        }
    }
}
