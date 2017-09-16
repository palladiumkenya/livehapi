using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsTestResultRepository : IRepository<ObsTestResult,Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsTestResult> obses);
    }
}