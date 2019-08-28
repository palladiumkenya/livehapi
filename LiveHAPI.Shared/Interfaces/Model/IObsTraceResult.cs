using System;

namespace LiveHAPI.Core.Model.Encounters
{
    public interface IObsTraceResult
    {
        DateTime Date { get; set; }
        Guid EncounterId { get; set; }
        Guid Mode { get; set; }
        Guid Outcome { get; set; }
        Guid? ReasonNotContacted { get; set; }
        string ReasonNotContactedOther { get; set; }
    }
}
