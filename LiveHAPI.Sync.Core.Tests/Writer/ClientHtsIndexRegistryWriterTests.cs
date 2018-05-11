using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
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
    public class ClientHtsIndexRegistryWriterTests
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private IClientPretestStageRepository _clientPretestStageRepository;
        private IClientEncounterRepository _clientEncounterRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private IPracticeRepository _practiceRepository;
        private IClientStageRepository _clientStageRepository;

        private LiveHAPIContext _context;
        private IHtsEncounterLoader _htsEncounterLoader;
        private IHtsIndexClientLoader _htsIndexClientLoader;
        private IClientHtsIndexRegistryWriter _clientHtsIndexRegistryWriter;

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
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);
            _practiceRepository = new PracticeRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _htsEncounterLoader = new HtsEncounterLoader(_clientEncounterRepository, _clientPretestStageRepository,
                _subscriberSystemRepository);
            _htsIndexClientLoader =
                new HtsIndexClientLoader(_practiceRepository, _clientStageRepository, _htsEncounterLoader);

            _clientHtsIndexRegistryWriter =
                new ClientHtsIndexRegistryWriter(new RestClient(_baseUrl), _htsIndexClientLoader);
        }

        [Test]
        public void should_Write_Clients()
        {
            var clientsResponses = _clientHtsIndexRegistryWriter.Write().Result.ToList();
            Assert.False(string.IsNullOrWhiteSpace(_clientHtsIndexRegistryWriter.Message));
            
            if (_clientHtsIndexRegistryWriter.Errors.Any())
                foreach (var e in _clientHtsIndexRegistryWriter.Errors)
                {
                    Console.WriteLine(e.ErrorMessage);

                    Console.WriteLine(new string('*',40));
                }
            Console.WriteLine(_clientHtsIndexRegistryWriter.Message);
            Assert.True(clientsResponses.Any());
            foreach (var response in clientsResponses)
            {
                Console.WriteLine(response);
            }
        }
    }
}