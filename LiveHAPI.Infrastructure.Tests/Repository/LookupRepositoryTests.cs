using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class LookupRepositoryTests
    {
        private LiveHAPIContext _context;
        private ILookupRepository _lookupRepository;

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

            _lookupRepository = new LookupRepository(_context);
        }

        [Test]
        public void should_Get_Counties()
        {
            var counties = _lookupRepository.ReadAll<County>().ToList();
            Assert.IsTrue(counties.Count > 0);
            var c = counties.First();
            Assert.IsNotNull(c);
            Console.WriteLine(c);
            foreach (var s in c.SubCounties)
            {
                Console.WriteLine($">.{s}");
            }
            
        }

        [Test]
        public void should_Get_County_SubCounties()
        {
            var counties = _lookupRepository.ReadAll<County>(x=>x.SubCounties).ToList();
            Assert.IsTrue(counties.Count > 0);
            Assert.IsTrue(counties.First().SubCounties.Count > 0);
            Console.WriteLine(counties.First());
        }
    }
}