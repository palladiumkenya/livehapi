using System;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IUserSummaryRepository : IRepository<UserSummary,Guid>
    {
        void DeleteByProvider(Guid providerId);
    }
}