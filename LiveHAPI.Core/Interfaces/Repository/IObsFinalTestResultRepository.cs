using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsFinalTestResultRepository : IRepository<ObsFinalTestResult, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsFinalTestResult> obses);
    }
}