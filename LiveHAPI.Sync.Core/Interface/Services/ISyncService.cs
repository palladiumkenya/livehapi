using System;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Services
{
    public interface ISyncService:IDisposable
    {
        Task<int> Sync();
    }
}