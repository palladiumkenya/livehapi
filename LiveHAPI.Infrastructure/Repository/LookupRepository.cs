using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
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

        public IEnumerable<Form> ReadAllForms()
        {
            return _context.Forms
                .Include(q => q.Programs)
                .ToList();
        }

        public IEnumerable<Question> ReadAllQuestions()
        {
            return _context.Questions
                .Include(q => q.Branches)
                .Include(q => q.Validations)
                .Include(q => q.ReValidations)
                .Include(q => q.Transformations)
                .Include(q => q.RemoteTransformations)
                .ToList();
        }


    }
}