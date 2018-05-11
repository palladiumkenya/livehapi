using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Interface.Extractors;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientStageRelationshipExtractor : IClientStageRelationshipExtractor
    {
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IClientStageRelationshipRepository _clientStageRelationshipRepository;
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public ClientStageRelationshipExtractor(IClientRelationshipRepository clientRelationshipRepository, IClientStageRelationshipRepository clientStageRelationshipRepository,
            ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientRelationshipRepository = clientRelationshipRepository;
            _clientStageRelationshipRepository = clientStageRelationshipRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
        }

        public async Task<IEnumerable<ClientStageRelationship>> Extract()
        {
            _clientStageRelationshipRepository.Clear();

            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");
            var clients = new List<ClientStageRelationship>();

            var indexRelations = _clientRelationshipRepository.GetIndexRelations();
            foreach (var relationship in indexRelations)
            {
                clients.Add(ClientStageRelationship.Create(relationship, subscriber));
            }

            _clientStageRelationshipRepository.BulkInsert(clients);

            return _clientStageRelationshipRepository.GetAll();
        }

        public Task ExtractAndStage()
        {
            throw new NotImplementedException();
        }
    }
}