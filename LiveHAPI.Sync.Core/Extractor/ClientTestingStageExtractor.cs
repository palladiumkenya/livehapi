using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientTestingStageExtractor : IClientTestingStageExtractor
    {
        private readonly IClientEncounterRepository _clientEncounterRepository;
       private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public ClientTestingStageExtractor(IClientEncounterRepository clientEncounterRepository, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientEncounterRepository = clientEncounterRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
        }

        public async Task<IEnumerable<ClientTestingStage>> Extract()
        {
            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var clients = new List<ClientTestingStage>();

            var encounters = _clientEncounterRepository.GetTesting();
            foreach (var encounter in encounters)
            {
                var pretestId =
                    _clientEncounterRepository.GetPretestEncounterId(encounter.ClientId, encounter.EncounterDate);
                clients.AddRange(ClientTestingStage.Create(encounter, subscriber,pretestId));
            }

            return clients;
        }

        public Task<int> ExtractAndStage()
        {
            throw new NotImplementedException();
        }
    }
}