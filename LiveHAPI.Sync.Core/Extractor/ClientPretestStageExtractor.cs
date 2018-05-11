using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientPretestStageExtractor : IClientPretestStageExtractor
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientEncounterRepository _clientEncounterRepository;
        private readonly IClientStageRepository _clientStageRepository;

        private readonly IClientPretestStageRepository _clientPretestStageRepository;
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public ClientPretestStageExtractor(IClientStageRepository clientStageRepository, IClientPretestStageRepository clientPretestStageRepository,
            ISubscriberSystemRepository subscriberSystemRepository, IClientEncounterRepository clientEncounterRepository, IClientRepository clientRepository)
        {
            _clientStageRepository = clientStageRepository;
            _clientPretestStageRepository = clientPretestStageRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
            _clientEncounterRepository = clientEncounterRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientPretestStage>> Extract()
        {
            _clientPretestStageRepository.Clear();

            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var pretestStages = new List<ClientPretestStage>();

            var clientIds = _clientStageRepository.GetAll().Where(x=>x.IsIndex).Select(x => x.ClientId).ToList();

            foreach (var clientId in clientIds)
            {
                HtsEncounterType encounterType = HtsEncounterType.Initial;
                //client
                var client = _clientRepository.GetClientStates(clientId);
                if (null != client)
                {
                    encounterType= client.IsInAnyState(LiveState.HtsRetestedInc, LiveState.HtsRetestedPos,
                        LiveState.HtsRetestedNeg)
                        ? HtsEncounterType.Repeat
                        : HtsEncounterType.Initial;
                }

                //Pretests   

                var pretests = _clientEncounterRepository.GetPretest(clientId).ToList();
                if (pretests.Any())
                {
                    foreach (var finalResult in pretests)
                    {
                        pretestStages.Add(ClientPretestStage.Create(encounterType,finalResult, subscriber));
                    }
                }
            }
            _clientPretestStageRepository.BulkInsert(pretestStages);
            return _clientPretestStageRepository.GetAll();
        }
    }
}