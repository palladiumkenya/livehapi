using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;
using System.Collections.Generic;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsTraceResult : Entity<Guid>, IObsTraceResult
    {
        public DateTime Date { get; set; }
        
        public Guid Mode { get; set; }
        
        public Guid Outcome { get; set; }
        public Guid EncounterId { get; set; }

        private ObsTraceResult(Guid id,DateTime date, Guid mode, Guid outcome, Guid encounterId)
        {
            Id = id;
            Date = date;
            Mode = mode;
            Outcome = outcome;
            EncounterId = encounterId;
        }

        public ObsTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }
        public static ObsTraceResult Create(ObsTraceResultInfo obsInfo)
        {
            return new ObsTraceResult(obsInfo.Id, obsInfo.Date,obsInfo.Mode,obsInfo.Outcome,obsInfo.EncounterId);
        }

        public static List<ObsTraceResult> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsTraceResult>();

            foreach (var obsInfo in encounterInfo.ObsTraceResults)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }
    }
}