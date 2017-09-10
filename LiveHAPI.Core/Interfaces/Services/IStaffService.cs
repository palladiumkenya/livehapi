using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IStaffService
    {
        Person Find(Identity identity);
        User EnlistUser(Identity identity, PersonNameInfo personNameInfo, UserInfo userInfo, Guid practiceId);
        void SyncUser(User user);
    }
}