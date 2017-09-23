using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Service
{
    public class ClientService:IClientService
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IClientRepository _clientRepository;
        
        public ClientService(IPracticeRepository practiceRepository, IPersonRepository personRepository, IClientRepository clientRepository)
        {
            _practiceRepository = practiceRepository;
            _personRepository = personRepository;
            _clientRepository = clientRepository;
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
                _clientRepository.Insert(cient);
                _clientRepository.Save();
            }
            else
            {
                exisitngPerson.UpdateClient(personInfo);
                _personRepository.Update(exisitngPerson);
                _personRepository.Save();

                var existingClient = _clientRepository.GetClient(client.Id);

                if (null != existingClient)
                {
                    existingClient.Update(client);
                    _clientRepository.Update(existingClient);
                }
                else
                {
                    var cient = Client.Create(client, client.PracticeId.Value, exisitngPerson.Id);
                    _clientRepository.Insert(cient);
                }
                _clientRepository.Save();
            }
        }
    }
}