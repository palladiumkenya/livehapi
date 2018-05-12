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
    public class ClientPartnerTracingStage : Entity<Guid>
    {

        public DateTime TracingDate { get; set; }

        public int TracingMode { get; set; }

        public int TracingOutcome { get; set; }

        public int Consent { get; set; }

        public DateTime? BookingDate { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }

        public ClientPartnerTracingStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate = DateTime.Now;
        }

        public static List<ClientPartnerTracingStage> Create(Encounter tracingEncounter, SubscriberSystem subscriber)
        {
            var stages = new List<ClientPartnerTracingStage>();

            if (tracingEncounter.ObsPartnerTraceResults.Any())
            {
                foreach (var traceResult in tracingEncounter.ObsPartnerTraceResults)
                {
                    var tracingStage = new ClientPartnerTracingStage();

                    tracingStage.Id = traceResult.Id;
                    tracingStage.TracingDate = traceResult.Date;
                    tracingStage.TracingMode = subscriber.GetTranslation(traceResult.Mode, "TracingMode", "ObsPartnerTraceResult.Mode", "0").SafeConvert<int>();
                    tracingStage.TracingOutcome = subscriber.GetTranslation(traceResult.Outcome, "PnsTracingOutcome", "ObsPartnerTraceResult.Outcomee", "0").SafeConvert<int>();
                    tracingStage.Consent = subscriber.GetTranslation(traceResult.Consent, "YesNo", "ObsPartnerTraceResult.Consent", "0").SafeConvert<int>();
                    tracingStage.BookingDate = traceResult.BookingDate;
                    tracingStage.ClientId = tracingEncounter.ClientId;
                    stages.Add(tracingStage);
                }
            }

            return stages;
        }

        public override string ToString()
        {
            return $" [{ClientId} {Id}]";
        }
    }
}