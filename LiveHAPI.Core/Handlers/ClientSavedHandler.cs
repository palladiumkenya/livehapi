using System;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;

namespace LiveHAPI.Core.Handlers
{
    public class ClientSavedHandler:IClientSavedHandler
    {
        private readonly IClientRepository _clientRepository;

        public ClientSavedHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void Handle(ClientSaved args)
        {
            var client = _clientRepository.GetClient(args.ClientId);

            //EMR Action | IL Action
            Console.WriteLine($"Client: {client} SAVED !");
        }
    }
}