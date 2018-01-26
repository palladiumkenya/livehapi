using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(LiveHAPIContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return Context.Users.FirstOrDefault(x => x.UserName.ToLower().Trim() == username.ToLower().Trim());
        }

        public void Sync(User user)
        {
            var existingUser = GetByUsername(user.UserName);
            if (null != existingUser)
            {
                existingUser.UpdateTo(user);
                Update(existingUser);
            }
            else
            {
                Insert(user);
            }
        }
    }
}