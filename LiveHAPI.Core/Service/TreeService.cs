using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Service
{
    public class TreeService:ITreeService
    {
        private readonly IClientNetworkRepository _repository;

        public TreeService(IClientNetworkRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<ClientNetwork> GetAll()
        {
            return _repository.GetAll();
        }
    }
}