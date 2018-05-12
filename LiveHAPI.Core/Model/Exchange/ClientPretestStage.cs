using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientPretestStage : Entity<Guid>
    {
        public HtsEncounterType EncounterType { get; set; }
        public DateTime EncounterDate { get; set; }
        public int? ServicePoint { get; set; }
        public int? EverTested { get; set; }
        public decimal? MonthsSinceLastTest { get; set; }
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
        public int UserId { get; set; }

        [NotMapped]
        public List<int> IqDisabilities
        {
            get
            {
                if (Disabilities.Any())
                    return Disabilities.Select(x => x.Disabilities).ToList();
                return new List<int>();
            }
        }

        public ClientPretestStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static ClientPretestStage Create(HtsEncounterType encounterType, Encounter obsEncounter, SubscriberSystem subscriber)
        {
            var clientStage=new ClientPretestStage();

            if (obsEncounter.Obses.Any())
            {             
                var obses = obsEncounter.Obses.ToList();
                clientStage.Id = obsEncounter.Id;
                clientStage.EncounterDate = obsEncounter.EncounterDate;
                clientStage.EncounterType = encounterType;
                clientStage.TestedAs = GetObsValue(obses, "TestedAs", subscriber, "B260401E-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.TbScreening = GetObsValue(obses, "TbScreening", subscriber, "B2605F54-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.Remarks = GetObsValue(obses, "B260665C-852F-11E7-BB31-BE2E44B06B34");
                clientStage.DisabilityIndicator = GetObsValue(obses, "YesNo", subscriber, "B260695C-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.Disabilities = GetDisabilityStages(obses, "B2603C5E-852F-11E7-BB31-BE2E44B06B34", subscriber, clientStage.Id);
                clientStage.EverTested = GetObsValue(obses, "YesNo", subscriber, "B2603772-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.SelfTest12Months = GetObsValue(obses, "YesNo", subscriber, "B2603773-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.Strategy = GetObsValue(obses, "Strategy", subscriber, "B260417C-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.ServicePoint = GetObsValue(obses, "HTSEntryPoints", subscriber, "B26039A1-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.MonthsSinceLastTest = GetObsValue(obses, "B26039A2-852F-11E7-BB31-BE2E44B06B34", true,"0").SafeConvert<decimal>();
                clientStage.Consent = GetObsValue(obses, "YesNo", subscriber, "B2603DC6-852F-11E7-BB31-BE2E44B06B34").SafeConvert<int>();
                clientStage.ClientId = obsEncounter.ClientId;
            }

            return clientStage;
        }

        private static string GetObsValue(List<Obs> obses,string field, SubscriberSystem subscriber,string question,bool coded=true)
        {
            var obs = obses.FirstOrDefault(x => x.QuestionId == new Guid(question));
            if (null != obs && coded)
            {
                return subscriber.GetTranslation(obs.ValueCoded, field, obs.QuestionId.ToString(), "0");
            }
            return string.Empty;
        }

        private static string GetObsValue(List<Obs> obses, string question, bool numeric = false,
            string defaultValue = "")
        {
            var obs = obses.FirstOrDefault(x => x.QuestionId == new Guid(question));
            if (null != obs)
            {
                if (numeric)
                    return obs.ValueNumeric.ToString();

                return obs.ValueText;
            }

            return defaultValue;
        }

        private static List<ClientPretestDisabilityStage> GetDisabilityStages(List<Obs> obses, string question,SubscriberSystem subscriber,Guid id)
        {
            var list=new List<ClientPretestDisabilityStage>();
            var obs = obses.FirstOrDefault(x => x.QuestionId == new Guid(question));
            if (null != obs)
            {
                var codes = obs.ValueMultiCoded.Split(',');
                foreach (var code in codes)
                {
                    var codeId = subscriber.GetTranslation(code, "Disabilities", question, "0").SafeConvert<int>();
                    list.Add(new ClientPretestDisabilityStage(codeId,id));
                }
            }
            return list;
        }


        public override string ToString()
        {
            return $" [{ClientId} {Id}]";
        }
    }
}