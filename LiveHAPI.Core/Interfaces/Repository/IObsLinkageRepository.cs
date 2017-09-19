using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsLinkageRepository : IRepository<ObsLinkage, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsLinkage> obses);
    }
}