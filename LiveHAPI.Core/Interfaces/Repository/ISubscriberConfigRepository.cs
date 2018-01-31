using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ISubscriberConfigRepository : IRepository<SubscriberConfig, Guid>
    {
        IEnumerable<SubscriberConfig> GetFeatures();
    }
}