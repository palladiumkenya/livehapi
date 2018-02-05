using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsMemberScreening : Entity<Guid>, IObsMemberScreening
    {
        public DateTime ScreeningDate { get; set; }
        //TODO: Use FamilyHIVStatus lookup
        public Guid HivStatus { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime BookingDate { get; set; }

        //TODO: NII Remarks
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }
        public bool BookingMet { get; set; }
        public DateTime? DateBookingMet { get; set; }
        public Guid? TraceId { get; set; }
      
        
        public ObsMemberScreening(Guid id, DateTime screeningDate, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
        {
            Id = id;
            ScreeningDate = screeningDate;
            HivStatus = hivStatus;
            Eligibility = eligibility;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public ObsMemberScreening()
        {
          
        }

        public static ObsMemberScreening Create(Guid id, DateTime screeningDate, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
        {
            var obs = new ObsMemberScreening(id, screeningDate, hivStatus, eligibility, bookingDate, remarks, encounterId);
            return obs;
        }

        public static ObsMemberScreening Create(ObsMemberScreeningInfo encounterInfo)
        {
          return  new ObsMemberScreening(encounterInfo.Id, encounterInfo.ScreeningDate, encounterInfo.HivStatus, encounterInfo.Eligibility, encounterInfo.BookingDate, encounterInfo.Remarks, encounterInfo.EncounterId);
        }
        public static List<ObsMemberScreening> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsMemberScreening>();

            foreach (var obsInfo in encounterInfo.ObsMemberScreenings)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }
    }
}