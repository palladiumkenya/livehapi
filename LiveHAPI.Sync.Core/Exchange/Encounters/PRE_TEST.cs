using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class PRE_TEST
    {
        public int ENCOUNTER_TYPE { get; set; }
        public string ENCOUNTER_DATE { get; set; }
        public int SERVICE_POINT { get; set; }
        public int EVER_TESTED { get; set; }
        public int MONTHS_SINCE_LAST_TEST { get; set; }
        public int SELF_TEST_12_MONTHS { get; set; }
        public int DISABILITY_INDICATOR { get; set; }
        public List<int> DISABILITIES { get; set; }=new List<int>();
        public int CONSENT { get; set; }
        public int TESTED_AS { get; set; }
        public int STRATEGY { get; set; }
        public int TB_SCREENING { get; set; }
        public string REMARKS { get; set; }

        public PRE_TEST()
        {
        }

        private PRE_TEST(int encounterType, string encounterDate, int servicePoint, int everTested, int monthsSinceLastTest, int selfTest12Months, int disabilityIndicator, List<int> disabilities, int consent, int testedAs, int strategy, int tbScreening, string remarks)
        {
            ENCOUNTER_TYPE = encounterType;
            ENCOUNTER_DATE = encounterDate;
            SERVICE_POINT = servicePoint;
            EVER_TESTED = everTested;
            MONTHS_SINCE_LAST_TEST = monthsSinceLastTest;
            SELF_TEST_12_MONTHS = selfTest12Months;
            DISABILITY_INDICATOR = disabilityIndicator;
            DISABILITIES = disabilities;
            CONSENT = consent;
            TESTED_AS = testedAs;
            STRATEGY = strategy;
            TB_SCREENING = tbScreening;
            REMARKS = remarks;
        }

        public static PRE_TEST Create(ClientPretestStage clientPretestStage)
        {
         return new PRE_TEST(
             (int)clientPretestStage.EncounterType,
             clientPretestStage.EncounterDate.ToIqDateOnly(),
             clientPretestStage.ServicePoint.Value,
             clientPretestStage.EverTested.Value,
             (int)clientPretestStage.MonthsSinceLastTest.Value,
             clientPretestStage.SelfTest12Months.Value,
             clientPretestStage.DisabilityIndicator.Value,
             clientPretestStage.IqDisabilities,
             clientPretestStage.Consent.Value,
             clientPretestStage.TestedAs.Value,
             clientPretestStage.Strategy.Value,
             clientPretestStage.TbScreening.Value,
             clientPretestStage.Remarks
             );
        }

        public static PRE_TEST Create(int encounterType, DateTime encounterDate, int servicePoint, int everTested, int monthsSinceLastTest, int selfTest12Months, int disabilityIndicator, List<int> disabilities, int consent, int testedAs, int strategy, int tbScreening, string remarks)
        {
            return new PRE_TEST(encounterType, encounterDate.ToIqDateOnly(), servicePoint,everTested,monthsSinceLastTest,selfTest12Months,disabilityIndicator,disabilities,consent,testedAs,strategy,tbScreening,remarks);
        }
    }
}