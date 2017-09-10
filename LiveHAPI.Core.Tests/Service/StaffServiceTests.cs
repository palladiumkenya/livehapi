using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class StaffServiceTests
    {
        private IStaffService _staffService;
        private LiveHAPIContext _context;
        private Practice _practice;

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
            var pr = new PracticeRepository(_context);
            _practice = pr.GetAll().First();
            
            var activationService = new ActivationService(new PracticeRepository(_context),new PracticeActivationRepository(_context),new MasterFacilityRepository(_context));
            _staffService=new StaffService(new PersonNameRepository(_context),new PersonRepository(_context),new UserRepository(_context),activationService);
        }

        [Test]
        public void should_Enlist_Users_New()
        {
            //13080 Mbagathi DH

            var users=
            var fac = activationService.Verify(13080);
            Assert.IsNotNull(fac);
            Console.WriteLine(fac);
        }

        [Test]
        public void should_Enlist_Users_Updated()
        {
            //13080 Mbagathi DH

            var fac = activationService.Verify(13080);
            Assert.IsNotNull(fac);
            Console.WriteLine(fac);
        }
    }
}