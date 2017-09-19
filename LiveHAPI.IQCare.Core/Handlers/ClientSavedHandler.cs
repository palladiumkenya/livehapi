using System;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Repository.Subscriber;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace LiveHAPI.IQCare.Core.Handlers
{
    public class ClientSavedHandler:IClientSavedHandler
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<ClientSavedHandler> _logger;

        public ClientSavedHandler(IPatientRepository patientRepository, ILogger<ClientSavedHandler> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }

        public void Handle(ClientSaved args)
        {
            var msg = $"Client: {args.Client} SAVED !";
            Console.WriteLine(msg);
            _logger.LogDebug(msg);
            _logger.LogDebug(new string('*', 50));
            _logger.LogDebug(new string('*', 50));
        }
    }
}