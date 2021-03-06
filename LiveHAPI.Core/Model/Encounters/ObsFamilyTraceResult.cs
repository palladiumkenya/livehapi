﻿using System;
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
        public Guid? Consent { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid IndexClientId { get; set; }
        public Guid EncounterId { get; set; }


        public Guid? ReasonNotContacted { get; set; }
        public string ReasonNotContactedOther { get; set; }

        public ObsFamilyTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsFamilyTraceResult(Guid id,DateTime date, Guid mode, Guid outcome, Guid? consent, DateTime? reminder, DateTime? bookingDate,Guid encounterId,Guid indexClientId,Guid? reasonnotcontacted,string reasonnotcontactedother) : this()
        {
            Id = id;
            Date = date;
            Mode = mode;
            Outcome = outcome;
            Consent = consent;
            Reminder = reminder;
            BookingDate = bookingDate;
            EncounterId = encounterId;
            IndexClientId = indexClientId;
            ReasonNotContacted = reasonnotcontacted;
            ReasonNotContactedOther = reasonnotcontactedother;
        }

        public static ObsFamilyTraceResult Create(ObsFamilyTraceResultInfo obsInfo)
        {
            return new ObsFamilyTraceResult(obsInfo.Id, obsInfo.Date, obsInfo.Mode, obsInfo.Outcome, obsInfo.Consent, obsInfo.Reminder, obsInfo.BookingDate,obsInfo.EncounterId,obsInfo.IndexClientId,obsInfo.ReasonNotContacted,obsInfo.ReasonNotContactedOther);
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
