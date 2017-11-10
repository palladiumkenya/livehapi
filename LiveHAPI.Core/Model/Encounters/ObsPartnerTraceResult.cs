﻿using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsPartnerTraceResult : Entity<Guid>, IObsPartnerTraceResult
    {
        public DateTime Date { get; set; }
        public Guid Mode { get; set; }
        public string ModeDisplay { get; set; }
        public Guid Outcome { get; set; }
        public string OutcomeDisplay { get; set; }
        public Guid EncounterId { get; set; }

        public ObsPartnerTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsPartnerTraceResult(Guid id,DateTime date, Guid mode, Guid outcome, Guid encounterId ) : this()
        {
            Id = id;
            Date = date;
            Mode = mode;
            Outcome = outcome;
            EncounterId = encounterId;
        }

        public static ObsPartnerTraceResult Create(Guid id, DateTime date, Guid mode, Guid outcome, Guid encounterId)
        {
            var obs = new ObsPartnerTraceResult(id,date, mode, outcome, encounterId);
            return obs;
        }
        public static ObsPartnerTraceResult Create(ObsPartnerTraceResultInfo obsInfo)
        {
            return new ObsPartnerTraceResult(obsInfo.Id, obsInfo.Date, obsInfo.Mode, obsInfo.Outcome, obsInfo.EncounterId);
        }

        public static List<ObsPartnerTraceResult> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsPartnerTraceResult>();

            foreach (var obsInfo in encounterInfo.ObsPartnerTraceResults)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }



    }
}