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
    public class ContactsEncounterRepositoryTests
    {
        private readonly Guid _clientId=new Guid("2C0DA8D1-0E20-41A8-BD3B-A8DC01554752");
        private LiveHAPIContext _context;
        private IContactsEncounterRepository _contactsEncounterRepository;

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
            _contactsEncounterRepository = new ContactsEncounterRepository(_context);
        }

        [Test]
        public void should_Get_Family_Tracing_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetFamilyTracing(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Family_Tracing()
        {
            var encounters = _contactsEncounterRepository.GetFamilyTracing().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Family_Screening_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetFamilyScreening(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Family_Screening()
        {
            var encounters = _contactsEncounterRepository.GetFamilyScreening().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Partner_Tracing_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetPartnerTracing(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Partner_Tracing()
        {
            var encounters = _contactsEncounterRepository.GetPartnerTracing().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }
        [Test]
        public void should_Get_Partner_Screening_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetPartnerScreening(_clientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Partner_Screening()
        {
            var encounters = _contactsEncounterRepository.GetPartnerScreening().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsTraceResults).ToList();
            Assert.True(details.Any());
        }
    }
}