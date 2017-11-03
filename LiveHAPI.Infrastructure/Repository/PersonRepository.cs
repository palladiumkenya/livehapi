using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Linq;
using System.Text.RegularExpressions;
using LiveHAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

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

        public IEnumerable<PersonMatch> Search(string searchItem)
        {
            var personMatches = new List<PersonMatch>();
            searchItem = Regex.Replace(searchItem, @"\s+", " ").Trim();
            var searchItems = searchItem.Split();


            var searchHits = new List<SearchHit>();

            foreach (var item in searchItems)
            {
                var names = Context
                    .PersonNames
                    .Where(x => x.FirstName.ToLower().Contains(item.Trim().ToLower())||
                                 x.MiddleName.ToLower().Contains(item.Trim().ToLower())||
                                 x.LastName.ToLower().Contains(item.Trim().ToLower()))
                    .ToList();

                if (names.Count > 0)
                {
                    foreach (var personName in names)
                    {
                        searchHits.Add(new SearchHit(personName.PersonId));
                    }
                }
            }

            if (searchHits.Count > 0)
            {
                searchHits = searchHits
                    .GroupBy(x => x.ItemId)
                    .Select(g => new SearchHit(g.Sum(s => s.Hits),g.First().ItemId))
                    .ToList();

                var personIds = searchHits.Select(x => x.ItemId).ToList();

                var persons = Context.Persons.Where(x => personIds.Contains(x.Id)).ToList();


                foreach (var person in persons)
                {
                    personMatches.Add(new PersonMatch(person,GetHit(person.Id,searchHits)));
                }
            }
            return personMatches;
        }

        private int GetHit(Guid personId, List<SearchHit> searchHits)
        {
            var found = searchHits.FirstOrDefault(x => x.ItemId == personId);

            return null == found ? 0 : found.Hits;
        }
    }
}