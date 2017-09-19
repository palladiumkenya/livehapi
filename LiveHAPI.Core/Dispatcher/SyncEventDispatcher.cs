using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Dispatcher
{
   public static class SyncEventDispatcher
   {
       public static void Raise<T>(T args, IHandler<T> handler, SubscriberSystem subscriberSystem) where T : IhEvent
       {
           handler.Handle(args,subscriberSystem);
       }
   }
}
