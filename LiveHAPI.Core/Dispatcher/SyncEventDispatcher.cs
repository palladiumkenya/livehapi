using LiveHAPI.Core.Events;
using LiveHAPI.Core.Handlers;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Interfaces.Handler;

namespace LiveHAPI.Core.Dispatcher
{
   public static class SyncEventDispatcher
   {
       public static void Raise<T>(T args, IHandler<T> handler) where T : IhEvent
       {
           handler.Handle(args);
       }
   }
}
