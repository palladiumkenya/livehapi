using System;
using System.Collections.Generic;
using System.Dynamic;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IPatientEncounterRepository
    {        
        void CreateOrUpdate(List<EncounterInfo> encounters, SubscriberSystem subscriberSystem, Location location);
    }
}