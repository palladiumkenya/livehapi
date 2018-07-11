using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
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

        public async Task<IEnumerable<ClientTestingStage>> Extract(Guid? htsClientId = null)
        {
            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var clients = new List<ClientTestingStage>();

            var encounters = _clientEncounterRepository.GetTesting(htsClientId);
            foreach (var encounter in encounters)
            {
                var pretestId =
                    _clientEncounterRepository.GetPretestEncounterId(encounter.ClientId, encounter.EncounterDate);
                clients.AddRange(ClientTestingStage.Create(encounter, subscriber,pretestId));
            }

            return clients.Where(x => !x.Id.IsNullOrEmpty());
        }

        public Task<int> ExtractAndStage()
        {
            throw new NotImplementedException();
        }
    }
}