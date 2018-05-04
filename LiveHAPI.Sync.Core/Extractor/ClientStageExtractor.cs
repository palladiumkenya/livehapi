using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientStageExtractor:IClientStageExtractor
    {
        private readonly IPersonRepository _personRepository;

        public ClientStageExtractor(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<IEnumerable<ClientStage>> Extract()
        {
            var persons = _personRepository.GetAllClients();
            throw new System.NotImplementedException();
        }
    }
}