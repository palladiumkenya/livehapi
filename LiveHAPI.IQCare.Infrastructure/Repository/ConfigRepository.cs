using System.Collections.Generic;
using System.Linq;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;

namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class ConfigRepository : BaseRepository, IConfigRepository
    {
        public ConfigRepository(EMRContext context) : base(context)
        {
        }

        public IEnumerable<User> GetUsers()
        {
            return Context.Users.Where(x => null == x.DeleteFlag || (null != x.DeleteFlag && x.DeleteFlag == 0));
        }

        public IEnumerable<Location> GetLocations()
        {
            return Context.Locations.Where(x => x.DeleteFlag == 0);
        }
        public IEnumerable<Module> GetModules()
        {
            return Context.Modules.Where(x => x.DeleteFlag ==false);
        }

        public IEnumerable<Feature> GetFeatures()
        {
            return Context.Features.Where(x => x.DeleteFlag == 0);
        }

        public IEnumerable<VisitType> GetVisitTypes()
        {
            return Context.VisitTypes.Where(x => x.DeleteFlag == 0);
        }
    }
}
