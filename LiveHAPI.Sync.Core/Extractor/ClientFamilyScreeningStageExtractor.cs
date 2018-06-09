using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientFamilyScreeningStageExtractor : IClientFamilyScreeningStageExtractor
    {
        private readonly IContactsEncounterRepository _clientEncounterRepository;
       private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public ClientFamilyScreeningStageExtractor(IContactsEncounterRepository clientEncounterRepository, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientEncounterRepository = clientEncounterRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
        }

        public async Task<IEnumerable<ClientFamilyScreeningStage>> Extract(Guid? htsClientId = null)
        {
            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var clients = new List<ClientFamilyScreeningStage>();

            var encounters = _clientEncounterRepository.GetFamilyScreening(htsClientId);
            foreach (var encounter in encounters)
            {
                clients.AddRange(ClientFamilyScreeningStage.Create(encounter, subscriber));
            }

            return clients;
        }

        public Task<int> ExtractAndStage()
        {
            throw new NotImplementedException();
        }
    }
}