using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsPartnerScreening
    {
        DateTime BookingDate { get; set; }
        Guid Eligibility { get; set; }
        Guid EncounterId { get; set; }
        Guid HivStatus { get; set; }
        Guid IPVScreening { get; set; }
        Guid PhysicalAssult { get; set; }
        string Remarks { get; set; }
        DateTime ScreeningDate { get; set; }
        Guid SexuallyUncomfortable { get; set; }
        Guid Threatened { get; set; }
    }
}