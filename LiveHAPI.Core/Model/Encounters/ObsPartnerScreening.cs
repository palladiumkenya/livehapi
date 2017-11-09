using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsPartnerScreening : Entity<Guid>, IObsPartnerScreening
    {
        public DateTime ScreeningDate { get; set; }
        public Guid IPVScreening { get; set; }
        public Guid PhysicalAssult { get; set; }
        public Guid Threatened { get; set; }
        public Guid SexuallyUncomfortable { get; set; }
        public Guid HivStatus { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime BookingDate { get; set; }
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsPartnerScreening()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsPartnerScreening(DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
        {
            ScreeningDate = screeningDate;

            IPVScreening = ipvScreening;
            PhysicalAssult = physicalAssult;
            Threatened = threatened;
            SexuallyUncomfortable = sexuallyUncomfortable;
            HivStatus = hivStatus;

            Eligibility = eligibility;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public ObsPartnerScreening(Guid id, DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
            : this(screeningDate, ipvScreening, physicalAssult, threatened, sexuallyUncomfortable, hivStatus, eligibility, bookingDate, remarks, encounterId)
        {
            Id = id;
        }

        public static ObsPartnerScreening Create(ObsPartnerScreeningInfo obsInfo)
        {
            return new ObsPartnerScreening(obsInfo.Id, obsInfo.ScreeningDate, obsInfo.IPVScreening, obsInfo.PhysicalAssult, obsInfo.Threatened, obsInfo.SexuallyUncomfortable, obsInfo.HivStatus, obsInfo.Eligibility, obsInfo.BookingDate, obsInfo.Remarks, obsInfo.EncounterId);
        }

        public static List<ObsPartnerScreening> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsPartnerScreening>();

            foreach (var obsInfo in encounterInfo.ObsPartnerScreenings)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }

    }
}
