using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private PersonRepository _personRepository;

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
            _personRepository=new PersonRepository(_context);
            var activationService = new ActivationService(new PracticeRepository(_context),new PracticeActivationRepository(_context),new MasterFacilityRepository(_context));
            _staffService=new StaffService(new PersonNameRepository(_context),new PersonRepository(_context),new UserRepository(_context),activationService);
        }

        [Test]
        public void should_Enlist_Users_New()
        {
            var usersKenyaEmr = TestData.TestUserInfos().Where(
                x => x.Identity.SourceRef == "11" &&
                     x.Identity.SourceSys == "KenyaEMR").ToList();

            var codeKenyaEmr = usersKenyaEmr.First().Identity.Source;

            var users= _staffService.EnlistUsers(codeKenyaEmr, usersKenyaEmr).ToList();

            Assert.IsTrue(users.Count>0);
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }
   
        [Test]
        public void should_Enlist_Users_Exisiting()
        {
            var usersKenyaEmr = TestData.TestUserInfos().Where(
                x => x.Identity.SourceRef == "10" &&
                     x.Identity.SourceSys == "KenyaEMR").ToList();
            usersKenyaEmr[0].UserName = "Maun";
            usersKenyaEmr[0].PersonInfo.LastName = "Maun M";


            var codeKenyaEmr = usersKenyaEmr.First().Identity.Source;

            var users = _staffService.EnlistUsers(codeKenyaEmr, usersKenyaEmr).ToList();
            var userUpdated = users.First();
            Assert.IsNotNull(userUpdated);
            Assert.AreEqual("Maun",userUpdated.UserName);
            var person = _personRepository.Get(userUpdated.PersonId);
            Assert.IsNotNull(person);
            Assert.AreEqual("Maun M", person.Names.First().LastName);
            Console.WriteLine(person.Names.First().FullName);
            Console.WriteLine(userUpdated);
        }
    }
}