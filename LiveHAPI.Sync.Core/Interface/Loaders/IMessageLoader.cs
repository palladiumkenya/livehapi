using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Loaders
{
    public interface IMessageLoader<T>
    {
       Task<IEnumerable<T>> Load();
    }
}