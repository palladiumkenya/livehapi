using System;
using System.Linq;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ConfigRepositoryTests
    {
        private EMRContext  _context;
        private IConfigRepository _configRepository;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:EMRConnection"];

            var options = new DbContextOptionsBuilder<EMRContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new EMRContext(options);
            _context.ApplyMigrations();
            _context.UpdateTranslations();
        }

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:EMRConnection"];

            var options = new DbContextOptionsBuilder<EMRContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new EMRContext(options);
            _configRepository=new ConfigRepository(_context);
        }


        [Test]
        public void should_Get_Users()
        {
            var users = _configRepository.GetUsers().ToList();
            Assert.IsTrue(users.Count > 0);
            foreach (var user in users)
            {
                Console.WriteLine($"{user}");
            }
        }

        [Test]
        public void should_Get_Facilities()
        {
            var locations = _configRepository.GetLocations().ToList();
            Assert.IsTrue(locations.Count > 0);
            foreach (var location in locations)
            {
                Console.WriteLine($">.{location}");
            }
        }
        [Test]
        public void should_Get_Modules()
        {
            var modules = _configRepository.GetModules().ToList();
            Assert.IsTrue(modules.Count > 0);
            foreach (var module in modules)
            {
                Console.WriteLine($">.{module}");
            }
        }
        [Test]
        public void should_Get_Features()
        {
            var features = _configRepository.GetFeatures().ToList();
            Assert.IsTrue(features.Count > 0);
            foreach (var feature in features)
            {
                Console.WriteLine($">.{feature}");
            }
        }

        [Test]
        public void should_Get_VisitTypes()
        {
            var visitTypes = _configRepository.GetVisitTypes().ToList();
            Assert.IsTrue(visitTypes.Count > 0);
            foreach (var visitType in visitTypes)
            {
                Console.WriteLine($">.{visitType}");
            }
        }
    }
}