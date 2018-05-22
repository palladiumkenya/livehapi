using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ClientStageRepositoryTests
    {
        private LiveHAPIContext _context;
        private IClientStageRepository _clientStageRepository;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:livehAPIConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
            //TestData.Init();
            //TestDataCreator.Init(_context);

            _clientStageRepository = new ClientStageRepository(_context);
        }

        [Test]
        public void should_Get_By_Code()
        {
            var clientStage = _context.ClientStages.AsNoTracking().First();
            
            _clientStageRepository.UpdateSyncStatus(clientStage.ClientId,SyncStatus.SentSuccess,"OK");

            var updatedClientStage = _context.ClientStages.FirstOrDefault(x=>x.ClientId==clientStage.ClientId);
            Assert.NotNull(updatedClientStage);
            Assert.AreEqual(SyncStatus.SentSuccess,updatedClientStage.SyncStatus);
            Assert.AreEqual("OK",updatedClientStage.SyncStatusInfo);
            Console.WriteLine($"{updatedClientStage} | {updatedClientStage.SyncStatus}, {updatedClientStage.SyncStatusInfo}");
        }
    }
}