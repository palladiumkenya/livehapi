using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientStageExtractor : IClientStageExtractor
    {
        private readonly IPersonRepository _personRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;
        
        public ClientStageExtractor(IPersonRepository personRepository, IClientStageRepository clientStageRepository,
            ISubscriberSystemRepository subscriberSystemRepository, IClientRepository clientRepository)
        {
            _personRepository = personRepository;
            _clientStageRepository = clientStageRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientStage>> Extract(Guid? htsClientId = null)
        {
            _clientStageRepository.Clear();

            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            
           
            var clients = new List<ClientStage>();

            var persons = _personRepository.GetAllClients();
            foreach (var person in persons)
            {
                clients.Add(ClientStage.Create(person, subscriber));
            }

            return clients;
        }

        public async Task<int> ExtractAndStage()
        {
            var clients =await Extract();
            
            //inserts or update
            
            _clientStageRepository.BulkInsert(clients);
            _clientRepository.UpdateSyncStatus(clients);
            return 1;
        }
    }
}