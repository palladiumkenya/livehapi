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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Loader
{
    [TestFixture]
    public class IndexClientMessageLoaderTests
    {
        private readonly bool goLive = true;
        private LiveHAPIContext _context;
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;

        private IClientEncounterRepository _clientEncounterRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;

        private IIndexClientMessageLoader _clientMessageLoader;
        private ClientStageExtractor _clientStageExtractor;
        private ClientPretestStageExtractor _clientPretestStageExtractor;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString=string.Empty;

            if (goLive)
                connectionString = config["connectionStrings:livehAPIConnection"];
            else
                connectionString = config["connectionStrings:hAPIConnection"].Replace("#dir#",
                    TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\"));
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);

            _clientPretestStageRepository = new ClientPretestStageRepository(_context);
            _clientEncounterRepository = new ClientEncounterRepository(_context);
            _subscriberSystemRepository=new SubscriberSystemRepository(_context);
            _practiceRepository = new PracticeRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
      
            _clientMessageLoader =
                new IndexClientMessageLoader(_practiceRepository, _clientStageRepository, _clientPretestStageRepository,
                    new ClientTestingStageExtractor(_clientEncounterRepository, _subscriberSystemRepository),
                    new ClientFinalTestStageExtractor(_clientEncounterRepository, _subscriberSystemRepository),
                    new ClientReferralStageExtractor(_clientEncounterRepository, _subscriberSystemRepository),
                    new ClientTracingStageExtractor(_clientEncounterRepository, _subscriberSystemRepository),
                    new ClientLinkageStageExtractor(_clientEncounterRepository, _subscriberSystemRepository)

                );
            _clientStageExtractor=new ClientStageExtractor(new PersonRepository(_context),_clientStageRepository,_subscriberSystemRepository ,new ClientRepository(_context));
            _clientPretestStageExtractor=new ClientPretestStageExtractor(_clientStageRepository,_clientPretestStageRepository,_subscriberSystemRepository,_clientEncounterRepository,new ClientRepository(_context));

        }

        [Test]
        public void should_Load_By_Client()
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            Assert.True(clients==1);
            Assert.True(pretests == 1);

            var indexClientMessages = _clientMessageLoader.Load(null).Result.ToList();
            Assert.True(indexClientMessages.Any());
            foreach (var m in indexClientMessages)
            {
                Console.WriteLine(JsonConvert.SerializeObject(m));
                Console.WriteLine(new string('=',50));
            }
//            var r = indexClientMessages.First();
//            Console.WriteLine(JsonConvert.SerializeObject(r));
        }
        
        [TestCase(LoadAction.RegistrationOnly)]
        [TestCase(LoadAction.Pretest)]
        [TestCase(LoadAction.Pretest,LoadAction.Testing)]
        [TestCase(LoadAction.Pretest,LoadAction.Testing,LoadAction.Referral)]
        [TestCase(LoadAction.Pretest,LoadAction.Testing,LoadAction.Referral,LoadAction.Linkage)]
        [TestCase(LoadAction.Linkage)]
        [TestCase(LoadAction.Tracing)]
        public void should_Load_With_Actions_By_Client(params LoadAction[] actions)
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            Assert.True(clients==1);
            Assert.True(pretests == 1);

            var indexClientMessages = _clientMessageLoader.Load(null, actions).Result.ToList();
            Assert.True(indexClientMessages.Any());
            var r = indexClientMessages.First();
            //Assert.IsNull(r.CLIENTS.First().ENCOUNTER);
            Console.WriteLine(JsonConvert.SerializeObject(r));
        }
    }
}