using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsRepository : IRepository<Obs,Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<Obs> obses);
    }
}