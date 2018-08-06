using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Service
{
    [TestFixture]
    public class ExtractClientsServiceTest
    {
        private readonly string _baseUrl = "http://localhost/iqcareapi";
        private LiveHAPIContext _context;
        private IUserRepository _repository;
        private IExtractClientsService _service;
        private IClientUserReader _reader;
        private IClientStageRepository _clientStageRepository;
        private IClientStageRelationshipRepository _clientStageRelationshipRepository;
        private IClientRepository _clientRepository;
        private IClientPretestStageRepository _clientPretestStageRepository;
        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            //var connectionString = config["connectionStrings:hAPIConnection"].Replace("#dir#", TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\"));
            
            var connectionString = config["connectionStrings:livehAPIConnection"];
            

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;
            _reader = new ClientUserReader(new RestClient(_baseUrl));
            _context = new LiveHAPIContext(options);
        
            _repository = new UserRepository(_context);

            _clientStageRepository = new ClientStageRepository(_context);
                _clientStageRelationshipRepository=new ClientStageRelationshipRepository(_context);
            _clientRepository=new ClientRepository(_context);
                _clientPretestStageRepository=new ClientPretestStageRepository(_context);
            var clientStageExtractor =
                new ClientStageExtractor(new PersonRepository(_context),new ClientStageRepository(_context),new SubscriberSystemRepository(_context), new ClientRepository(_context), new PracticeRepository(_context));
            var clientStageRelationshipExtractor =
                new ClientStageRelationshipExtractor(new ClientRelationshipRepository(_context),new ClientStageRelationshipRepository(_context),new SubscriberSystemRepository(_context)  );
            var  clientPretestStageExtractor =
                new ClientPretestStageExtractor(new ClientStageRepository(_context),new ClientPretestStageRepository(_context),new SubscriberSystemRepository(_context),new ClientEncounterRepository(_context),    new ClientRepository(_context));
            _service=new ExtractClientsService(clientStageExtractor,clientStageRelationshipExtractor,clientPretestStageExtractor);
        }

        [Test]
        public void should_Sync_Users()
        {
            var count=_service.Sync().Result;
            Assert.True(count>0);
            var clientStages = _clientStageRepository.GetAll();
            Assert.True(clientStages.Any(x=>x.SyncStatus == SyncStatus.Staged));
            
            var clientRelStages = _clientStageRelationshipRepository.GetAll();
            Assert.True(clientRelStages.Any());
            
            var clients = _clientRepository.GetAll();
            Assert.True(clients.Any(x=>x.SyncStatus==SyncStatus.Synced));

            var pretests = _clientPretestStageRepository.GetAll();
            Assert.True(pretests.Any());
            
            Console.WriteLine($"synced {count}");
        }
    }
}