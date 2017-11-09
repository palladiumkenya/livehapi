using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsMemberScreening
    {
        DateTime BookingDate { get; set; }
        Guid Eligibility { get; set; }
        Guid EncounterId { get; set; }
        Guid HivStatus { get; set; }
        string Remarks { get; set; }
        DateTime ScreeningDate { get; set; }
    }
}