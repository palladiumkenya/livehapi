using System;
using System.Collections.Generic;
using System.Dynamic;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IPStoreRepository
    {        
        void CreateOrUpdate(List<PSmartStoreInfo> storeInfos, SubscriberSystem subscriberSystem, Location location);
    }
}