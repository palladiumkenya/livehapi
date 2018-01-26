using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Service
{
    public class SetupService: ISetupService
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IUserRepository _userRepository;

        public SetupService(IPracticeRepository practiceRepository, IUserRepository userRepository)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
        }

        public void SyncFacilities(IEnumerable<Practice> practices)
        {
            var toSync = practices.ToList();

            if (toSync.Count == 1)
            {
                var prac = toSync.First();
                prac.IsDefault = true;
                _practiceRepository.Sync(prac);
                return;
            }

            if (toSync.Count > 1)
            {
                var defualtPrac = toSync.FirstOrDefault(x => x.IsDefault);
                if (null != defualtPrac)
                {
                    foreach (var practice in toSync)
                    {
                        practice.IsDefault = false;
                    }
                    defualtPrac.IsDefault = true;
                }
                else
                {
                    defualtPrac = toSync.First();
                    defualtPrac.IsDefault = true;
                }

                foreach (var practice in toSync)
                {
                    _practiceRepository.Sync(practice);
                }
                
            }

          
        }

        public void SyncUsers(IEnumerable<User> practiceUsers)
        {
            var toSync = practiceUsers.ToList();
            foreach (var user in toSync)
            {
                _userRepository.Sync(user);
            }
            _userRepository.Save();

        }
    }
}