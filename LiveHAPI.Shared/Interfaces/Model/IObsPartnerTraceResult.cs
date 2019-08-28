using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsPartnerTraceResult
    {
        DateTime Date { get; set; }
        Guid IndexClientId { get; set; }
        Guid EncounterId { get; set; }
        Guid Mode { get; set; }
        string ModeDisplay { get; set; }
        Guid Outcome { get; set; }
        string OutcomeDisplay { get; set; }
        Guid? Consent { get; set; }
        DateTime? BookingDate { get; set; }
        Guid? ReasonNotContacted { get; set; }
        string ReasonNotContactedOther { get; set; }
    }
}
