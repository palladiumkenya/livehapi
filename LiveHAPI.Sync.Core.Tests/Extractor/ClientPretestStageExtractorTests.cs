using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Extractor
{
    public class ClientPretestStageExtractorTests
    {
        private IClientEncounterRepository _clientEncounterRepository;
        private  IClientStageRepository _clientStageRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;
        private  ISubscriberSystemRepository _subscriberSystemRepository;
        private IClientPretestStageExtractor _clientPretestStageExtractor;
        private LiveHAPIContext _context;
        private Person person;
        private SubscriberSystem subscriber;


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

            _clientEncounterRepository = new ClientEncounterRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _clientPretestStageRepository = new ClientPretestStageRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);

            _clientPretestStageExtractor =
                new ClientPretestStageExtractor(_clientStageRepository, _clientPretestStageRepository,
                    _subscriberSystemRepository, _clientEncounterRepository);

        }
//
//        [Test]
//        public void should_Extract_Translated()
//        {
//           
//        }
        [Test]
        public void should_Extract()
        {
            var clients = _clientPretestStageExtractor.Extract().Result.ToList();
            Assert.True(clients.Count>0);
            foreach (var clientStage in clients)
            {
                Console.WriteLine(clientStage);
            }
        }
    }
}