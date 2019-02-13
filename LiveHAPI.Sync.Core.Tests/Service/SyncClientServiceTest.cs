using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Loader;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using LiveHAPI.Sync.Core.Writer;
using LiveHAPI.Sync.Core.Writer.Index;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Service
{
    [TestFixture]
    public class SyncClientServiceTest
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private LiveHAPIContext _context;
        private IUserRepository _repository;
        private ISyncClientsService _service;
        private IClientUserReader _reader;

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
        
           var clientPretestStageRepository = new ClientPretestStageRepository(_context);
            var clientEncounterRepository = new ClientEncounterRepository(_context);
            var  subscriberSystemRepository = new SubscriberSystemRepository(_context);
            var  practiceRepository = new PracticeRepository(_context);
            var  clientStageRepository = new ClientStageRepository(_context);

            var  clientStageExtractor = new ClientStageExtractor(new PersonRepository(_context), clientStageRepository, subscriberSystemRepository,new ClientRepository(_context), new PracticeRepository(_context));
            var clientPretestStageExtractor = new ClientPretestStageExtractor(clientStageRepository, clientPretestStageRepository, subscriberSystemRepository, clientEncounterRepository, new ClientRepository(_context));
            var contactsEncounterRepository = new ContactsEncounterRepository(_context);
            
           var clientMessageLoader =
                new IndexClientMessageLoader(practiceRepository, clientStageRepository, clientPretestStageRepository,
                    new ClientTestingStageExtractor(clientEncounterRepository, subscriberSystemRepository),
                    new ClientFinalTestStageExtractor(clientEncounterRepository, subscriberSystemRepository),
                    new ClientReferralStageExtractor(clientEncounterRepository, subscriberSystemRepository),
                    new ClientTracingStageExtractor(clientEncounterRepository, subscriberSystemRepository),
                    new ClientLinkageStageExtractor(clientEncounterRepository, subscriberSystemRepository)

                );

            
           var clientMessageWriter =
                new IndexClientMessageWriter(new RestClient(_baseUrl), clientMessageLoader,clientStageRepository);
            
           var demographicsWriter =
               new DemographicsWriter(new RestClient(_baseUrl), clientMessageLoader,clientStageRepository);
            
          var cclientMessageLoader =
                new FamilyClientMessageLoader(

                    practiceRepository, clientStageRepository, new ClientStageRelationshipRepository(_context),
                    new ClientFamilyScreeningStageExtractor(contactsEncounterRepository, subscriberSystemRepository),
                    new ClientFamilyTracingStageExtractor(contactsEncounterRepository, subscriberSystemRepository));

           var cclientMessageWriter =
                new FamilyClientMessageWriter(new RestClient(_baseUrl), cclientMessageLoader,clientStageRepository);

            var ccclientMessageLoader =
                new PartnerClientMessageLoader(
                    practiceRepository, clientStageRepository, new ClientStageRelationshipRepository(_context),
                    new ClientPartnerScreeningStageExtractor(contactsEncounterRepository, subscriberSystemRepository),
                    new ClientPartnerTracingStageExtractor(contactsEncounterRepository, subscriberSystemRepository));

            var ccclientMessageWriter =
                new PartnerClientMessageWriter(new RestClient(_baseUrl), ccclientMessageLoader,clientStageRepository);
            
            _service=new SyncClientsService(clientMessageWriter,ccclientMessageWriter,cclientMessageWriter,demographicsWriter);
        }

        [Test]
        public void should_Sync_Users()
        {
            var count=_service.Sync().Result;
            Assert.True(count>0);
            Console.WriteLine($"synced {count}");
        }
    }
}