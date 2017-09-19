using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsTestResultRepository : IRepository<ObsTestResult,Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsTestResult> obses);
    }
}