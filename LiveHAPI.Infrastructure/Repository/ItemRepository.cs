using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ItemRepository : BaseRepository<Item, Guid>, IItemRepository
    {
        public ItemRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}