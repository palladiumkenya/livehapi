using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client, Guid>, IClientRepository
    {
        public ClientRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Client GetClient(Guid id)
        {
            return Context.Clients
                .Include(x => x.Identifiers)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}