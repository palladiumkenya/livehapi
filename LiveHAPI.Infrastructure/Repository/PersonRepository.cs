using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
    {
        public PersonRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Person GetDemographics(Guid id)
        {
            return Context.Persons
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Names)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> GetStaff()
        {
            var users = Context.Users.Select(x => x.PersonId).ToList();
            var providers = Context.Providers.Select(x => x.PersonId).ToList();

            var personIds=new List<Guid>();

            personIds.AddRange(users);
            personIds.AddRange(providers);


            var persons = Context
                .Persons.Include(x=>x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Names)
                .Where(x => personIds.Contains(x.Id));

            return persons;

        }
    }
}