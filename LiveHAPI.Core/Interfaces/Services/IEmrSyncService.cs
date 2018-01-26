using System.Collections.Generic;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IEmrSyncService
    {
        void UpdateFacilties(IEnumerable<Practice> practices);
        void UpdateUsers(IEnumerable<User> users);
    }
}