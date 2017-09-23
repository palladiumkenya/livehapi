using System;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Events
{
    public class ClientSaved:IhEvent
    {
        public ClientInfo Client { get; }

        public ClientSaved(ClientInfo client)
        {
            Client = client;
        }
    }
}