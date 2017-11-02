using System;
using System.Linq;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Repository.Subscriber;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;


namespace LiveHAPI.IQCare.Core.Handlers
{
    public class ClientSavedHandler:IClientSavedHandler
    {
        private readonly IConfigRepository _configRepository;
        private readonly IPatientRepository _patientRepository;


        public ClientSavedHandler(IPatientRepository patientRepository,IConfigRepository configRepository)
        {
            _patientRepository = patientRepository;
         
            _configRepository = configRepository;
        }

        public void Handle(ClientSaved args,SubscriberSystem subscriberSystem)
        {
            var location = _configRepository.GetLocations().FirstOrDefault();
            var patient = Patient.Create(args.Client, location.FacilityID, subscriberSystem);
            
            _patientRepository.CreateOrUpdate(patient, subscriberSystem, location);

            if (args.Client.HasRelationships())
                _patientRepository.CreateOrUpdateRelations(args.Client.Id, args.Client.Relationships, subscriberSystem,
                    location);


        }
    }
}