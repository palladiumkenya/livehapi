using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PSmartStoreRepository : BaseRepository<PSmartStore, Guid> , IPSmartStoreRepository
    {
        public PSmartStoreRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}
