using System;

namespace LiveHAPI.Core.Model.Encounters
{
    public interface IObsLinkage
    {
        DateTime? DateEnrolled { get; set; }
        DateTime? DatePromised { get; set; }
        Guid EncounterId { get; set; }
        string EnrollmentId { get; set; }
        string FacilityHandedTo { get; set; }
        string HandedTo { get; set; }
        string ReferredTo { get; set; }
        string Remarks { get; set; }
        string WorkerCarde { get; set; }
        bool? HasArtStartDate { get; set; }
        DateTime? ARTStartDate { get; set; }
    }
}
