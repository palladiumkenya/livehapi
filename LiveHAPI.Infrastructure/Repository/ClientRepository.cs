using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client, Guid>, IClientRepository
    {
        public ClientRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Client GetClient(Guid id)
        {
            return Context.Clients
                .Include(x => x.Identifiers)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PersonMatch> GetById(Guid id)
        {
            var personMatches = new List<PersonMatch>();

            var personIds = Context.Clients.Where(x => x.Id == id).Select(x => x.PersonId).ToList();

            var persons = Context.Persons.Where(x => personIds.Contains(x.Id))
                .Include(x => x.Clients).ThenInclude(c => c.Identifiers)
                //.Include(x => x.Clients).ThenInclude(c => c.Relationships)
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

        public IEnumerable<PersonMatch> GetRelationsById(Guid id)
        {
            var personMatches = new List<PersonMatch>();

            var clients = Context.Clients.Where(x => x.Id == id).Include(x=>x.Relationships);
            var ids=new List<Guid>();
            foreach (var client in clients)
            {
                foreach (var clientRelationship in client.Relationships)
                {
                    if (clientRelationship.RelatedClientId != id)
                        ids.Add(clientRelationship.RelatedClientId);
                }
            }

            foreach (var guid in ids)
            {
                var pm = GetById(guid);
                if(null!=pm)
                    personMatches.AddRange(pm);
            }        
            return personMatches;
        }


        public IEnumerable<PersonMatch> Search(string searchItem)
        {
            var personMatches = new List<PersonMatch>();
            searchItem = Regex.Replace(searchItem, @"\s+", " ").Trim();
            var searchItems = searchItem.Split();


            var searchHits = new List<SearchHit>();
            
            //IDs

            foreach (var item in searchItems)
            {
                var identifiers = Context
                    .ClientIdentifiers
                    .Where(x => x.Identifier.ToLower().Contains(item.Trim().ToLower()))
                    .Select(x => x.ClientId)
                    .ToList();

                if (identifiers.Count > 0)
                {

                    var personIds = Context.Clients.Where(x => identifiers.Contains(x.Id)).Select(x => x.PersonId).ToList();


                    foreach (var identifier in personIds)
                    {
                        searchHits.Add(new SearchHit(identifier));
                    }
                }
            }


            if (searchHits.Count > 0)
            {
                searchHits = searchHits
                    .GroupBy(x => x.ItemId)
                    .Select(g => new SearchHit(g.Sum(s => s.Hits), g.First().ItemId))
                    .ToList();

                var personIds = searchHits.Select(x => x.ItemId).ToList();

                var persons = Context.Persons.Where(x => personIds.Contains(x.Id))
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
                    personMatches.Add(new PersonMatch(person, GetHit(person.Id, searchHits)));
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