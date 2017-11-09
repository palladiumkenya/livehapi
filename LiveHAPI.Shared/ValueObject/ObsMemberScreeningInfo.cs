using System;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsMemberScreeningInfo : IObsMemberScreening
    {
        public Guid Id { get; set; }
        public DateTime ScreeningDate { get; set; }
        public Guid HivStatus { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime BookingDate { get; set; }
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }
    }
}