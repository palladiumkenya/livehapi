using System;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Subscriber;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class SubscriberSystemRepository : BaseRepository<SubscriberSystem, Guid>, ISubscriberSystemRepository
    {
        public SubscriberSystemRepository(LiveHAPIContext context) : base(context)
        {
        }
        
        public SubscriberSystem GetDefault()
        {
            var sub=Context.SubscriberSystems
                .Include(x => x.Configs)
                .Include(x => x.Actions)
                .Include(x => x.Messages)
                .Include(x => x.Maps)
                .Include(x=>x.Translations)
                .Include(x => x.Cohorts)
                .FirstOrDefault(x=>x.IsDefault);

            if (null != sub)
                sub.Users = Context.Users.ToList();

            return sub;
        }

        public async Task<SubscriberSystem> GetDefaultAsync()
        {
            var sub = Context.SubscriberSystems
                          .Include(x => x.Translations)
                          .Include(x => x.Maps)
                          .AsNoTracking()
                          .FirstOrDefaultAsync(x => x.IsDefault) ?? Context.SubscriberSystems
                          .Include(x => x.Translations)
                          .Include(x=>x.Maps)
                          .AsNoTracking()
                          .FirstOrDefaultAsync();

            var subscriber = await sub;
            if (null != subscriber)
                subscriber.Users = Context.Users.ToList();
           
            return subscriber;
        }
    }
}