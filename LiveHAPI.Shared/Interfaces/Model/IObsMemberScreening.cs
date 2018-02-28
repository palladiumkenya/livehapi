using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsMemberScreening
    {
        DateTime BookingDate { get; set; }
        Guid Eligibility { get; set; }
        Guid IndexClientId { get; set; }
        Guid EncounterId { get; set; }
        Guid HivStatus { get; set; }
        string Remarks { get; set; }
        DateTime ScreeningDate { get; set; }
        bool BookingMet { get; set; }
        DateTime? DateBookingMet { get; set; }
        Guid? TraceId { get; set; }
    }
}