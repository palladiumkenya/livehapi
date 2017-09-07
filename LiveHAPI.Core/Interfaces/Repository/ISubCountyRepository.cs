using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ISubCountyRepository : IRepository<SubCounty, Guid>
    {
        IEnumerable<SubCounty> GetByCounty(int countyId);
    }
}