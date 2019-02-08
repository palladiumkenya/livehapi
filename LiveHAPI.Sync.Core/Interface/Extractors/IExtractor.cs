using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Extractors
{
    public interface IExtractor<T> : IDisposable
    {
        Task<IEnumerable<T>> Extract(Guid? htsClientId = null);
        Task<int> ExtractAndStage();
    }
}