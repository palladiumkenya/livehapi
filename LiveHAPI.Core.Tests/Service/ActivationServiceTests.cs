using System;
using System.Collections.Generic;
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
    public class ActivationServiceTests
    {
        private IActivationService _activationService;
        private LiveHAPIContext _context;
        private Practice _practice;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:realConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
           // TestDataCreator.Init(_context);
          //  var pr = new PracticeRepository(_context);
//            _practice = pr.GetAll().First();
            
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
        public void should_Enroll_Device_Site()
        {
            var p1 = Practice.Enroll(Guid.NewGuid(), "1", "Fac1");
            var p2 = Practice.Enroll(Guid.NewGuid(), "2", "Fac2");

            var practices = new List<Practice> {p1, p2};

            _activationService.EnrollDevicePractice(practices);
            var ids = practices.Select(x => x.Id);
            var pr = new PracticeRepository(_context);
            var ps = pr.GetAll().Where(x => ids.Contains(x.Id)).ToList();
            Assert.True(ps.Count == 2);
        }


        [Test]
        public void should_Get_Activation_Code_New()
        {
            var device=new DeviceInfo("1XY1","LG G6","192");

            var code= _activationService.GetActivationCode(_practice.Code, device);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(code));
            Console.WriteLine($"{device}>>>{code}");
        }

        [Test]
        public void should_Get_Activation_Code_ReNew()
        {
            var expiredActivation= _practice.Activations.FirstOrDefault(x => x.IsExpired());

            var device = new DeviceInfo(expiredActivation.Device,expiredActivation.Model,expiredActivation.DeviceCode);

            var code = _activationService.GetActivationCode(_practice.Code, device);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(code));
            Console.WriteLine($"{device}>>>{code}");
        }
    }

  
}