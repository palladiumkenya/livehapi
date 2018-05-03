using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveHAPI.Sync.Core.Interface.Writers
{
    public interface IClientWriter<T>
    {
        Task<IEnumerable<T>> Write();
    }
}