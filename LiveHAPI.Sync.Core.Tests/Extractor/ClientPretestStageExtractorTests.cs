using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
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
    public class ClientPretestStageExtractorTests
    {
        private IClientEncounterRepository _clientEncounterRepository;
        private  IClientStageRepository _clientStageRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;
        private  ISubscriberSystemRepository _subscriberSystemRepository;
        private IClientPretestStageExtractor _clientPretestStageExtractor;
        private LiveHAPIContext _context;
        private ClientStageExtractor _clientStageExtractor;


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

            _clientEncounterRepository = new ClientEncounterRepository(_context);
            _clientStageRepository = new ClientStageRepository(_context);
            _clientPretestStageRepository = new ClientPretestStageRepository(_context);
            _subscriberSystemRepository = new SubscriberSystemRepository(_context);

            _clientPretestStageExtractor =
                new ClientPretestStageExtractor( _clientStageRepository, _clientPretestStageRepository,
                    _subscriberSystemRepository, _clientEncounterRepository, new ClientRepository(_context));


            var x=new ClientStageExtractor(new PersonRepository(_context), _clientStageRepository,
                _subscriberSystemRepository,
                new ClientRepository(_context)
                ).Extract().Result;

           _clientStageExtractor= new ClientStageExtractor(new PersonRepository(_context), _clientStageRepository,
                _subscriberSystemRepository,new ClientRepository(_context));
        }

        [Test]
        public void should_Extract()
        {
            var clients = _clientPretestStageExtractor.Extract().Result.ToList();
            Assert.True(clients.Any());
            foreach (var clientStage in clients)
            {
                Console.WriteLine(clientStage);
            }
        }

        [Test]
        public void should_ExtractAndStage()
        {
            var inital= _clientStageExtractor.ExtractAndStage().Result;
            Assert.True(inital == 1);

            var clients = _clientPretestStageExtractor.ExtractAndStage().Result;
            Assert.True(clients == 1);
            var stagedClients = _clientPretestStageRepository.GetAll().ToList();
            Assert.True(stagedClients.Count > 0);
            foreach (var clientStage in stagedClients)
            {
                Console.WriteLine(clientStage);
            }
        }
    }
}