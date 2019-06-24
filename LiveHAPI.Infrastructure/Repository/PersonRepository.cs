using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Dapper;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
    {
        public PersonRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Person GetProvider(Guid id)
        {
            return Context.Persons
                .Include(x => x.Names)
                .Include(x=>x.Providers)
                .FirstOrDefault(x => x.Id == id);
        }

        public Person GetDemographics(Guid id)
        {
            return Context.Persons
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Names)
                .Include(x=>x.Providers)
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

            //FNAME
            if (searchItems.Length == 1)
            {
                var item=searchItems[0];

                var personIds = Context
                    .PersonNames
                    .Where(x => x.FirstName.ToLower().Contains(item.Trim().ToLower())||
                                x.MiddleName.ToLower().Contains(item.Trim().ToLower())||
                                x.LastName.ToLower().Contains(item.Trim().ToLower()))
                    .Select(x=>x.PersonId)
                    .ToList();

                if (personIds.Count > 0)
                {
                    foreach (var id in personIds)
                    {
                        searchHits.Add(new SearchHit(id));
                    }
                }
            }
            //FNAME LNAME
            if (searchItems.Length == 2)
            {
                var item=searchItems[0];
                var item2=searchItems[1];

                var personIds = Context
                    .PersonNames
                    .Where(x => x.FirstName.ToLower().Contains(item.Trim().ToLower()) && (
                                x.MiddleName.ToLower().Contains(item2.Trim().ToLower())||
                                x.LastName.ToLower().Contains(item2.Trim().ToLower())))
                    .Select(x=>x.PersonId)
                    .ToList();

                if (personIds.Count > 0)
                {
                    foreach (var id in personIds)
                    {
                        searchHits.Add(new SearchHit(id));
                    }
                }
            }
            //FNAME LNAME ONAME
            if (searchItems.Length == 3)
            {
                var item=searchItems[0];
                var item2=searchItems[1];
                var item3=searchItems[2];

                var personIds = Context
                    .PersonNames
                    .Where(x => x.FirstName.ToLower().Contains(item.Trim().ToLower()) &&
                                x.MiddleName.ToLower().Contains(item2.Trim().ToLower()) &&
                                x.LastName.ToLower().Contains(item3.Trim().ToLower()))
                    .Select(x=>x.PersonId)
                    .ToList();

                if (personIds.Count > 0)
                {
                    foreach (var id in personIds)
                    {
                        searchHits.Add(new SearchHit(id));
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

                var persons = Context.Persons.Where(x => personIds.Contains(x.Id))
                    .Include(x=>x.Clients).ThenInclude(c=>c.Identifiers)
                    .Include(x => x.Clients).ThenInclude(c => c.ClientStates)
                    .Include(x => x.Addresses)
                    .Include(x => x.Contacts)
                    .Include(x => x.Names)
                    .ToList();


                foreach (var person in persons)
                {
                    foreach (var personClient in person.Clients)
                    {
                        personClient.Encounters = Context.Encounters
                            .Where(x => x.ClientId == personClient.Id)
                            .Include(x => x.Obses)
                            .Include(x => x.ObsTestResults)
                            .Include(x => x.ObsFinalTestResults)
                            .Include(x => x.ObsTraceResults)
                            .Include(x => x.ObsLinkages)


                            .Include(x => x.ObsMemberScreenings)
                            .Include(x => x.ObsPartnerScreenings)
                            .Include(x => x.ObsFamilyTraceResults)
                            .Include(x => x.ObsPartnerTraceResults)

                            .ToList();
                    }
                    personMatches.Add(new PersonMatch(person,GetHit(person.Id,searchHits)));
                }
            }

            return personMatches;
        }

        public IEnumerable<PersonMatch> SearchSite(string site, string searchItem)
        {
            var practiceIds = new List<Guid>();
            var results = new List<PersonMatch>();

            Guid siteId;
            bool isValid = Guid.TryParse(site, out siteId);

            if (isValid)
            {
                var practiceCodes = Context.Practices.AsNoTracking()
                    .Where(x => x.Id == siteId)
                    .Select(x => x.Code)
                    .ToList();

                if (practiceCodes.Any())
                {
                    var allPracticeIds = Context.Practices
                        .Where(x => practiceCodes.Contains(x.Code))
                        .Select(x => x.Id);

                    practiceIds.AddRange(allPracticeIds);
                }
                else
                {
                    practiceIds = Context.Practices.AsNoTracking()
                        .Where(x => x.Id == siteId)
                        .Select(x => x.Id)
                        .ToList();
                }
            }
            else
            {
                practiceIds = Context.Practices.AsNoTracking()
                    .Where(x => x.Code == site)
                    .Select(x => x.Id)
                    .ToList();
            }

            if (practiceIds.Any())
                results = Search(searchItem).Where(x => practiceIds.Contains(x.PracticeId)).ToList();

            return results;
        }

        public IEnumerable<PersonMatch> GetByCohort(SubscriberCohort cohort)
        {
            var personMatches = new List<PersonMatch>();

            string sql = $"SELECT PersonId FROM dbo.Clients WHERE ID In(SELECT ClientId FROM {cohort.View})";
            var personIds = Context.Database.GetDbConnection().Query<Guid>($"{sql}").ToList();

            var persons = Context.Persons.Where(x => personIds.Contains(x.Id))
                .Include(x => x.Clients).ThenInclude(c => c.Identifiers)
                .Include(x => x.Clients).ThenInclude(c => c.Identifiers)
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Names)
                .ToList();

            foreach (var person in persons)
            {
                foreach (var personClient in person.Clients)
                {
                   personClient.Encounters = Context.Encounters
                        .Where(x => x.ClientId == personClient.Id)
                        .Include(x => x.Obses)
                       .Include(x => x.ObsTestResults)
                       .Include(x => x.ObsFinalTestResults)
                       .Include(x => x.ObsTraceResults)
                        .Include(x => x.ObsLinkages)


                        .Include(x => x.ObsMemberScreenings)
                        .Include(x => x.ObsPartnerScreenings)
                        .Include(x => x.ObsFamilyTraceResults)
                        .Include(x => x.ObsPartnerTraceResults)

                        .ToList();
                }
                personMatches.Add(new PersonMatch(person, 1));
            }

            return personMatches;
        }

        public IEnumerable<PersonMatch> GetBySiteCohort(string site, SubscriberCohort cohort)
        {
            var practiceIds = new List<Guid>();
            var results = new List<PersonMatch>();

            Guid siteId;
            bool isValid = Guid.TryParse(site, out siteId);

            if (isValid)
            {
                practiceIds = Context.Practices.AsNoTracking()
                    .Where(x => x.Id == siteId)
                    .Select(x => x.Id)
                    .ToList();
            }
            else
            {
                practiceIds = Context.Practices.AsNoTracking()
                    .Where(x => x.Code == site)
                    .Select(x => x.Id)
                    .ToList();
            }

            if (practiceIds.Any())
                results = GetByCohort(cohort).Where(x => practiceIds.Contains(x.PracticeId)).ToList();

            return results;
        }

        public IEnumerable<Person> GetAllClients()
        {
            return Context.Persons
                .Include(x => x.Names)
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Clients)
                .ThenInclude(y => y.Identifiers)
                .Include(x => x.Clients)
                .ThenInclude(y => y.ClientStates).AsNoTracking().ToList()
                .Where(x => x.IsClient&&x.NotSynced);
        }

        public IEnumerable<Person> GetAllIndexClients()
        {
            return Context.Persons
                .Include(x => x.Names)
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Clients)
                .ThenInclude(y => y.Identifiers)
                .Include(x => x.Clients)
                .ThenInclude(y => y.ClientStates).AsNoTracking().ToList()
                .Where(x => x.IsHtsClient);
        }

        public IEnumerable<Person> GetAllSecondaryClients()
        {
            return Context.Persons
                .Include(x => x.Names)
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.Clients)
                .ThenInclude(y => y.Identifiers)
                .Include(x => x.Clients)
                .ThenInclude(y => y.ClientStates).AsNoTracking().ToList()
                .Where(x =>x.IsClient&& !x.IsHtsClient);
        }

        public IEnumerable<Person> GetContacts(Guid clientId)
        {
            throw new NotImplementedException();
        }

        private int GetHit(Guid personId, List<SearchHit> searchHits)
        {
            var found = searchHits.FirstOrDefault(x => x.ItemId == personId);

            return null == found ? 0 : found.Hits;
        }
    }
}
