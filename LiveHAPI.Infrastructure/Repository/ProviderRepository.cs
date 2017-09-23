using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ProviderRepository : BaseRepository<Provider, Guid>, IProviderRepository
    {
        public ProviderRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}