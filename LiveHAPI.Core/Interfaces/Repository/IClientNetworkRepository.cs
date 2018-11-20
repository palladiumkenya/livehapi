using System;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientNetworkRepository:IRepository<ClientNetwork,Guid>
    {
        Task Generate();
        IQueryable<ClientNetwork> GetAll();
    }
}