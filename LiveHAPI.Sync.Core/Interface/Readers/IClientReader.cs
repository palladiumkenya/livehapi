using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Readers
{
    public interface IClientReader<T>:IDisposable
    {
        Task<IEnumerable<T>> Read();
    }
}