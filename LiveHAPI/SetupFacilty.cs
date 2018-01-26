using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces;

namespace LiveHAPI
{
    public class SetupFacilty : ISetupFacilty
    {
        private readonly ISetupService _setupService;
        private readonly IConfigRepository _configRepository;
    
        public SetupFacilty(ISetupService setupService, IConfigRepository configRepository)
        {
            _setupService = setupService;
            _configRepository = configRepository;
        }

        public void SyncFacilities()
        {
            var locations = _configRepository.GetLocations().ToList();
            var practices = Mapper.Map<List<Practice>>(locations);
            _setupService.SyncFacilities(practices);
        }

        public void SyncUsers()
        {
            var users = _configRepository.GetUsers().ToList();
            var practiceUsers = Mapper.Map<List<Core.Model.People.User>>(users);
            _setupService.SyncUsers(practiceUsers);
        }
    }
}