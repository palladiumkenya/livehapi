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
    public class ClientFinalTestStageExtractor : IClientFinalTestStageExtractor
    {
        private readonly IClientEncounterRepository _clientEncounterRepository;
       private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public ClientFinalTestStageExtractor(IClientEncounterRepository clientEncounterRepository, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientEncounterRepository = clientEncounterRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
        }

        public async Task<IEnumerable<ClientFinalTestStage>> Extract(Guid? htsClientId = null)
        {
            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var clients = new List<ClientFinalTestStage>();

            var encounters = _clientEncounterRepository.GetFinalTesting(htsClientId);
            foreach (var encounter in encounters)
            {
                var pretestId =
                    _clientEncounterRepository.GetPretestEncounterId(encounter.ClientId, encounter.EncounterDate);
              
                clients.AddRange(ClientFinalTestStage.Create(encounter, subscriber,pretestId));
            }

            return clients.Where(x => !x.Id.IsNullOrEmpty());
        }

        public Task<int> ExtractAndStage()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _clientEncounterRepository?.Dispose();
            _subscriberSystemRepository?.Dispose();
        }
    }
}