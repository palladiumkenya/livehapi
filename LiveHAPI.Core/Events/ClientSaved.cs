using System;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Events
{
    public class ClientSaved:IhEvent
    {
        public Guid ClientId { get;}

        public ClientSaved(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}