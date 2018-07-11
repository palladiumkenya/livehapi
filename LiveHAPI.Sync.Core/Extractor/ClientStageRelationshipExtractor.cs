using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
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

        public async Task<IEnumerable<ClientStageRelationship>> Extract(Guid? htsClientId = null)
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

            return clients.Where(x => !x.Id.IsNullOrEmpty());
        }

        public async Task<int> ExtractAndStage()
        {
            var clients = await Extract();
            _clientStageRelationshipRepository.BulkInsert(clients);
            return 1;
        }
    }
}