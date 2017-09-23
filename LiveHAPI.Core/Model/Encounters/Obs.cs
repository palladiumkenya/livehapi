using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class Obs:Entity<Guid>,IObs
    {
        public Guid QuestionId { get; set; }
        public DateTime ObsDate { get; set; }        
        public string ValueText { get; set; }
        public decimal? ValueNumeric { get; set; }
        public Guid? ValueCoded { get; set; }
        public string ValueMultiCoded { get; set; }
        public DateTime? ValueDateTime { get; set; }
        
        public Guid EncounterId { get; set; }
        
        public bool IsNull { get; set; }

        public Obs()
        {
            Id = LiveGuid.NewGuid();
            ObsDate=DateTime.Now;
        }

        private Obs(Guid id, Guid questionId, DateTime obsDate, string valueText, decimal? valueNumeric, Guid? valueCoded, string valueMultiCoded, DateTime? valueDateTime, Guid encounterId) : base(id)
        {
            QuestionId = questionId;
            ObsDate = obsDate;
            ValueText = valueText;
            ValueNumeric = valueNumeric;
            ValueCoded = valueCoded;
            ValueMultiCoded = valueMultiCoded;
            ValueDateTime = valueDateTime;
            EncounterId = encounterId;
        }

        private Obs(Guid questionId, DateTime obsDate, string valueText, decimal? valueNumeric, Guid? valueCoded,
            string valueMultiCoded, DateTime? valueDateTime, Guid encounterId)
            : this(LiveGuid.NewGuid(), questionId, obsDate, valueText, valueNumeric, valueCoded, valueMultiCoded,
                valueDateTime, encounterId)
        {

        }

        public static Obs Create(ObsInfo obsInfo)
        {
            return new Obs(obsInfo.Id, obsInfo.QuestionId, obsInfo.ObsDate, obsInfo.ValueText, obsInfo.ValueNumeric, obsInfo.ValueCoded, obsInfo.ValueMultiCoded,
                obsInfo.ValueDateTime, obsInfo.EncounterId);
        }

        public static List<Obs> Create(EncounterInfo encounterInfo)
        {
            var list = new List<Obs>();

            foreach (var obsInfo in encounterInfo.Obses)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }

        public override string ToString()
        {
            return $"{QuestionId} | {ObsDate} | {ValueText},{ValueNumeric},{ValueCoded},{ValueMultiCoded},{ValueDateTime:yyyy MMMM dd}";
        }
    }
}