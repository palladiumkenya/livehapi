using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsTraceResultRepository : IRepository<ObsTraceResult, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsTraceResult> obses);
    }
}