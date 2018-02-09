using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Infrastructure.Repository
{
    public class SubscriberConfigRepository : BaseRepository<SubscriberConfig,Guid>, ISubscriberConfigRepository
    {
        public SubscriberConfigRepository(LiveHAPIContext context) : base(context)
        {
        }

        public IEnumerable<SubscriberConfig> GetFeatures()
        {
            return Context.SubscriberConfigs.Where(x => x.Name.ToLower().Contains(".FeatureId".ToLower()));
        }
    }
}