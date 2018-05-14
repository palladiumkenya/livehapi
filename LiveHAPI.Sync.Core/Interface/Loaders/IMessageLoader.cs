using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Shared.Enum;

namespace LiveHAPI.Sync.Core.Interface.Loaders
{
    public interface IMessageLoader<T>
    {
       Task<IEnumerable<T>> Load(params LoadAction[] actions);
    }
}