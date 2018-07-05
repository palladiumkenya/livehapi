using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Extractor
{
    public class ClientStageExtractorTests
    {
        private  IPersonRepository _personRepository;
        private  IClientStageRepository _clientStageRepository;
        private  ISubscriberSystemRepository _subscriberSystemRepository;
        private IClientStageExtractor _clientStageExtractor;
        private LiveHAPIContext _context;
        private Person person;
        private SubscriberSystem subscriber;


        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //var connectionString = config["connectionStrings:hAPIConnection"].Replace("#dir#", TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\"));
            var connectionString = config["connectionStrings:livehAPIConnection"];
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);

            _personRepository = new PersonRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);

            _clientStageExtractor =
                new ClientStageExtractor(_personRepository, _clientStageRepository, _subscriberSystemRepository,new ClientRepository(_context));

            subscriber = Builder<SubscriberSystem>.CreateNew()
                .With(x => x.Id = new Guid("16E23877-9D69-11E7-ABC4-CEC278B6B50A"))
                .With(x => x.Translations = TestData.TestTranslations())
                .Build();
            person = Builder<Person>.CreateNew().With(x => x.Gender = "F").Build();
            var client = Builder<Client>.CreateNew()
                .With(x => x.KeyPop = "O")
                .With(x => x.MaritalStatus = "S")
                .Build();
            person.Clients.Add(client);
        }

        [Test]
        public void should_Extract_Translated()
        {
            var stage = ClientStage.Create(person, subscriber);
            Assert.AreEqual(52, stage.Sex);
            Assert.AreEqual(25, stage.KeyPop);
            Assert.AreEqual(58, stage.MaritalStatus);
            Console.WriteLine(stage);

                                                                /*
            Code|Display|SubCode|SubDisplay|SubRef
            ---------------------------------------------------
            O   |Others	|25	    |Other     |HTSKeyPopulation
            S   |Single	|58	    |Single	   |HTSMaritalStatus
            F   |F	    |52	    |Female    |Gender
             
*/
        }
        [Test]
        public void should_Extract()
        {
            var clients = _clientStageExtractor.Extract().Result.ToList();
            Assert.True(clients.Count>0);
            foreach (var clientStage in clients)
            {
                Assert.True(clientStage.RegistrationDate>DateTime.MinValue);
                Console.WriteLine(clientStage);
            }
        }

        [Test]
        public void should_ExtractAndStage()
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            Assert.True(clients==1);
            var stagedClients = _clientStageRepository.GetAll().ToList();
            Assert.True(stagedClients.Count>0);
            foreach (var clientStage in stagedClients)
            {
                Console.WriteLine(clientStage);
            }
        }
    }
}