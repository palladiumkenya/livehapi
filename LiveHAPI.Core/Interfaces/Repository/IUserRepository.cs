using System;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User,Guid>
    {
        User GetByUsername(string username);
        void Sync(User user);
    }
}