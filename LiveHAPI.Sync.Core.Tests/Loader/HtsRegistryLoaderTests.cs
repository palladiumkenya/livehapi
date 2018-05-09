using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Loader
{
    [TestFixture]
    public class HtsRegistryLoaderTests
    {
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;
        private LiveHAPIContext _context;
        private IHtsRegistryLoader _htsRegistryLoader;

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

            _practiceRepository = new PracticeRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _htsRegistryLoader = new HtsRegistryLoader(_clientStageRepository, _practiceRepository);
        }

        [Test]
        public void should_Load()
        {
            var htsRegistry = _htsRegistryLoader.Load();
            Assert.NotNull(htsRegistry);
            Assert.True(htsRegistry.CLIENTS.Any());
            Console.WriteLine(JsonConvert.SerializeObject(htsRegistry));
        }
    }
}