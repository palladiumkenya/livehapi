using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface ITreeService
    {
        IEnumerable<ClientContactNetwork> GetAll();
    }
}