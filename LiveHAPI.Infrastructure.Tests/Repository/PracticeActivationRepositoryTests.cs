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
    public class PracticeActivationRepositoryTests
    {
        private LiveHAPIContext _context;
        private IPracticeActivationRepository _practiceActivationRepository;

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
           // TestData.Init();
            //TestDataCreator.Init(_context);

            _practiceActivationRepository = new PracticeActivationRepository(_context);
        }

        [Test]
        public void should_Create_Activation()
        {
            var practiceActivation = _practiceActivationRepository.GetAll().FirstOrDefault();
            var newActication = TestData.TestPracticeActivations().First();
            newActication.Id=Guid.NewGuid();
            newActication.Device = "XXXXXX";
            newActication.PracticeId = practiceActivation.PracticeId;
                      
            _practiceActivationRepository.InsertOrUpdate(newActication);
            _practiceActivationRepository.Save();

            _practiceActivationRepository = new PracticeActivationRepository(_context);
            var saved = _practiceActivationRepository.Get(newActication.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual("XXXXXX", saved.Device);
            Console.WriteLine(saved);
        }

        [Test]
        public void should_Update_Activation()
        {
            var practiceActivation = _practiceActivationRepository.GetAll().FirstOrDefault();

            practiceActivation.ActivationCode = "xyz-";
            _practiceActivationRepository.InsertOrUpdate(practiceActivation);
            _practiceActivationRepository.Save();

            _practiceActivationRepository=new PracticeActivationRepository(_context);
            var saved = _practiceActivationRepository.Get(practiceActivation.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual("xyz-", saved.ActivationCode);
            Console.WriteLine(saved);
        }
    }
}