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
        private readonly Guid _indexClientId = new Guid("2C0DA8D1-0E20-41A8-BD3B-A8DC01554752");
        private readonly Guid _familyClientId=new Guid("5576248E-72B5-40B1-95CC-A8DC0156FAD7");
        private readonly Guid _partnerClientId = new Guid("1B26C3EC-5338-4A71-B919-A8DC01572E49");
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
            var encounters = _contactsEncounterRepository.GetFamilyTracing(_familyClientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsFamilyTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Family_Tracing()
        {
            var encounters = _contactsEncounterRepository.GetFamilyTracing().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsFamilyTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Family_Screening_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetFamilyScreening(_familyClientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsMemberScreenings).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Family_Screening()
        {
            var encounters = _contactsEncounterRepository.GetFamilyScreening().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsMemberScreenings).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Partner_Tracing_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetPartnerTracing(_partnerClientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsPartnerTraceResults).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Partner_Tracing()
        {
            var encounters = _contactsEncounterRepository.GetPartnerTracing().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsPartnerTraceResults).ToList();
            Assert.True(details.Any());
        }
        [Test]
        public void should_Get_Partner_Screening_By_Client()
        {
            var encounters = _contactsEncounterRepository.GetPartnerScreening(_partnerClientId).ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsPartnerScreenings).ToList();
            Assert.True(details.Any());
        }

        [Test]
        public void should_Get_Partner_Screening()
        {
            var encounters = _contactsEncounterRepository.GetPartnerScreening().ToList();
            Assert.True(encounters.Any());
            var details = encounters.SelectMany(x => x.ObsPartnerScreenings).ToList();
            Assert.True(details.Any());
        }
    }
}