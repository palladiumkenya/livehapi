using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client, Guid>, IClientRepository
    {
        public ClientRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}