using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Loader
{
    [TestFixture]
    public class FamilyClientMessageLoaderTests
    {
        private LiveHAPIContext _context;
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;

        private IClientEncounterRepository _clientEncounterRepository;
        private IContactsEncounterRepository _contactsEncounterRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;

        private IFamilyClientMessageLoader _clientMessageLoader;
        private ClientStageExtractor _clientStageExtractor;
        private ClientPretestStageExtractor _clientPretestStageExtractor;
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
            _subscriberSystemRepository=new SubscriberSystemRepository(_context);
            _practiceRepository = new PracticeRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
      _contactsEncounterRepository=new ContactsEncounterRepository(_context);
            _clientMessageLoader =
                new FamilyClientMessageLoader(
                    
                    _practiceRepository, _clientStageRepository,new ClientStageRelationshipRepository(_context),
                    new ClientFamilyScreeningStageExtractor(_contactsEncounterRepository,_subscriberSystemRepository),
                                        new ClientFamilyTracingStageExtractor(_contactsEncounterRepository,_subscriberSystemRepository));

            _clientStageExtractor=new ClientStageExtractor(new PersonRepository(_context),_clientStageRepository,_subscriberSystemRepository );
            _clientPretestStageExtractor=new ClientPretestStageExtractor(_clientStageRepository,_clientPretestStageRepository,_subscriberSystemRepository,_clientEncounterRepository,new ClientRepository(_context));
            _clientStageRelationshipExtractor=new ClientStageRelationshipExtractor(new ClientRelationshipRepository(_context),new ClientStageRelationshipRepository(_context),_subscriberSystemRepository);
        }

        [Test]
        public void should_Load_By_Client()
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            var rels = _clientStageRelationshipExtractor.ExtractAndStage().Result;

            Assert.True(clients==1);
            Assert.True(pretests == 1);
            Assert.True(rels==1);

            var clientMessages = _clientMessageLoader.Load().Result.ToList();
            Assert.True(clientMessages.Any());
            var r = clientMessages.First();
            Console.WriteLine(JsonConvert.SerializeObject(r));
        }
    }
}