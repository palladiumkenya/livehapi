using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository.Subscriber
{
    public interface IEmrRepository
    {
        void CreateOrUpdate(SubscriberSystem system, Client client);
    }
}