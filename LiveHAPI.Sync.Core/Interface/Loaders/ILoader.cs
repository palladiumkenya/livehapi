using System.Collections.Generic;

namespace LiveHAPI.Sync.Core.Interface.Loaders
{
    public interface ILoader<T>
    {
        T Load();
    }
}