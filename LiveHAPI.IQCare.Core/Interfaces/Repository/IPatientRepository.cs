using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IPatientRepository
    {
      void  CreateOrUpdate(Patient patient, SubscriberSystem subscriberSystem, Location location);
    }
}