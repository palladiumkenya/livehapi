using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsPartnerScreening
    {
        Guid? PNSApproach { get; set; }
        Guid? LivingWithClient { get; set; }
        Guid? PNSRealtionship { get; set; }
        string Occupation { get; set; }
        Guid? IPVOutcome { get; set; }
        Guid? PnsAccepted { get; set; }
        DateTime? BookingDate { get; set; }
        Guid Eligibility { get; set; }
        Guid EncounterId { get; set; }
        Guid HivStatus { get; set; }
        Guid IPVScreening { get; set; }
        Guid PhysicalAssult { get; set; }
        string Remarks { get; set; }
        DateTime ScreeningDate { get; set; }
        Guid SexuallyUncomfortable { get; set; }
        Guid Threatened { get; set; }
        bool BookingMet { get; set; }
        DateTime? DateBookingMet { get; set; }
        Guid? TraceId { get; set; }
    }
}