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

        public Group GetGroup(string name)
        {
           return GetGroups().FirstOrDefault(x => x.GroupName.ToLower().Trim() == name.ToLower().Trim());
        }

        public IEnumerable<Group> GetGroups()
        {
            return Context.Groups.Where(x => null == x.DeleteFlag || (null != x.DeleteFlag && x.DeleteFlag == 0));
        }

        public IEnumerable<UserGroup> GetUserGroups()
        {
            return Context.UserGroups.Where(x => null == x.DeleteFlag || (null != x.DeleteFlag && x.DeleteFlag == 0)); 
        }

        public IEnumerable<GroupFeature> GetGroupFeatures()
        {
            return Context.GroupFeatures;
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

        public void CreateOrUpdateGroup(Group g)
        {
            var exisitingGroup = GetGroups()
                .FirstOrDefault(x => x.GroupName.ToLower().Trim() == g.GroupName.ToLower().Trim());

            if (null != exisitingGroup)
            {
                exisitingGroup.UpdateTo(g);
                Context.Update(exisitingGroup);
            }
            else
            {
                Context.Groups.Add(g);
            }
            
            Context.SaveChanges();
        }

        public void CreateGroupFeature(string name, List<int> featureIds)
        {
            var newGroupFeatures = new List<GroupFeature>();
            var grp = GetGroup(name);
            if (null == grp)
                return;

            var groupFeatures = GroupFeature.Create(grp.GroupID, featureIds);
            foreach (var groupFeature in groupFeatures)
            {
                var exists = Context.GroupFeatures.Any(x =>
                    x.FeatureID == groupFeature.FeatureID && 
                    x.GroupID == groupFeature.GroupID &&
                    x.FunctionID == groupFeature.FunctionID);
                if (!exists)
                {
                    newGroupFeatures.Add(groupFeature);
                }
            }

            if (newGroupFeatures.Count > 0)
            {
                Context.AddRange(newGroupFeatures);
                Context.SaveChanges();
            }
        }

        public void AssignUsersToGroup(List<int> userIds, string name)
        {
            var newUserGroups = new List<UserGroup>();
            var grp = GetGroup(name);
            if (null == grp)
                return;

            var userGroups = UserGroup.Create(grp.GroupID, userIds);
            foreach (var userGroup in userGroups)
            {
                var exists = Context.UserGroups.Any(x =>
                    x.GroupID == userGroup.GroupID &&
                    x.UserID == userGroup.UserID);
                if (!exists)
                {
                    newUserGroups.Add(userGroup);
                }
            }

            if (newUserGroups.Count > 0)
            {
                Context.AddRange(newUserGroups);
                Context.SaveChanges();
            }
        }
    }
}
