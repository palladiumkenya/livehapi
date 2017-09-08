using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
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

            _context =new LiveHAPIContext(options);
          TestDataCreator.Init(_context);

            _practiceRepository =new PracticeRepository(_context);
        }

        [Test]
        public void should_Get_All()
        {
            var practices = _practiceRepository.GetAll().ToList();

            Assert.IsTrue(practices.Count>0);
            foreach (var practice in practices)
            {
                Console.WriteLine(practice); 
            }
        }

    }
}