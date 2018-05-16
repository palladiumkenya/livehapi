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
    public class ClientTestingStage : Entity<Guid>
    {
        public Guid PretestEncounterId { get; set; }
        public HtsTestType HtsTestType { get; set; }
        public int KitType { get; set; }
        public string KitOther { get; set; }
        public string LotNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int Result { get; set; }
        public int TestRound { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }

        public ClientTestingStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static List <ClientTestingStage> Create(Encounter testingEncounter, SubscriberSystem subscriber, Guid? pretestEncounterId)
        {
            var clientTestingStages=new List<ClientTestingStage>();

            if (testingEncounter.ObsTestResults.Any())
            {
                foreach (var testResult in testingEncounter.ObsTestResults)
                {
                    var clientTestingStage = new ClientTestingStage();
                    clientTestingStage.UserId = subscriber.GetEmrUserId(testingEncounter.UserId);
                    clientTestingStage.Id = testResult.Id;
                    clientTestingStage.PretestEncounterId =
                        pretestEncounterId.IsNullOrEmpty() ? testResult.Id : pretestEncounterId.Value;
                    clientTestingStage.HtsTestType = GetTestType(testResult.TestName);
                    clientTestingStage.KitType = subscriber.GetTranslation(testResult.Kit, "HIVTestKits", "ObsTestResult.Kit", "0").SafeConvert<int>();
                    clientTestingStage.KitOther = testResult.KitOther;
                    clientTestingStage.LotNumber = testResult.LotNumber;
                    clientTestingStage.ExpiryDate = testResult.Expiry.ToIqDateOnly();
                    clientTestingStage.Result = subscriber.GetTranslation(testResult.Kit, "HIVResults", "ObsTestResult.Result", "0").SafeConvert<int>();
                    clientTestingStage.TestRound = (int)clientTestingStage.HtsTestType;
                    clientTestingStage.ClientId = testingEncounter.ClientId;

                    clientTestingStages.Add(clientTestingStage);
                }
               
            }
            return clientTestingStages;
        }

        private  static HtsTestType GetTestType(string name)
        {
            return name.IsSameAs("HIV Test 1") ? HtsTestType.Screening : HtsTestType.Confrimatory;
        }
        public override string ToString()
        {
            return $"{KitType} | {LotNumber} | {Result} [{ClientId}]";
        }
    }
}