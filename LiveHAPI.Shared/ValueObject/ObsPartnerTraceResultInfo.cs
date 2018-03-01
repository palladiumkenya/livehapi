using System;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsPartnerTraceResultInfo : IObsPartnerTraceResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid IndexClientId { get; set; }

        public Guid Mode { get; set; }
        public string ModeDisplay { get; set; }
        public Guid Outcome { get; set; }
        public string OutcomeDisplay { get; set; }
        public Guid? Consent { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid EncounterId { get; set; }
        
    }
}
