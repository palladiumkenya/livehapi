using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsFamilyTraceResult : Entity<Guid>, IObsFamilyTraceResult
    {
        public DateTime Date { get; set; }
        public Guid Mode { get; set; }
        public string ModeDisplay { get; set; }
        public Guid Outcome { get; set; }
        public string OutcomeDisplay { get; set; }
        public Guid EncounterId { get; set; }

        public ObsFamilyTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsFamilyTraceResult(Guid Id,DateTime date, Guid mode, Guid outcome, Guid encounterId) : this()
        {
            Id = Id;
            Date = date;
            Mode = mode;
            Outcome = outcome;
            EncounterId = encounterId;
        }

        public static ObsFamilyTraceResult Create(ObsFamilyTraceResultInfo obsInfo)
        {
            return new ObsFamilyTraceResult(obsInfo.Id, obsInfo.Date, obsInfo.Mode, obsInfo.Outcome, obsInfo.EncounterId);
        }

        public static List<ObsFamilyTraceResult> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsFamilyTraceResult>();

            foreach (var obsInfo in encounterInfo.ObsFamilyTraceResults)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }
    }
}
