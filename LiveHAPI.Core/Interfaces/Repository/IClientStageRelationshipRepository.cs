using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientStageRelationshipRepository : IRepository<ClientStageRelationship, Guid>
    {
        void Clear();
        void BulkInsert(IEnumerable<ClientStageRelationship> clientStages);
    }
}