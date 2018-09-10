using System;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsLinkageInfo : IObsLinkage
    {
        public Guid Id { get; set; }


        public DateTime? DateEnrolled { get; set; }
        public DateTime? DatePromised { get; set; }
        public Guid EncounterId { get; set; }
        public string EnrollmentId { get; set; }
        public string FacilityHandedTo { get; set; }
        public string HandedTo { get; set; }
        public DateTime? ARTStartDate { get; set; }
        public string ReferredTo { get; set; }
        public string Remarks { get; set; }
        public string WorkerCarde { get; set; }
    }
}