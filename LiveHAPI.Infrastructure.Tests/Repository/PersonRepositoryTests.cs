using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private LiveHAPIContext _context;
        private IPersonRepository _personRepository;

        [SetUp]
        public void SetUp()
        {
             var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:livehAPIConnection"].Replace("#dir#", TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\"));

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
            //TestData.Init();
            //TestDataCreator.Init(_context);

            _personRepository = new PersonRepository(_context);
        }

        [Test]
        public void should_Get_Staff()
        {
            var persons = _personRepository.GetStaff().ToList();
            Assert.IsTrue(persons.Count>0);
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }

        [Test]
        public void should_Search_Person()
        {
            var persons = _personRepository.Search("index").ToList();
            Assert.True(persons.Count > 0);
            foreach (var person in persons.OrderByDescending(x=>x.Rank))
            {
                Console.WriteLine(person);
                Assert.IsTrue(person.Person.Clients.Count>0);
                Console.WriteLine(person.Person.Clients.First());
            }
        }

        [Test]
        public void should_Get_All_Clients()
        {
            var persons = _personRepository.GetAllClients().ToList();
            Assert.IsTrue(persons.Count > 0);
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }

        [Test]
        public void should_Get_All_Index_Clients()
        {
            var persons = _personRepository.GetAllIndexClients().ToList();
            Assert.IsTrue(persons.Count > 0);
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }

        [Test]
        public void should_Get_All_SecondaryClients()
        {
            var persons = _personRepository.GetAllSecondaryClients().ToList();
            Assert.IsTrue(persons.Count > 0);
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }

        [Test]
        public void should_Search_by_Site()
        {
            var persons = _personRepository.SearchSite("12618","o").ToList();
            Assert.True(persons.Count > 0);
            foreach (var person in persons.OrderByDescending(x=>x.Rank))
            {
                Console.WriteLine(person);
                Assert.IsTrue(person.Person.Clients.Count>0);
                Console.WriteLine($"{person.Person.Clients.First()} | {person.Person.Clients.First().PracticeId}" );
            }
        }
    }
}
