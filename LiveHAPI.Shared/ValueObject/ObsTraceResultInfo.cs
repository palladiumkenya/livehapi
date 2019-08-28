using System;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsTraceResultInfo : IObsTraceResult
    {
        public Guid Id { get; set; }


        public DateTime Date { get; set; }
        public Guid EncounterId { get; set; }
        public Guid Mode { get; set; }
        public Guid Outcome { get; set; }
        public Guid? ReasonNotContacted { get; set; }
        public string ReasonNotContactedOther { get; set; }
    }
}
