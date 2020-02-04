using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using Serilog;

namespace LiveHAPI.Sync.Core.Service
{
    public class SyncLookupService : ISyncLookupService
    {
        private readonly IClientLookupReader _clientLookupReader;
        private readonly ISubscriberTranslationRepository _subscriberTranslationRepository;


        public SyncLookupService(IClientLookupReader clientLookupReader, ISubscriberTranslationRepository subscriberTranslationRepository)
        {
            _subscriberTranslationRepository = subscriberTranslationRepository;
            _clientLookupReader = clientLookupReader;
        }

        public async Task<int> Sync()
        {
            Log.Debug($"Syncing lookups...");
            var clientLookups = await _clientLookupReader.Read();

            var translations = Mapper.Map<List<SubscriberTranslation>>(clientLookups);
            int count = translations.Count;
            _subscriberTranslationRepository.Sync(translations);
            _subscriberTranslationRepository.Dispose();
            Log.Debug($"Synced {count} lookups!");
            return count;
        }

        public void Dispose()
        {
            _clientLookupReader?.Dispose();
            _subscriberTranslationRepository?.Dispose();
        }
    }
}
