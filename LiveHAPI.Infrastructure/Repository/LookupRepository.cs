using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;

namespace LiveHAPI.Infrastructure.Repository
{
    public class LookupRepository:ILookupRepository
    {
        private readonly LiveHAPIContext _context;

        public LookupRepository(LiveHAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<T> ReadAll<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}