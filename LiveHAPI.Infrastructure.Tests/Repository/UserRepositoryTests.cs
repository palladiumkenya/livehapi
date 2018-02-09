using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private LiveHAPIContext _context;
        private IUserRepository _userRepository;
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

            _userRepository = new UserRepository(_context);
            _personRepository = new PersonRepository(_context);
        }

        [Test]
        public void should_Get_UserName()
        {
            var username = TestData.TestUsers().First().UserName;
            var user = _userRepository.GetByUsername(username);
            Assert.IsNotNull(user);
            Console.WriteLine(user);
        }

        [Test]
        public void should_Sync_New_User()
        {
            var practice = TestData.TestPractices().First();
            
            var user = Builder<User>.CreateNew()
                .With(x=>x.UserName=DateTime.Now.Ticks.ToString())
                .With(x => x.PracticeId = practice.Id)
                .With(x => x.Source ="Jim")
                .With(x => x.SourceSys = "Jam")


                .Build();
            
            _userRepository.Sync(user);
            _userRepository.Save();

            var newUser = _userRepository.Get(user.Id);
            Assert.IsNotNull(newUser);
            Assert.IsNotNull(newUser.PersonId);
            Assert.AreEqual(user.UserName, newUser.UserName);

            var p = _personRepository.Get(newUser.PersonId);
            Assert.NotNull(p);
            Assert.AreEqual("Jim", p.Names.First().FirstName);
            Assert.AreEqual("Jam", p.Names.First().LastName);
            Assert.True(p.HasDOB());
            Assert.True(p.HasGender());
            Assert.True(p.HasDOBEstimate());
            Assert.True(p.Providers.Count > 0);
            Assert.True(p.Users.Count > 0);
            Console.WriteLine(newUser);
        }

        [Test]
        public void should_Sync_Updated_User()
        {
            var user = TestData.TestUsers().First();
            user.Password = "14080";
            user.Source = "Jikuna";
            user.SourceSys = "Nikune";


            _userRepository.Sync(user);
            _userRepository.Save();

            var updatedUser = _userRepository.GetByUsername(user.UserName);
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("14080", updatedUser.Password);

            var p = _personRepository.Get(updatedUser.PersonId);
            Assert.NotNull(p);
            Assert.AreEqual("Jikuna", p.Names.First().FirstName);
            Assert.AreEqual("Nikune", p.Names.First().LastName);
            Assert.True(p.HasDOB());
            Assert.True(p.HasGender());
            Assert.True(p.HasDOBEstimate());
            Console.WriteLine(updatedUser);
        }
    }
}