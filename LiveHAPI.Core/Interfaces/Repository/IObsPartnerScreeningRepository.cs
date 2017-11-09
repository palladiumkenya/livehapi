using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IObsPartnerScreeningRepository : IRepository<ObsPartnerScreening, Guid>
    {
        void ReplaceAll(Guid encounterId,IEnumerable<ObsPartnerScreening> obses);
    }
}