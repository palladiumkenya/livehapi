using System;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientSummaryRepository : IRepository<ClientSummary,Guid>
    {
        void DeleteByClient(Guid clientId);
    }
}