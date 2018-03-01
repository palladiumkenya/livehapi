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
    public class SummaryServiceTests
    {
        private LiveHAPIContext _context;
        private IClientService _clientService;
        private List<ClientInfo> _clientInfos;
        private PracticeRepository _practiceRepository;
        private IPersonRepository _personRepository;
        private IClientRepository _clientRepository;
        private ISummaryService _summaryService;

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

            _summaryService=new SummaryService(new ItemRepository(_context),new ClientSummaryRepository(_context),new UserSummaryRepository(_context),new EncounterRepository(_context) );
        }

        [Test]
        public void should_Generate_Client_Summary()
        {
            /*
                50E56B13-BBB4-4E62-AA48-A896013F79C7
                045F0947-CD80-4E82-8E79-A896013FE740
             */
            
            var personMatches = _clientService.FindById(new Guid("045F0947-CD80-4E82-8E79-A896013FE740"));
            Assert.NotNull(personMatches);
            var client = personMatches.FirstOrDefault().Person.Clients.FirstOrDefault();
            Assert.NotNull(client);

            var summary = _summaryService.Generate(client).ToList();
            Assert.True(summary.Count>0);
            foreach (var clientSummary in summary)
            {
                Console.WriteLine(clientSummary);
            }
        }

        [Test]
        public void should_Generate_User_Summary()
        {
            /*
                61A9E04C-2ED0-414A-9387-A7B7016DF233
            */

            var summaries = _summaryService.Generate(new Guid("61A9E04C-2ED0-414A-9387-A7B7016DF233")).ToList();
            Assert.True(summaries.Count > 0);
            foreach (var userSummary in summaries)
            {
                Console.WriteLine(userSummary);
            }
        }
    }
}