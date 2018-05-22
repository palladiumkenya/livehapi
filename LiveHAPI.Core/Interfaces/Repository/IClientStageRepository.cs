using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Enum;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientStageRepository : IRepository<ClientStage, Guid>
    {
        void Clear();
        void BulkInsert(IEnumerable<ClientStage> clientStages);
        IEnumerable<ClientStage> GetIndexClients();
        ClientStage GetQueued(Guid clientId);
        void UpdateSyncStatus(Guid clientId, SyncStatus syncStatus, string statusInfo="");
    }
}