using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Readers
{
    public interface IClientReader<T>
    {
        Task<IEnumerable<T>> Read();
    }
}