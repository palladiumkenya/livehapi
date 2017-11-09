using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsFamilyTraceResultRepository : IRepository<ObsFamilyTraceResult, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsFamilyTraceResult> obses);
        void UpdateBooking(Encounter encounter, ObsFamilyTraceResult familyTraceResult);
    }
}