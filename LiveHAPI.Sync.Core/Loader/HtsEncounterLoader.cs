using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Interface.Loaders;

namespace LiveHAPI.Sync.Core.Loader
{
    public class HtsEncounterLoader : IHtsEncounterLoader
    {
        private readonly IClientEncounterRepository _clientEncounterRepository;

        public HtsEncounterLoader(IClientEncounterRepository clientEncounterRepository)
        {
            _clientEncounterRepository = clientEncounterRepository;
        }

        public ENCOUNTERS Load()
        {
            throw new NotImplementedException();
        }

        public ENCOUNTERS Load(Guid clientId)
        {
            var pretests = _clientEncounterRepository.GetPretest(clientId);

            var encounters = _clientEncounterRepository.GetFinalTesting(clientId);
            throw new NotImplementedException();
        }
    }
}