using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Shared.Enum;

namespace LiveHAPI.Sync.Core.Interface.Loaders
{
    public interface IMessageLoader<T>:IDisposable
    {
        Task<IEnumerable<T>> Load(Guid? htsClientId = null, params LoadAction[] actions);
    }
}