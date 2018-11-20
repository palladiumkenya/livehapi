using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Service
{
    public class TreeService:ITreeService
    {
        private readonly IClientContactNetworkRepository _repository;

        public TreeService(IClientContactNetworkRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ClientContactNetwork> GetAll()
        {
            return _repository.GetAll();
        }
    }
}