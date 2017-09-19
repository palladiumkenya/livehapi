using LiveHAPI.Core.Interfaces.Events;

namespace LiveHAPI.Core.Interfaces.Handler
{
    public interface IHandler<T> where T : IhEvent
    {
        void Handle(T args);
    }
}