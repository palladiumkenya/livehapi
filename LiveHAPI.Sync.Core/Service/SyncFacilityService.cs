using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using Serilog;

namespace LiveHAPI.Sync.Core.Service
{
    public class SyncFacilityService : ISyncFacilityService
    {
        private readonly IClientFacilityReader _clientFacilityReader;
        private readonly IPracticeRepository  _practiceRepository;


        public SyncFacilityService(IClientFacilityReader clientFacilityReader, IPracticeRepository practiceRepository)
        {
            _practiceRepository = practiceRepository;
            _clientFacilityReader = clientFacilityReader;
        }

        public async Task<int> Sync()
        {
            Log.Debug("Syncing facilities...");
            var clientFacilities = await _clientFacilityReader.Read();

            var practices = Mapper.Map<List<Practice>>(clientFacilities);
            int count = practices.Count;

            foreach (var practice in practices)
            {
                practice.MakeFacility();
            }

            _practiceRepository.Sync(practices);

            Log.Debug($"Synced {count} facilities!");
            return count;
        }


        public void Dispose()
        {
            _clientFacilityReader?.Dispose();
            _practiceRepository?.Dispose();
        }
    }
}
