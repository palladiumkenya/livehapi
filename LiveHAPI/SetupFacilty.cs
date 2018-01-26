using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.IQCare.Core.Interfaces.Repository;

namespace LiveHAPI
{
    public class SetupFacilty : ISetupFacilty
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IConfigRepository _configRepository;

        public SetupFacilty(IPracticeRepository practiceRepository, IConfigRepository configRepository)
        {
            _practiceRepository = practiceRepository;
            _configRepository = configRepository;
        }

        public void SyncFacilities()
        {
            var locations = _configRepository.GetLocations().ToList();

            if (locations.Count == 1)
            {
                var practice = Mapper.Map<Practice>(locations.First());
                practice.IsDefault = true;
                _practiceRepository.Sync(practice);
                _practiceRepository.Save();
                
                return;
            }


            if (locations.Count > 1)
            {
                foreach (var location in locations)
                {
                    var practice = Mapper.Map<Practice>(locations.First());
                    practice.IsDefault = location.Preferred.HasValue && location.Preferred.Value == 1;
                    _practiceRepository.Sync(practice);
                    _practiceRepository.Save();
                }
            }
            
        }
    }
}