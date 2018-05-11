using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Extractor
{
    public class ClientStageRelationshipExtractorTests
    {
         private  IClientRelationshipRepository _clientRelationshipRepository;
        private IClientStageRelationshipRepository _clientStageRelationshipRepository;
        private  ISubscriberSystemRepository _subscriberSystemRepository;
        private IClientStageRelationshipExtractor _clientStageRelationshipExtractor;
        private LiveHAPIContext _context;
  
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

             _clientRelationshipRepository = new ClientRelationshipRepository(_context);
            _clientStageRelationshipRepository = new ClientStageRelationshipRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);

            _clientStageRelationshipExtractor =
                new ClientStageRelationshipExtractor(_clientRelationshipRepository,_clientStageRelationshipRepository,_subscriberSystemRepository);

        }

        [Test]
        public void should_Extract()
        {
            var clients = _clientStageRelationshipExtractor.Extract().Result.ToList();
            Assert.True(clients.Count>0);
            foreach (var clientStage in clients)
            {
                Console.WriteLine(clientStage);
            }
        }
    }
}