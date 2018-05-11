using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientRelationshipRepository : IRepository<ClientRelationship,Guid>
    {
        IEnumerable<ClientRelationship> GetIndexRelations();
    }
}