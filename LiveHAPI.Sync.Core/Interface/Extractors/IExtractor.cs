using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Extractors
{
    public interface IExtractor<T>
    {
        Task<IEnumerable<T>> Extract(); 
    }
}