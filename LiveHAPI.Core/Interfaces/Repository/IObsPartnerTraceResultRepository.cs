using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsPartnerTraceResultRepository : IRepository<ObsPartnerTraceResult, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsPartnerTraceResult> obses);
        void UpdateBooking(Encounter encounter, ObsPartnerTraceResult familyTraceResult);

    }
}