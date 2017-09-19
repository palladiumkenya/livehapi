using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class EncounterServiceTests
    {
        private IEncounterService _encounterService;
        private LiveHAPIContext _context;
        private EncounterRepository _encounterRepository;

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
            _encounterRepository =new EncounterRepository(_context);
            //_encounterService =new EncounterService(new ClientRepository(_context),new PracticeRepository(_context),_encounterRepository,new ObsRepository(_context)  );
        }

        [Test]
        public void should_Sync_New_Encounter()
        {
            var encounterInfo = TestData.TestEncounterInfos().Last();

            _encounterService.Sync(encounterInfo);

            _encounterRepository=new EncounterRepository(_context);
            var savedEncounte = _encounterRepository.Get(encounterInfo.Id);
            Assert.IsNotNull(savedEncounte);
            Console.WriteLine(savedEncounte);
        }

        [Test]
        public void should_Sync_New_Encounter_Obs()
        {
            var encounterInfo = TestData.TestEncounterInfos().Last();

            _encounterService.Sync(encounterInfo);

            _encounterRepository = new EncounterRepository(_context);
            var savedEncounte = _encounterRepository.Get(encounterInfo.Id);
            Assert.IsNotNull(savedEncounte);
            Assert.IsTrue(savedEncounte.Obses.Count>0);
            Console.WriteLine(savedEncounte);

            foreach (var obs in savedEncounte.Obses)
            {
                Console.WriteLine($"  >.{obs}");
            }
        }

        [Test]
        public void should_Sync_Update_Encounter()
        {
            var encounterInfo = TestData.TestEncounterInfos().First();
            encounterInfo.EncounterDate=new DateTime(2020,1,1);

            _encounterService.Sync(encounterInfo);

            _encounterRepository = new EncounterRepository(_context);
            var savedEncounter = _encounterRepository.Get(encounterInfo.Id);
            Assert.IsNotNull(savedEncounter);
            Assert.AreEqual(new DateTime(2020, 1, 1),savedEncounter.EncounterDate );
            Console.WriteLine(savedEncounter);
        }

        [Test]
        public void should_Sync_Update_Encounter_Obses()
        {
            var encounterInfo = TestData.TestEncounterInfos().First();
            encounterInfo.EncounterDate = new DateTime(2020, 1, 1);
            var findO = encounterInfo.Obses.First();
            var updateO = findO;
            updateO.ValueText = "MAUNN";

            encounterInfo.Obses.Remove(findO);
            encounterInfo.Obses.Add(updateO);

            _encounterService.Sync(encounterInfo);

            _encounterRepository = new EncounterRepository(_context);
            var savedEncounter = _encounterRepository.Get(encounterInfo.Id);
            Assert.IsNotNull(savedEncounter);
            Assert.AreEqual(new DateTime(2020, 1, 1), savedEncounter.EncounterDate);
            Assert.IsTrue(savedEncounter.Obses.Count > 0);
            var updatedObs = savedEncounter.Obses.FirstOrDefault(x => x.Id == findO.Id);
            Assert.IsNotNull(updatedObs);
            Assert.AreEqual("MAUNN", updatedObs.ValueText);
            Console.WriteLine(updatedObs);
        }
    }
}