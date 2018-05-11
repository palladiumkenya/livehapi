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
    public class ClientFamilyTracingStage : Entity<Guid>
    {
        public DateTime TracingDate { get; set; }
        public DateTime? ReminderDate { get; set; }
        public int TracingMode { get; set; }
        public int TracingOutcome { get; set; }
        public int Consent { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
      
        public ClientFamilyTracingStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static List <ClientFamilyTracingStage> Create(Encounter tracingEncounter, SubscriberSystem subscriber)
        {
            var stages=new List<ClientFamilyTracingStage>();

            if (tracingEncounter.ObsFamilyTraceResults.Any())
            {
                foreach (var traceResult in tracingEncounter.ObsFamilyTraceResults)
                {
                    var tracingStage = new ClientFamilyTracingStage();

                    tracingStage.Id = traceResult.Id;
                    tracingStage.HtsTestType = GetTestType(traceResult.TestName);
                    tracingStage.KitType = subscriber.GetTranslation(traceResult.Kit, "HIVTestKits", "ObsTestResult.Kit", "0").SafeConvert<int>();
                    tracingStage.KitOther = traceResult.KitOther;
                    tracingStage.LotNumber = traceResult.LotNumber;
                    tracingStage.ExpiryDate = traceResult.Expiry.ToIqDateOnly();
                    tracingStage.Result = subscriber.GetTranslation(traceResult.Kit, "HIVResults", "ObsTestResult.Result", "0").SafeConvert<int>();
                    tracingStage.TestRound = (int)tracingStage.HtsTestType;
                    tracingStage.ClientId = testingEncounter.ClientId;

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