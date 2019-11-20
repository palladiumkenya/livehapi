using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Interface.Extractors;
using Serilog;

namespace LiveHAPI.Sync.Core.Extractor
{
    public class ClientStageExtractor : IClientStageExtractor
    {
        private readonly IPersonRepository _personRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;
        private readonly IPracticeRepository _practiceRepository;

        public ClientStageExtractor(IPersonRepository personRepository, IClientStageRepository clientStageRepository,
            ISubscriberSystemRepository subscriberSystemRepository, IClientRepository clientRepository, IPracticeRepository practiceRepository)
        {
            _personRepository = personRepository;
            _clientStageRepository = clientStageRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
            _clientRepository = clientRepository;
            _practiceRepository = practiceRepository;
        }

        public async Task<IEnumerable<ClientStage>> Extract(Guid? htsClientId = null)
        {
            //_clientStageRepository.Clear();

            var subscriber = await _subscriberSystemRepository.GetDefaultAsync();

            if (null == subscriber)
                throw new Exception("Default EMR NOT SET");

            var practices = _practiceRepository.GetAllDefault().ToList();

            var clients = new List<ClientStage>();

            var persons = _personRepository.GetAllClients().ToList();
            var tCount = persons.Count;
            int count = 0;
            Log.Debug($"clientStages:{tCount}");

            foreach (var person in persons)
            {
                count++;
                var client = ClientStage.Create(person, subscriber);
                var practice= practices.FirstOrDefault(x => x.Id == client.PracticeId);

                if (null != practice)
                    client.SiteCode = practice.Code;

                clients.Add(client);
                Log.Debug($"clientStages: {count} of {tCount}");
            }
            return clients.Where(x => !x.ClientId.IsNullOrEmpty());
        }

        public async Task<int> ExtractAndStage()
        {
            var inserts = new List<ClientStage>();
            var updates = new List<ClientStage>();

            var clients = await Extract();

            if (null != clients)
            {
                var clientStages = clients.ToList();

                foreach (var client in clientStages)
                {
                    if (_clientStageRepository.ClientExisits(client.ClientId))
                    {
                        updates.Add(client);
                    }
                    else
                    {
                        inserts.Add(client);
                    }
                }

                if (inserts.Any())
                    _clientStageRepository.BulkInsert(inserts);

                if (updates.Any())
                    _clientStageRepository.BulkUpdate(updates);

                if (clientStages.Any())
                    _clientRepository.UpdateSyncStatus(clientStages);
            }

            return 1;
        }

        public void Dispose()
        {
            _personRepository?.Dispose();
            _clientStageRepository?.Dispose();
            _clientRepository?.Dispose();
            _subscriberSystemRepository?.Dispose();
            _practiceRepository?.Dispose();
        }
    }
}
