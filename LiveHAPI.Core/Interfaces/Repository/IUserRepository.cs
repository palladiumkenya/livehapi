using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User,Guid>
    {
        int GetUserId(Guid id);
        User GetByUsername(string username);
        User GetByUser(string username);
        void Sync(User user);
        void Sync(IEnumerable<User> users);
    }
}