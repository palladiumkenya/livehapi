using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ClientEncounterRepositoryTests
    {
        private readonly Guid _clientId=new Guid("2C0DA8D1-0E20-41A8-BD3B-A8DC01554752");
        private LiveHAPIContext _context;
        private IClientEncounterRepository _clientRepository;

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
            _clientRepository = new ClientEncounterRepository(_context);
        }

       
        [Test]
        public void should_Get_ReferralLinkage_By_Client()
        {
            var encounters = _clientRepository.GetReferralLinkage(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsLinkages).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Tracing_By_Client()
        {
            var encounters = _clientRepository.GetTracing(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Testing_By_Client()
        {
            var encounters = _clientRepository.GetTesting(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTestResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_FinalTesting_By_Client()
        {
            var encounters = _clientRepository.GetFinalTesting(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsFinalTestResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Pretest_By_Client()
        {
            var encounters = _clientRepository.GetPretest(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.Obses).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_ReferralLinkage()
        {
            var encounters = _clientRepository.GetReferralLinkage().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsLinkages).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Tracing()
        {
            var encounters = _clientRepository.GetTracing().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Testing()
        {
            var encounters = _clientRepository.GetTesting().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTestResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_FinalTesting()
        {
            var encounters = _clientRepository.GetFinalTesting().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsFinalTestResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Pretest()
        {
            var encounters = _clientRepository.GetPretest().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.Obses).ToList();
            Assert.True(details.Any());
        }
    }
}