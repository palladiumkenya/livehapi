using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Loader
{
    [TestFixture]
    public class HtsEncounterLoaderTests
    {
       private readonly Guid _clientId=new Guid("fae99d4a-8f7a-42c4-b43e-a8c9008c66ac");
        private  IClientPretestStageRepository _clientPretestStageRepository;
        private  IClientEncounterRepository _clientEncounterRepository;
        private  ISubscriberSystemRepository _subscriberSystemRepository;

        private LiveHAPIContext _context;
        private IHtsEncounterLoader _htsEncounterLoader;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:livehAPIConnection"];
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);

            _clientPretestStageRepository = new ClientPretestStageRepository(_context);
            _clientEncounterRepository = new ClientEncounterRepository(_context);
            _subscriberSystemRepository=new SubscriberSystemRepository(_context);
            _htsEncounterLoader = new HtsEncounterLoader(_clientEncounterRepository,_clientPretestStageRepository,_subscriberSystemRepository);
        }

        [Test]
        public void should_Load_By_Client()
        {
            var htsEncounter = _htsEncounterLoader.Load(_clientId);
            Assert.NotNull(htsEncounter);
            Assert.True(htsEncounter.HIV_TESTS.SCREENING.Any());
            Assert.True(htsEncounter.HIV_TESTS.CONFIRMATORY.Any());
            Assert.True(htsEncounter.TRACING.Any());
            Assert.NotNull(htsEncounter.PLACER_DETAIL);
            Assert.NotNull(htsEncounter.PRE_TEST);
            Assert.NotNull(htsEncounter.REFERRAL);
            Assert.NotNull(htsEncounter.LINKAGE);
            Console.WriteLine(JsonConvert.SerializeObject(htsEncounter));
        }
    }
}