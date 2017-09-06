using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;

namespace LiveHAPI.Infrastructure.Repository
{
    public class CountyRepository : BaseRepository<County, int>, ICountyRepository
    {
        public CountyRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}