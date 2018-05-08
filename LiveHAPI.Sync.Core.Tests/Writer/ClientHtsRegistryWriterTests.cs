using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Writer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
            _clientHtsRegistryWriter=new ClientHtsRegistryWriter(new RestClient(_baseUrl),);
        }

        [Test]
        public void should_Write_Clients()
        {
            var clientsResponses = _clientHtsRegistryWriter.Write().Result.ToList();
            Assert.True(clientsResponses.Any());
            foreach (var response in clientsResponses)
            {
                Console.WriteLine(response);
            }
        }
    }
}