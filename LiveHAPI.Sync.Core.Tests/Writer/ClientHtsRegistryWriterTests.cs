using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
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
    public class ClientHtsRegistryWriterTests
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private IClientHtsRegistryWriter _clientHtsRegistryWriter;
        private IPersonRepository _personRepository;
        private IClientStageRepository _clientStageRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private IClientStageExtractor _clientStageExtractor;
        private IHtsRegistryLoader _htsRegistryLoader;
      
        private LiveHAPIContext _context;
   

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

            _personRepository = new PersonRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);

            _clientStageExtractor =
                new ClientStageExtractor(_personRepository, _clientStageRepository, _subscriberSystemRepository);
            _htsRegistryLoader=new HtsRegistryLoader(_clientStageRepository,new PracticeRepository(_context));
            _clientHtsRegistryWriter=new ClientHtsRegistryWriter(new RestClient(_baseUrl),_htsRegistryLoader);
        }

        [Test]
        public void should_Write_Clients()
        {
            var clientsResponses = _clientHtsRegistryWriter.Write().Result.ToList();
                Assert.False(string.IsNullOrWhiteSpace(_clientHtsRegistryWriter.Message));
            Console.WriteLine(_clientHtsRegistryWriter.Message);
            Assert.True(clientsResponses.Any());
            foreach (var response in clientsResponses)
            {
                Console.WriteLine(response);
            }
        }
    }
}