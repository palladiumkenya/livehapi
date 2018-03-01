using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientSummaryRepository : BaseRepository<ClientSummary, Guid>, IClientSummaryRepository
    {
        public ClientSummaryRepository(LiveHAPIContext context) : base(context)
        {
        }

        public void DeleteByClient(Guid clientId)
        {
            var summaries = Context.ClientSummaries.Where(x => x.ClientId == clientId);
            Context.RemoveRange(summaries);
            Context.SaveChanges();
        }
    }
}