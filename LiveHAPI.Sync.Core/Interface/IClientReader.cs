using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface
{
    public interface IClientReader<T>
    {
        Task<IEnumerable<T>> Read();
    }
}