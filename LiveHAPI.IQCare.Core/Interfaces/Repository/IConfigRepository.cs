using System.Collections.Generic;
using LiveHAPI.IQCare.Core.Model;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IConfigRepository
    {
        IEnumerable<User> GetUsers();

        Group GetGroup(string name);
        IEnumerable<Group> GetGroups();
        IEnumerable<UserGroup> GetUserGroups();
        IEnumerable<GroupFeature> GetGroupFeatures();
        IEnumerable<Location> GetLocations();
        IEnumerable<Module> GetModules();
        IEnumerable<Feature> GetFeatures();
        IEnumerable<VisitType> GetVisitTypes();

        void CreateOrUpdateGroup(Group group);
        void CreateGroupFeature(string name, List<int> featureIds);
        void AssignUsersToGroup(List<int> userIds,string name);
    }
}