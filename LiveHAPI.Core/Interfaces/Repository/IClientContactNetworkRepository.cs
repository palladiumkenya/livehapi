using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientContactNetworkRepository:IRepository<ClientContactNetwork,Guid>
    {
        Task Generate();
        IEnumerable<ClientContactNetwork> LoadAll();
        IEnumerable<ClientContactNetwork> LoadById(Guid id); 
        IEnumerable<ClientContactNetwork> LoadTree();
    }
}