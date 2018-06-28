using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.ValueObject;
using Newtonsoft.Json;
using Serilog;

namespace LiveHAPI.Core.Service
{
    public class ClientService:IClientService
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IInvalidMessageRepository _invalidMessageRepository;
        
        public ClientService(IPracticeRepository practiceRepository, IPersonRepository personRepository, IClientRepository clientRepository, IInvalidMessageRepository invalidMessageRepository)
        {
            _practiceRepository = practiceRepository;
            _personRepository = personRepository;
            _clientRepository = clientRepository;
            _invalidMessageRepository = invalidMessageRepository;
        }

        public IEnumerable<PersonMatch> FindById(Guid id)
        {
            return _clientRepository.GetById(id);
        }

        public IEnumerable<PersonMatch> SearchById(string searchItem)
        {
            if (!string.IsNullOrWhiteSpace(searchItem))
                return _clientRepository.Search(searchItem).ToList();

            return new List<PersonMatch>();
        }

        public IEnumerable<PersonMatch> SearchByName(string searchItem)
        {
            if (!string.IsNullOrWhiteSpace(searchItem))
                return _personRepository.Search(searchItem).ToList();

            return new List<PersonMatch>();
        }

        public IEnumerable<PersonMatch> LoadByCohort(SubscriberCohort cohort)
        {
            return _personRepository.GetByCohort(cohort);
        }

        public IEnumerable<Encounter> LoadEncounters(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Sync(Guid practiceId, ClientInfo client)
        {
            var practice = _practiceRepository.Get(practiceId);

            if (null == practice)
                throw new ArgumentException("Facility is not Registered!");

            //person

            var personInfo = client.Person;
            

            var exisitngPerson = _personRepository.Get(personInfo.Id);

            if (null == exisitngPerson)
            {
                var person = Person.CreateClient(personInfo);
                _personRepository.Insert(person);
                _personRepository.Save();

                //client
                var cient = Client.Create(client, practiceId, person.Id);
                _clientRepository.Insert(cient);
                _clientRepository.Save();
            }
            else
            {
                exisitngPerson.UpdateClient(personInfo);
                _personRepository.Update(exisitngPerson);
                _personRepository.Save();

                var existingClient = exisitngPerson.Clients.FirstOrDefault(x => x.Id == client.Id);

                if (null != existingClient)
                {
                    existingClient.Update(client);
                    _clientRepository.Update(existingClient);
                }
                else
                {
                    var cient = Client.Create(client, practiceId, exisitngPerson.Id);
                    _clientRepository.Insert(cient);
                }

                _clientRepository.Save();
            }
        }

        public void SyncClient(ClientInfo client)
        {
            var personInfo = client.Person;

            var exisitngPerson = _personRepository.GetDemographics(personInfo.Id);

            if (null == exisitngPerson)
            {
                var person = Person.CreateClient(personInfo);
                _personRepository.Insert(person);
                _personRepository.Save();

                //client
                var cient = Client.Create(client, client.PracticeId.Value, person.Id);
                cient.SyncStatus = SyncStatus.Staged;
                cient.SyncStatusDate = DateTime.Now;
                _clientRepository.Insert(cient);
                _clientRepository.Save();
            }
            else
            {
                exisitngPerson.UpdateClient(personInfo);
                _personRepository.Update(exisitngPerson);
                _personRepository.Save();

                var existingClient = _clientRepository.GetClient(client.Id, false);

                if (null != existingClient)
                {
                    existingClient.Update(client);
                    var clientToUpdate = existingClient;
                    clientToUpdate.Identifiers = new List<ClientIdentifier>();
                    clientToUpdate.SyncStatus = SyncStatus.Staged;
                    clientToUpdate.SyncStatusDate = DateTime.Now;
                    _clientRepository.Update(clientToUpdate);
                    _clientRepository.UpdateIds(existingClient.Identifiers.ToList());
                }
                else
                {
                    var cient = Client.Create(client, client.PracticeId.Value, exisitngPerson.Id);
                    _clientRepository.Insert(cient);
                }
                _clientRepository.Save();
            }
        }

        public void SmartSync(ClientInfo client)
        {
            // IDS
            var clientIdentifiers = ClientIdentifier.Create(client.Identifiers);
            client.Identifiers = new List<IdentifierInfo>();

            // RELATIONSHIPS
            var clientRelationships = ClientRelationship.Create(client.Relationships);
            client.Relationships = new List<RelationshipInfo>();
            
            //STATES
            var states = client.ClientStates;
            client.ClientStates=new List<ClientStateInfo>();

            // PERSON 
            var personInfo = client.Person;
            var exisitngPerson = _personRepository.GetDemographics(personInfo.Id);

            try
            {
                if (null == exisitngPerson)
                {
                    var person = Person.CreateClient(personInfo);
                    _personRepository.Insert(person);
                    _personRepository.Save();

                    //client
                    var cient = Client.Create(client, client.PracticeId.Value, person.Id);


                    _clientRepository.Insert(cient);
                    _clientRepository.Save();
                }
                else
                {
                    exisitngPerson.UpdateClient(personInfo);
                    _personRepository.Update(exisitngPerson);
                    _personRepository.Save();

                    var existingClient = _clientRepository.GetClient(client.Id, false);

                    if (null != existingClient)
                    {
                        existingClient.Update(client);
                        _clientRepository.Update(existingClient);
                        _clientRepository.Save();
                    }
                    else
                    {
                        var cient = Client.Create(client, client.PracticeId.Value, exisitngPerson.Id);

                        _clientRepository.Insert(cient);
                        _clientRepository.Save();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error saving encounter");
                Preserve(client);
            }
            
           
            //STATES
            var allstates = Mapper.Map<List<ClientState>>(states);
            _clientRepository.UpdateClientState(client.Id, allstates);

            //IDS
            if (clientIdentifiers.Any())
            {
                _clientRepository.UpdateIds(clientIdentifiers);
               // _clientRepository.Save();
            }

            // RELATIONSHIPS
            if (clientRelationships.Any())
            {
                _clientRepository.UpdateTempRelations(clientRelationships);
                _clientRepository.UpdateRelationships();
             }
        }

        public void Preserve(ClientInfo client)
        {
            try
            {
                _invalidMessageRepository.Insert(new InvalidMessage(client.Id, MessageType.Client, JsonConvert.SerializeObject(client), client.PracticeId));
                _invalidMessageRepository.Save();
            }
            catch (Exception e)
            {
                Log.Error(e, "Preserve error");
            }
            
        }
    }
}