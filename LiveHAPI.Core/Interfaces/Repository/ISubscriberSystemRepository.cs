using System;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ISubscriberSystemRepository : IRepository<SubscriberSystem,Guid>
    {
        SubscriberSystem GetDefault();
        Task<SubscriberSystem> GetDefaultAsync();
    }
}