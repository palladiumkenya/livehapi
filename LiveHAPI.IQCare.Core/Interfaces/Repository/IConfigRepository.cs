using System.Collections.Generic;
using LiveHAPI.IQCare.Core.Model;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IConfigRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Location> GetLocations();
        IEnumerable<Module> GetModules();
        IEnumerable<Feature> GetFeatures();
        IEnumerable<VisitType> GetVisitTypes();
    }
}