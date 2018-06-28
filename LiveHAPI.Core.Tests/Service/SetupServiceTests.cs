using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class SetupServiceTests
    {
        private LiveHAPIContext _context;
        private IClientService _clientService;
        private List<ClientInfo> _clientInfos;
        private PracticeRepository _practiceRepository;
        private IPersonRepository _personRepository;
        private IClientRepository _clientRepository;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:hAPIConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
            TestData.Init();
            TestDataCreator.Init(_context);
            _clientInfos = TestData.TestClientInfos();
            _practiceRepository = new PracticeRepository(_context);
            _personRepository = new PersonRepository(_context);
            _clientRepository = new ClientRepository(_context);
            _clientService = new ClientService(_practiceRepository, new PersonRepository(_context),
                new ClientRepository(_context), new InvalidMessageRepository(_context));
        }

        [Test]
        public void should_Sync_Facilities()
        {
            var client = _clientInfos.Last();
            var prac = _practiceRepository.GetByCode(client.PracticeCode);


            _clientService.Sync(prac.Id, client);

            _personRepository = new PersonRepository(_context);
            var savedPerson = _personRepository.Get(client.Person.Id);
            Assert.IsNotNull(savedPerson);
            Assert.IsTrue(savedPerson.Names.Count > 0);
            Assert.IsTrue(savedPerson.Contacts.Count > 0);
            Assert.IsTrue(savedPerson.Addresses.Count > 0);
            Console.WriteLine(savedPerson);

            foreach (var name in savedPerson.Names)
            {
                Console.WriteLine($"  {name}");
            }
            foreach (var address in savedPerson.Addresses)
            {
                Console.WriteLine($"  {address}");
            }
            foreach (var contact in savedPerson.Contacts)
            {
                Console.WriteLine($"  {contact}");
            }
        }


        [Test]
        public void should_Sync_Update_Person()
        {
            var client = _clientInfos.First();            
            var c = _clientRepository.Get(client.Id);
            Assert.AreEqual(c.PersonId,client.Person.Id);

            
            client.Person.FirstName = "MAUN";
            client.Person.LastName = "MAUN L";

            _clientService.Sync(c.PracticeId, client);

            _personRepository = new PersonRepository(_context);
            var savedPerson = _personRepository.Get(client.Person.Id);
            Assert.IsNotNull(savedPerson);
            
            Assert.AreEqual("MAUN", savedPerson.Names.First().FirstName);
            Assert.AreEqual("MAUN L", savedPerson.Names.First().LastName);
            Console.WriteLine(savedPerson);

            foreach (var name in savedPerson.Names)
            {
                Console.WriteLine($"  {name}");
            }
            foreach (var address in savedPerson.Addresses)
            {
                Console.WriteLine($"  {address}");
            }
            foreach (var contact in savedPerson.Contacts)
            {
                Console.WriteLine($"  {contact}");
            }
        }

        [Test]
        public void should_Sync_New_Create_Client()
        {
            var client = _clientInfos.Last();
            var prac = _practiceRepository.GetByCode(client.PracticeCode);

            _clientService.Sync(prac.Id, client);
            _clientRepository = new ClientRepository(_context);

            var savedClient = _clientRepository.Get(client.Id);
            Assert.IsNotNull(savedClient);
            Console.WriteLine(savedClient);
        }

        [Test]
        public void should_Sync_Update_Client()
        {
            var client = _clientInfos.First();
            var c = _clientRepository.Get(client.Id);
            Assert.AreEqual(c.PersonId, client.Person.Id);

            client.KeyPop = "Fala";
            _clientService.Sync(client.PracticeId.Value, client);
            _clientRepository = new ClientRepository(_context);

            var savedClient = _clientRepository.Get(client.Id);
            Assert.IsNotNull(savedClient);
            Assert.AreEqual("Fala", savedClient.KeyPop);
            Console.WriteLine(savedClient);
        }
    }
}
