using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientPretestStageRepository : IRepository<ClientPretestStage, Guid>
    {
        void BulkInsert(IEnumerable<ClientPretestStage> clientStages);
    }
}