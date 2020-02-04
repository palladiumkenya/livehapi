using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;
using LiveHAPI.Sync.Core.Interface.Extractors;
using Serilog;

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

        public async Task<IEnumerable<ClientPretestStage>> Extract(Guid? htsClientId = null)
        {
            _clientPretestStageRepository.Clear();

            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var pretestStages = new List<ClientPretestStage>();

            var clientIds = _clientStageRepository.GetAll().Where(x => x.IsIndex && x.SyncStatus == SyncStatus.Staged)
                .Select(x => x.ClientId).ToList();

            var tCount = clientIds.Count;
            int count = 0;
            Log.Debug($"pretestStages:{tCount}");

            foreach (var clientId in clientIds)
            {
                count++;
                HtsEncounterType encounterType = HtsEncounterType.Initial;

                //  Client

                var client = _clientRepository.GetClientStates(clientId);
                if (null != client)
                {
                    encounterType = client.IsInAnyState(LiveState.HtsRetestedInc, LiveState.HtsRetestedPos,
                        LiveState.HtsRetestedNeg)
                        ? HtsEncounterType.Repeat
                        : HtsEncounterType.Initial;
                }

                //  Pretests
                var pretests = _clientEncounterRepository.GetPretest(clientId).ToList();

                if (pretests.Any())
                {
                    foreach (var finalResult in pretests)
                    {
                        var stage = ClientPretestStage.Create(encounterType, finalResult, subscriber);
                        if (null != stage)
                            pretestStages.Add(stage);
                    }
                }

                Log.Debug($"pretestStages: {count} of {tCount}");
            }

            return pretestStages;
        }

        public async Task<int> ExtractAndStage()
        {
            var pretestStages = await Extract();
            _clientPretestStageRepository.BulkInsert(pretestStages);
           // _clientStageRepository.UpdateSyncStatus();
            return 1;
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
            _clientEncounterRepository?.Dispose();
            _clientStageRepository?.Dispose();
            _clientPretestStageRepository?.Dispose();
            _subscriberSystemRepository?.Dispose();
        }
    }
}
