using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ILookupRepository
    {
        IEnumerable<T> ReadAll<T>(Expression<Func<T, object>> children=null) where T : class;
        IEnumerable<Form> ReadAllForms();
        IEnumerable<Question> ReadAllQuestions();
    }
}