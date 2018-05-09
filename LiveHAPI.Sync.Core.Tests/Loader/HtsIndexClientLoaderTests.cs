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
    public class HtsIndexClientLoaderTests
    {
       private readonly Guid _clientId=new Guid("fae99d4a-8f7a-42c4-b43e-a8c9008c66ac");
        private  IClientPretestStageRepository _clientPretestStageRepository;
        private  IClientEncounterRepository _clientEncounterRepository;
        private  ISubscriberSystemRepository _subscriberSystemRepository;
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;

        private LiveHAPIContext _context;
        private IHtsEncounterLoader _htsEncounterLoader;
        private IHtsIndexClientLoader _htsIndexClientLoader;

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
            _practiceRepository = new PracticeRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _htsEncounterLoader = new HtsEncounterLoader(_clientEncounterRepository,_clientPretestStageRepository,_subscriberSystemRepository);
            _htsIndexClientLoader=new HtsIndexClientLoader(_practiceRepository,_clientStageRepository,_htsEncounterLoader);
        }

        [Test]
        public void should_Load_By_Client()
        {
            var registries = _htsIndexClientLoader.LoadAll().ToList();
            Assert.True(registries.Any());
            var r = registries.First();
            Console.WriteLine(JsonConvert.SerializeObject(r));
        }
    }
}