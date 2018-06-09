using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientRelationshipRepository : BaseRepository<ClientRelationship, Guid>, IClientRelationshipRepository
    {
        public ClientRelationshipRepository(LiveHAPIContext context) : base(context)
        {
        }
        
        public IEnumerable<ClientRelationship> GetIndexRelations()
        {
            return GetAll(x => x.ClientIsIndex);
        }
    }
}