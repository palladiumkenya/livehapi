using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Handler
{
    public interface IHandler<T> where T : IhEvent
    {
        void Handle(T args,SubscriberSystem subscriberSystem);
    }
}