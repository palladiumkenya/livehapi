using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class SubscriberTranslationRepositoryTests
    {
        private LiveHAPIContext _context;
        private ISubscriberTranslationRepository _subscriberTranslationRepository;
        private List<SubscriberSystem> _subscriberSystems;
        private SubscriberSystem _subscriberSystem;

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
            _subscriberSystems=_context.SubscriberSystems.ToList();
            _subscriberSystem = _subscriberSystems.First(x => x.IsDefault);
            _subscriberTranslationRepository = new SubscriberTranslationRepository(_context);
        }

        [Test]
        public void should_Sync_Updated_SubscriberTranslation()
        {
            var translation = TestData.TestSubscriberTranslations().First();
            translation.SubCode = "Jikuna";
         
            _subscriberTranslationRepository.Sync(new List<SubscriberTranslation>{translation});
            var updatedSubscriberTranslation = _subscriberTranslationRepository.Get(translation.Id);
            Assert.IsNotNull(updatedSubscriberTranslation);
            Assert.AreEqual("Jikuna", updatedSubscriberTranslation.SubCode);
            Console.WriteLine(updatedSubscriberTranslation);
        }
    }
}