using System;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.Lookup;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IInvalidMessageRepository : IRepository<InvalidMessage, Guid>
    {
    }
}