using System;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ISubscriberSystemRepository : IRepository<SubscriberSystem,Guid>
    {
        SubscriberSystem GetDefault();
    }
}