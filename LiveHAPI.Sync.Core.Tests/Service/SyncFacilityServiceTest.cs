using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Profiles;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Service
{
    [TestFixture]
    public class SyncFacilityServiceTest
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private LiveHAPIContext _context;
        private IPracticeRepository _repository;
        private ISyncFacilityService _service;
        private IClientFacilityReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<ClientProfile>(); });
        }
        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:hAPIConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;
            _reader = new ClientFacilityReader(new RestClient(_baseUrl));
            _context = new LiveHAPIContext(options);
            TestData.Init();
            TestDataCreator.Init(_context);
            _repository = new PracticeRepository(_context);
            _service=new SyncFacilityService(_reader,_repository);
        }

        [Test]
        public void should_Sync_Facilitys()
        {
            var count=_service.Sync().Result;
            Assert.True(count>0);
            Console.WriteLine($"synced {count}");
        }
    }
}