using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class PracticeRepositoryTests
    {
        private LiveHAPIContext _context;
        private IPracticeRepository _practiceRepository;

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
            TestDataCreator.Init(_context);

            _practiceRepository = new PracticeRepository(_context);
        }

        [Test]
        public void should_Get_By_Code()
        {
            var practice = _practiceRepository.GetByCode("14080");
            Assert.IsNotNull(practice);
            Console.WriteLine(practice);
        }
    }
}