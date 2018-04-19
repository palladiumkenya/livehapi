using System;
using System.Linq;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Interfaces.Repository;


namespace LiveHAPI.IQCare.Core.Handlers
{
    public class PSmartStoreSavedHandler : IPSmartStoreSavedHandler
    {
        private readonly IConfigRepository _configRepository;
        private readonly IPStoreRepository _encounterRepository;

        public PSmartStoreSavedHandler(IPStoreRepository encounterRepository, IConfigRepository configRepository)
        {
            _encounterRepository = encounterRepository;
            _configRepository = configRepository;
        }

        public void Handle(PSmartStoreSaved args, SubscriberSystem subscriberSystem)
        {
            var location = _configRepository.GetLocations().FirstOrDefault();
            _encounterRepository.CreateOrUpdate(args.Encounters,subscriberSystem,location);
        }
    }
}