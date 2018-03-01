using System;
using System.Linq;
using EFCore.BulkExtensions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class UserSummaryRepository : BaseRepository<UserSummary, Guid>, IUserSummaryRepository
    {
        public UserSummaryRepository(LiveHAPIContext context) : base(context)
        {
        }

        public void DeleteByProvider(Guid providerId)
        {
            var summaries = Context.UserSummaries.Where(x => x.UserId == providerId);
            Context.RemoveRange(summaries);
            Context.SaveChanges();
        }
    }
}