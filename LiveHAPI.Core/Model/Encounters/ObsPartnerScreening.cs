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
        public Guid? PnsAccepted { get; set; }
        public Guid IPVScreening { get; set; }
        public Guid PhysicalAssult { get; set; }
        public Guid Threatened { get; set; }
        public Guid SexuallyUncomfortable { get; set; }
        public Guid? IPVOutcome { get; set; }
        public string Occupation { get; set; }
        public Guid? PNSRealtionship { get; set; }

        public Guid? LivingWithClient { get; set; }
      
        public Guid HivStatus { get; set; }
        public Guid? PNSApproach { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime? BookingDate { get; set; }
        public string Remarks { get; set; }
        public string PhoneNumber { get; set; }
        public Guid IndexClientId { get; set; }
        public Guid EncounterId { get; set; }

        public bool BookingMet { get; set; }
        public DateTime? DateBookingMet { get; set; }
        public Guid? TraceId { get; set; }

        public ObsPartnerScreening()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsPartnerScreening(DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened,
            Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime? bookingDate, string remarks, string phoneNumber,
            Guid encounterId, Guid? pnsAccepted, Guid? iPVOutcome, string occupation, Guid? pNSRealtionship,
            Guid? livingWithClient, Guid? pNSApproach,Guid indexClientId)
        {
            ScreeningDate = screeningDate;
            PnsAccepted = pnsAccepted;
            IPVScreening = ipvScreening;
            PhysicalAssult = physicalAssult;
            Threatened = threatened;
            SexuallyUncomfortable = sexuallyUncomfortable;
            IPVOutcome = iPVOutcome;
            Occupation = occupation;
            PNSRealtionship = pNSRealtionship;

            LivingWithClient = livingWithClient;



            PNSApproach = pNSApproach;

        HivStatus = hivStatus;

            Eligibility = eligibility;
            BookingDate = bookingDate;
            Remarks = remarks;
            PhoneNumber = PhoneNumber;
            EncounterId = encounterId;
            IndexClientId = indexClientId;
        }

        public ObsPartnerScreening(Guid id, DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime? bookingDate, string remarks, string phoneNumber, Guid encounterId,
        Guid? pnsAccepted, Guid? iPVOutcome, string occupation, Guid? pNSRealtionship,
        Guid? livingWithClient, Guid? pNSApproach,Guid indexClientId
        )
            : this(screeningDate, ipvScreening, physicalAssult, threatened, sexuallyUncomfortable, hivStatus, eligibility, bookingDate, remarks, phoneNumber,encounterId, pnsAccepted, iPVOutcome, occupation, pNSRealtionship, livingWithClient, pNSApproach,indexClientId)
        {
            Id = id;
        }

        public static ObsPartnerScreening Create(ObsPartnerScreeningInfo obsInfo)
        {
            return new ObsPartnerScreening(obsInfo.Id, obsInfo.ScreeningDate, obsInfo.IPVScreening, obsInfo.PhysicalAssult, obsInfo.Threatened, obsInfo.SexuallyUncomfortable, obsInfo.HivStatus, obsInfo.Eligibility, obsInfo.BookingDate, obsInfo.Remarks,obsInfo.PhoneNumber, obsInfo.EncounterId,
            obsInfo.PnsAccepted, obsInfo.IPVOutcome, obsInfo.Occupation, obsInfo.PNSRealtionship, obsInfo.LivingWithClient, obsInfo.PNSApproach,obsInfo.IndexClientId
            );
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
