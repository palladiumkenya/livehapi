using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Network;

namespace LiveHAPI.Infrastructure.Repository
{
    public class MasterFacilityRepository:BaseRepository<MasterFacility,int>, IMasterFacilityRepository
    {
        public MasterFacilityRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}