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
    public class ClientFinalTestStage : Entity<Guid>
    {
        public int ScreeningResult { get; set; }
        public int ConfirmatoryResult { get; set; }
        public int FinalResult { get; set; }
        public int FinalResultGiven { get; set; }
        public int CoupleDiscordant { get; set; }
        public int PnsAccepted { get; set; }
        public int PnsDeclineReason { get; set; }
        public string Remarks { get; set; }
        public Guid ClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }

        public ClientFinalTestStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static List <ClientFinalTestStage> Create(Encounter testingEncounter, SubscriberSystem subscriber)
        {
            var stages=new List<ClientFinalTestStage>();

            if (testingEncounter.ObsFinalTestResults.Any())
            {
                var clientStage = new ClientFinalTestStage();
                var testResult = testingEncounter.ObsFinalTestResults.First();
                clientStage.Id = testResult.Id;
                clientStage.ScreeningResult = subscriber.GetTranslation(testResult.FirstTestResult, "HIVResults", "ObsFinalTestResult.FirstTestResult", "0").SafeConvert<int>();
                clientStage.ConfirmatoryResult = subscriber.GetTranslation(testResult.SecondTestResult, "HIVResults", "ObsFinalTestResult.SecondTestResult", "0").SafeConvert<int>();
                clientStage.FinalResult = subscriber.GetTranslation(testResult.FinalResult, "HIVFinalResults", "ObsFinalTestResult.FinalResult", "0").SafeConvert<int>();
                clientStage.FinalResultGiven = subscriber.GetTranslation(testResult.ResultGiven, "YesNo", "ObsFinalTestResult.ResultGiven", "0").SafeConvert<int>();
                clientStage.CoupleDiscordant = subscriber.GetTranslation(testResult.CoupleDiscordant, "YesNoNA", "ObsFinalTestResult.CoupleDiscordant", "0").SafeConvert<int>();
                clientStage.PnsAccepted = subscriber.GetTranslation(testResult.SelfTestOption, "YesNo", "ObsFinalTestResult.SelfTestOptions", "0").SafeConvert<int>();
                clientStage.PnsDeclineReason = subscriber.GetTranslation(testResult.PnsDeclined, "ReasonsPartner", "ObsFinalTestResult.PnsDeclineds", "0").SafeConvert<int>();
                clientStage.Remarks = testResult.Remarks;
                clientStage.ClientId = testingEncounter.ClientId;
                stages.Add(clientStage);
            }
            return stages;
        }

        private  static HtsTestType GetTestType(string name)
        {
            return name.IsSameAs("HIV Test 1") ? HtsTestType.Screening : HtsTestType.Confrimatory;
        }
        public override string ToString()
        {
            return $"{ScreeningResult} |{ConfirmatoryResult}|{FinalResult} | {ClientId}";
        }
    }
}