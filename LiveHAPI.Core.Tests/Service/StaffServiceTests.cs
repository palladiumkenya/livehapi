using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class StaffServiceTests
    {
        private LiveHAPIContext _context;
        private IStaffService _staffService;

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
            
            var activationService = new ActivationService(new PracticeRepository(_context),new PracticeActivationRepository(_context),new MasterFacilityRepository(_context));
            _staffService=new StaffService(new PersonNameRepository(_context),new PersonRepository(_context),new UserRepository(_context),activationService);
        }

        [Test]
        public void should_Enlist_Users_New()
        {
            var usersKenyaEMR = TestData.TestUserInfos().Where(
                x => x.Identity.SourceRef == "11" &&
                     x.Identity.SourceSys == "KenyaEMR").ToList();

            var codeKenyaEMR = usersKenyaEMR.First().Identity.Source;

            var userKE= _staffService.EnlistUsers(codeKenyaEMR, usersKenyaEMR).ToList();

            Assert.IsTrue(userKE.Count>0);
            foreach (var user in userKE)
            {
                Console.WriteLine(user);
            }
        }        
    }
}