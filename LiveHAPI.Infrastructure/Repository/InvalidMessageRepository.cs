using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;

namespace LiveHAPI.Infrastructure.Repository
{
    public class InvalidMessageRepository : BaseRepository<InvalidMessage, Guid>, IInvalidMessageRepository
    {
        public InvalidMessageRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}