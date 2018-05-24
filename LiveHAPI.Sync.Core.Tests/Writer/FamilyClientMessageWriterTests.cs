using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Loader;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Writer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Writer
{
    [TestFixture]
    public class FamilyClientMessageWriterTests
    {
        private readonly string _baseUrl = "http://localhost:3333";

        private LiveHAPIContext _context;
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;

        private IClientEncounterRepository _clientEncounterRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;

        private IFamilyClientMessageLoader _clientMessageLoader;
        private IFamilyClientMessageWriter _clientMessageWriter;
        private ClientStageExtractor _clientStageExtractor;
        private ClientPretestStageExtractor _clientPretestStageExtractor;
        private ContactsEncounterRepository _contactsEncounterRepository;
        private ClientStageRelationshipExtractor _clientStageRelationshipExtractor;


        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:hAPIConnection"].Replace("#dir#", TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\"));
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);


            _clientPretestStageRepository = new ClientPretestStageRepository(_context);
            _clientEncounterRepository = new ClientEncounterRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);
            _practiceRepository = new PracticeRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);

            _clientStageExtractor = new ClientStageExtractor(new PersonRepository(_context), _clientStageRepository, _subscriberSystemRepository,new ClientRepository(_context));
            _clientPretestStageExtractor = new ClientPretestStageExtractor(_clientStageRepository, _clientPretestStageRepository, _subscriberSystemRepository, _clientEncounterRepository, new ClientRepository(_context));

            _contactsEncounterRepository = new ContactsEncounterRepository(_context);

            _clientMessageLoader =
                new FamilyClientMessageLoader(

                    _practiceRepository, _clientStageRepository, new ClientStageRelationshipRepository(_context),
                    new ClientFamilyScreeningStageExtractor(_contactsEncounterRepository, _subscriberSystemRepository),
                    new ClientFamilyTracingStageExtractor(_contactsEncounterRepository, _subscriberSystemRepository));

            _clientMessageWriter =
                new FamilyClientMessageWriter(new RestClient(_baseUrl), _clientMessageLoader,_clientStageRepository);
            _clientStageRelationshipExtractor = new ClientStageRelationshipExtractor(new ClientRelationshipRepository(_context), new ClientStageRelationshipRepository(_context), _subscriberSystemRepository);


        }

        [Test]
        public void should_Write_Clients()
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            var rels = _clientStageRelationshipExtractor.ExtractAndStage().Result;

            var clientsResponses = _clientMessageWriter.Write().Result.ToList();

            foreach (var message in _clientMessageWriter.Messages)
                Assert.False(string.IsNullOrWhiteSpace(message));
            
          

            if (_clientMessageWriter.Errors.Any())
                foreach (var e in _clientMessageWriter.Errors)
                {
                    Console.WriteLine(e.Message);

                    Console.WriteLine(new string('*', 40));
                }

            foreach (var message in _clientMessageWriter.Messages)
                Console.WriteLine(message);
            Assert.True(clientsResponses.Any());
            foreach (var response in clientsResponses)
            {
                Console.WriteLine(response);
            }
        }
        
        [TestCase(LoadAction.RegistrationOnly)]
        [TestCase(LoadAction.ContactScreenig)]
        [TestCase(LoadAction.ContactScreenig,LoadAction.ContactTracing)]
        [TestCase(LoadAction.ContactTracing)]
        public void should_Write_Client_By_Actions(params LoadAction[] actions)
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            var rels = _clientStageRelationshipExtractor.ExtractAndStage().Result;

            var clientsResponses = _clientMessageWriter.Write(actions).Result.ToList();

            foreach (var message in _clientMessageWriter.Messages)
                Assert.False(string.IsNullOrWhiteSpace(message));

            if (_clientMessageWriter.Errors.Any())
                foreach (var e in _clientMessageWriter.Errors)
                {
                    Console.WriteLine(e.Message);

                    Console.WriteLine(new string('*', 40));
                }

            foreach (var message in _clientMessageWriter.Messages)
                Console.WriteLine(message);
            Assert.True(clientsResponses.Any());
            foreach (var response in clientsResponses)
            {
                Console.WriteLine(response);
            }
        }
        
       
    }
}