using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientFamilyTracingStageExtractor : IClientFamilyTracingStageExtractor
    {
        private readonly IClientEncounterRepository _clientEncounterRepository;
       private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public ClientFamilyTracingStageExtractor(IClientEncounterRepository clientEncounterRepository, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientEncounterRepository = clientEncounterRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
        }

        public async Task<IEnumerable<ClientFamilyTracingStage>> Extract()
        {
            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var clients = new List<ClientFamilyTracingStage>();
//
//            var encounters = _clientEncounterRepository.GetFamilyTracing();
//            foreach (var encounter in encounters)
//            {
//                clients.AddRange(ClientFamilyTracingStage.Create(encounter, subscriber));
//            }

            return clients;
        }
    }
}