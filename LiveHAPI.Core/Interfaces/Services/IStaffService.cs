using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IStaffService
    {
        Person Find(PersonInfo personInfo);

        User EnlistUser(UserInfo userInfo, Guid practiceId);
        IEnumerable<User> EnlistUsers(string practiceCode,IEnumerable<UserInfo> userInfos);

        Provider EnlistProvider(ProviderInfo providerInfo, Guid practiceId);
        IEnumerable<Provider> EnlistProviders(string practiceCode, IEnumerable<ProviderInfo> providerInfos);
    }
}