using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Services
{
    public interface ISyncService
    {
        Task<int> Sync();
    }
}