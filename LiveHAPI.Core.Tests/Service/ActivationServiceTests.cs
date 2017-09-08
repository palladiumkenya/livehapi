using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Service;
using LiveHAPI.Core.ValueModel;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class ActivationServiceTests
    {
        private IActivationService _activationService;
        private LiveHAPIContext _context;
        private Practice _practice;
        private PracticeActivation _practiceActivation;
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
            
            _activationService = new ActivationService(new PracticeRepository(_context),new PracticeActivationRepository(_context),new MasterFacilityRepository(_context));
        }

        [Test]
        public void should_Verify_Site()
        {
            //13080 Mbagathi DH

            var fac = _activationService.Verify(13080);
            Assert.IsNotNull(fac);
            Console.WriteLine(fac);
        }

        [Test]
        public void should_Enroll_Site()
        {
            //13080 Mbagathi DH

            var practice = _activationService.EnrollPractice("13080");
            Assert.IsNotNull(practice);
            Console.WriteLine(practice);
        }


        [Test]
        public void should_Get_Activation_Code_New()
        {
            var device=new DeviceIdentity("1XY1","LG G6","192");

            var code= _activationService.GetActivationCode(_practice.Code, device);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(code));
            Console.WriteLine($"{device}>>>{code}");
        }

        [Test]
        public void should_Get_Activation_Code_ReNew()
        {
            var expiredActivation= _practice.Activations.FirstOrDefault(x => x.IsExpired());

            var device = new DeviceIdentity(expiredActivation.Device,expiredActivation.Model,expiredActivation.DeviceCode);

            var code = _activationService.GetActivationCode(_practice.Code, device);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(code));
            Console.WriteLine($"{device}>>>{code}");
        }
    }
}