using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.ValueModel;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IStaffService
    {
        Person Find(PersonIdentity personIdentity);
        User EnlistUser(PersonIdentity personIdentity, PersonNameIdentity personNameIdentity, UserIdentity userIdentity);
    }
}