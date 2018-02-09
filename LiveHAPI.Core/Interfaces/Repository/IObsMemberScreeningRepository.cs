using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsMemberScreeningRepository : IRepository<ObsMemberScreening, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsMemberScreening> obses);
    }
}