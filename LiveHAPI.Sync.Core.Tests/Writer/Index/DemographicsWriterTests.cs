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
using LiveHAPI.Sync.Core.Interface.Writers.Index;
using LiveHAPI.Sync.Core.Loader;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Writer;
using LiveHAPI.Sync.Core.Writer.Index;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Writer.Index
{
    [TestFixture]
    public class DemographicsWriterTests
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private readonly bool goLive = true;
        private LiveHAPIContext _context;
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;

        private IClientEncounterRepository _clientEncounterRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;

        private IIndexClientMessageLoader _clientMessageLoader;
        private IDemographicsWriter _clientMessageWriter;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            string connectionString=string.Empty;

            connectionString = config["connectionStrings:livehAPIConnection"];
            
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);

            _clientPretestStageRepository = new ClientPretestStageRepository(_context);
            _clientEncounterRepository = new ClientEncounterRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);
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

            _clientMessageWriter =
                new DemographicsWriter(new RestClient(_baseUrl), _clientMessageLoader,_clientStageRepository);
        }

        [Test]
        [Category("live")]
        public void should_Write_Clients()
        {
            var clientsResponses = _clientMessageWriter.Write().Result.ToList();

            foreach (var message in _clientMessageWriter.Messages) 
                Assert.False(string.IsNullOrWhiteSpace(message));

            var stagedIndexClients = _clientStageRepository.GetIndexClients();
            Assert.False(stagedIndexClients.Any());
            if (_clientMessageWriter.Errors.Any())
                foreach (var e in _clientMessageWriter.Errors)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(new string('*', 40));
                }

            foreach (var message in _clientMessageWriter.Messages)
            {
                Console.WriteLine(message);
                Console.WriteLine(new string('|', 40));
            }

            Assert.True(clientsResponses.Any());
            foreach (var response in clientsResponses)
            {
                Console.WriteLine(response);
            }
        }
    }
}