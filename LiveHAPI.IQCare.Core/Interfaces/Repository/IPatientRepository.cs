using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IPatientRepository
    {
        Patient Get(Guid id);
        IEnumerable<Patient> GetAll(List<Guid> ids);
        void CreateOrUpdate(Patient patient, SubscriberSystem subscriberSystem, Location location);

        void CreateOrUpdateRelations(Guid patientId, List<RelationshipInfo> relatedPatients, SubscriberSystem subscriberSystem,
            Location location);
    }
}