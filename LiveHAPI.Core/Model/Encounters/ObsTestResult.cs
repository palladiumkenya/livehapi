using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsTestResult : Entity<Guid>, IObsTestResult
    {
        [MaxLength(50)]
        public string TestName { get; set; }
        public int Attempt {get; set; }
        
        public Guid Kit { get; set; }
        [MaxLength(50)]
        public string KitDisplay { get; set; }
        [MaxLength(50)]
        public string KitOther { get; set; }
        [MaxLength(50)]
        public string LotNumber { get; set; }
        public DateTime Expiry { get; set; }
        
        public Guid Result { get; set; }
        [MaxLength(50)]
        public string ResultCode { get; set; }

        [MaxLength(50)]
        public string ResultDisplay { get; set; }
        public bool IsValid { get; set; }
        
        public Guid EncounterId { get; set; }

        public ObsTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

        private ObsTestResult(Guid id, string testName, int attempt, Guid kit, string kitDisplay, string kitOther, string lotNumber, DateTime expiry, Guid result, string resultCode, string resultDisplay, Guid encounterId)
        {
            Id = id;
            TestName = testName;
            Attempt = attempt;
            Kit = kit;
            KitDisplay = kitDisplay;
            KitOther = kitOther;
            LotNumber = lotNumber;
            Expiry = expiry;
            Result = result;
            ResultCode = resultCode;
            ResultDisplay = resultDisplay;
            EncounterId = encounterId;
        }

        public static ObsTestResult Create(ObsTestResultInfo obsInfo)
        {
            return new ObsTestResult(obsInfo.Id, obsInfo.TestName, obsInfo.Attempt, obsInfo.Kit, obsInfo.KitDisplay, obsInfo.KitOther, obsInfo.LotNumber,
                obsInfo.Expiry, obsInfo.Result,obsInfo.ResultCode,obsInfo.ResultDisplay,obsInfo.EncounterId);
        }

        public static List<ObsTestResult> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsTestResult>();

            foreach (var obsInfo in encounterInfo.ObsTestResults)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }

    }
}