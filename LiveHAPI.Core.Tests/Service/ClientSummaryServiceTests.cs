using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class ClientSummaryServiceTests
    {
        private LiveHAPIContext _context;
        private IClientService _clientService;
        private List<ClientInfo> _clientInfos;
        private PracticeRepository _practiceRepository;
        private IPersonRepository _personRepository;
        private IClientRepository _clientRepository;
        private IClientSummaryService _clientSummaryService;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:realConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
         
            _practiceRepository = new PracticeRepository(_context);
            _personRepository = new PersonRepository(_context);
            _clientRepository = new ClientRepository(_context);
            _clientService = new ClientService(_practiceRepository, new PersonRepository(_context),
                new ClientRepository(_context));

            _clientSummaryService=new ClientSummaryService(new ItemRepository(_context));
        }

        [Test]
        public void should_Generate_Client_Summary()
        {
            /*
                3DF14C84-C6B1-4B41-840E-A89600CE6ED8
                
             */
            var personMatches = _clientService.FindById(new Guid("7314B45C-2D46-47B1-8B0C-A89600CDBE32"));
            Assert.NotNull(personMatches);
            var client = personMatches.FirstOrDefault().Person.Clients.FirstOrDefault();
            Assert.NotNull(client);

            var summary = _clientSummaryService.Generate(client).ToList();
            Assert.True(summary.Count>0);
            foreach (var clientSummary in summary)
            {
                Console.WriteLine(clientSummary);
            }
        }
    }
}