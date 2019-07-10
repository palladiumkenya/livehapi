using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure.Repository;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private LiveHAPIContext _context;
        private IPersonRepository _personRepository;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.InitDb();
        }

        [SetUp]
        public void SetUp()
        {
            _context = TestInitializer.ServiceProvider.GetService<LiveHAPIContext>();
            _personRepository =  TestInitializer.ServiceProvider.GetService<IPersonRepository>();
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
            Assert.IsTrue(persons.Any());
            Console.WriteLine($"Clients:{persons.Count}");
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
