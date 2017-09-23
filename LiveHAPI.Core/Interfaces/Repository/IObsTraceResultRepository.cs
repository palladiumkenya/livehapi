using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsTraceResultRepository : IRepository<ObsTraceResult, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsTraceResult> obses);
    }
}