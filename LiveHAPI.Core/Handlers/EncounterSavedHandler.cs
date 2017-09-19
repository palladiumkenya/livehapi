using System;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;

namespace LiveHAPI.Core.Handlers
{
    public class EncounterSavedHandler : IEncounterSavedHandler
    {
        private readonly IEncounterRepository _encounterRepository;


        public EncounterSavedHandler(IEncounterRepository encounterRepository)
        {
            _encounterRepository = encounterRepository;
        }

        public void Handle(EncounterSaved args)
        {
            Console.WriteLine(new string('*', 50));

            foreach (var e in args.EncounterIds)
            {
                var encounter = _encounterRepository.Get(e);

                //EMR Action | IL Action
                
                Console.WriteLine($"Encounter: {encounter} SAVED !");
            }

        }
    }
}