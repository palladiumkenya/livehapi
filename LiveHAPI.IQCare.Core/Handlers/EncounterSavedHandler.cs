using System;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace LiveHAPI.IQCare.Core.Handlers
{
    public class EncounterSavedHandler : IEncounterSavedHandler
    {
        private readonly IPatientEncounterRepository _encounterRepository;
        private readonly ILogger<EncounterSavedHandler> _logger;

        public EncounterSavedHandler(IPatientEncounterRepository encounterRepository, ILogger<EncounterSavedHandler> logger)
        {
            _encounterRepository = encounterRepository;
            _logger = logger;
        }

        public void Handle(EncounterSaved args)
        {
            foreach (var e in args.Encounters)
            {
                var msg = $"Encounter: {e} SAVED !";
                Console.WriteLine(msg);
            }
            _logger.LogDebug(new string('+', 50));
            _logger.LogDebug(new string('+', 50));
        }
    }
}