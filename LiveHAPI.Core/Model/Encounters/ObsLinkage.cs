using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsLinkage : Entity<Guid>, IObsLinkage
    {
        [MaxLength(50)]
        public string ReferredTo { get; set; }

        public DateTime? DatePromised { get; set; }
        [MaxLength(50)]
        public string FacilityHandedTo { get; set; }
        [MaxLength(50)]
        public string HandedTo { get; set; }
        [MaxLength(50)]
        public string WorkerCarde { get; set; }
        public DateTime? ARTStartDate { get; set; }
        public DateTime? DateEnrolled { get; set; }
        [MaxLength(50)]
        public string EnrollmentId { get; set; }
        [MaxLength(50)]
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsLinkage()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsLinkage(Guid id, string referredTo, DateTime? datePromised, string facilityHandedTo, string handedTo,
            string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId,
            DateTime? artStartDate)
        {
            Id = id;
            ReferredTo = referredTo;
            DatePromised = datePromised;
            FacilityHandedTo = facilityHandedTo;
            HandedTo = handedTo;
            WorkerCarde = workerCarde;
            DateEnrolled = dateEnrolled;
            EnrollmentId = enrollmentId;
            Remarks = remarks;
            EncounterId = encounterId;
            ARTStartDate = artStartDate;
        }


        public static ObsLinkage Create(ObsLinkageInfo obsInfo)
        {
            return new ObsLinkage(obsInfo.Id, obsInfo.ReferredTo ,obsInfo.DatePromised, obsInfo.FacilityHandedTo, obsInfo.HandedTo, obsInfo.WorkerCarde, obsInfo.DateEnrolled, obsInfo.EnrollmentId,
                obsInfo.Remarks, obsInfo.EncounterId,obsInfo.ARTStartDate);
        }

        public static List<ObsLinkage> Create(EncounterInfo encounterInfo)
        {
            var list = new List<ObsLinkage>();

            foreach (var obsInfo in encounterInfo.ObsLinkages)
            {
                list.Add(Create(obsInfo));
            }
            return list;
        }

    }
}