using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;

namespace LiveHAPI.Core.Service
{
    public class SyncManagerService:ISyncManagerService
    {
        private readonly IClientStageRepository _clientStageRepository;

        public SyncManagerService(IClientStageRepository clientStageRepository)
        {
            _clientStageRepository = clientStageRepository;
        }

        public IEnumerable<ClientStage> GetSyncErrorClients()
        {
            return _clientStageRepository.GetByStatus(SyncStatus.SentFail);
        }

        public int GetSyncErrorClientsCount()
        {
            return GetSyncErrorClients().Select(x => x.ClientId).Count();
        }

        public Task Resend(IEnumerable<Guid> clientIds)
        {
            return _clientStageRepository.UpdateSyncStatus(clientIds, SyncStatus.Staged);
        }

        public Task ResendAll()
        {
            return _clientStageRepository.UpdateAllWithSyncStatus(SyncStatus.SentFail, SyncStatus.Staged);
        }

        public Stats GetStats()
        {
            var all = _clientStageRepository.GetAll().Select(x => new {x.ClientId, x.SyncStatus}).ToList();

            var stats = new Stats(
                all.Count,
                all.Where(x => x.SyncStatus == SyncStatus.Staged).ToList().Count, 
                all.Where(x => x.SyncStatus == SyncStatus.SentSuccess).ToList().Count,
                all.Where(x => x.SyncStatus == SyncStatus.SentFail).ToList().Count
                );

            return stats;
        }
    }
}