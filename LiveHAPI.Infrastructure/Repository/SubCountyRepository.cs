using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;

namespace LiveHAPI.Infrastructure.Repository
{
    public class SubCountyRepository:BaseRepository<SubCounty,Guid>,ISubCountyRepository
    {
        public SubCountyRepository(LiveHAPIContext context) : base(context)
        {
        }

        public IEnumerable<SubCounty> GetByCounty(int countyId)
        {
            return Context.SubCounties.Where(x => x.CountyId == countyId).ToList();
        }
    }
}