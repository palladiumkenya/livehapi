using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;

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
            var clientUsers = await _clientUserReader.Read();

            var users = Mapper.Map<List<User>>(clientUsers);
            int count = users.Count;
            _userRepository.Sync(users);
            return count;
        }
    }
}