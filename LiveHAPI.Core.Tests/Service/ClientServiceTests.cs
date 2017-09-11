using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class ClientServiceTests
    {
        private LiveHAPIContext _context;
        private IClientService _clientService;

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

            _context = new LiveHAPIContext(options);
            TestData.Init();
            TestDataCreator.Init(_context);

            _clientService = new ClientService(new PracticeRepository(_context), new PersonRepository(_context),
                new ClientRepository(_context));
        }

        [Test]
        public void should_Sync_New()
        {
            
        }
    }
}
