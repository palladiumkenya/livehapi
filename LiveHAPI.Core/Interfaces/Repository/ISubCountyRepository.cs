using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ISubCountyRepository : IRepository<SubCounty, Guid>
    {
        IEnumerable<SubCounty> GetByCounty(int countyId);
    }
}