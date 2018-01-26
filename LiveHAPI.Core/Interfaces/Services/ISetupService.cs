using System.Collections.Generic;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface ISetupService
    {
        void SyncFacilities(IEnumerable<Practice> practices);
        void SyncUsers(IEnumerable<User> practiceUsers);
    }
}