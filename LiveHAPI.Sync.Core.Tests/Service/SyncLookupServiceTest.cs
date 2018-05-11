using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
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
    public class SyncLookupServiceTest
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private LiveHAPIContext _context;
        private ISubscriberTranslationRepository _repository;
        private ISyncLookupService _service;
        private IClientLookupReader _reader;

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
            _reader = new ClientLookupReader(new RestClient(_baseUrl));
            _context = new LiveHAPIContext(options);
          
            _repository = new SubscriberTranslationRepository(_context);
            _service=new SyncLookupService(_reader,_repository);
        }

        [Test]
        public void should_Sync_Lookups()
        {
            var count=_service.Sync().Result;
            Assert.True(count>0);
            Console.WriteLine($"synced {count}");
        }
    }
}