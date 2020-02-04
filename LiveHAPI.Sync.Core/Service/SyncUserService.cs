using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LiveHAPI.Sync.Core.Service
{
    public class SyncUserService : ISyncUserService
    {
        private readonly IClientUserReader _clientUserReader;
        private readonly IUserRepository _userRepository;


        public SyncUserService(IClientUserReader clientUserReader, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _clientUserReader = clientUserReader;
        }

        public async Task<int> Sync()
        {
            Log.Debug("Syncing users...");
            var clientUsers = await _clientUserReader.Read();

            var users = Mapper.Map<List<User>>(clientUsers);
            int count = users.Count;
            _userRepository.Sync(users);
            Log.Debug($"Synced {count} users!");
            return count;
        }


        public void Dispose()
        {
            _clientUserReader?.Dispose();
            _userRepository?.Dispose();
        }
    }
}
