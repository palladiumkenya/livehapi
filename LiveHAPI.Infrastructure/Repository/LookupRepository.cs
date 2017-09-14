using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class LookupRepository:ILookupRepository
    {
        private readonly LiveHAPIContext _context;

        public LookupRepository(LiveHAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<T> ReadAll<T>(Expression<Func<T, object>> children=null) where T : class
        {
            if(null!=children)
                return _context.Set<T>().Include(children);

            return _context.Set<T>();
        }

      
    }
}