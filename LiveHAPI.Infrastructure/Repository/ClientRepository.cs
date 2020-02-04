using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using Dapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Z.Dapper.Plus;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client, Guid>, IClientRepository
    {
        public ClientRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Client GetClient(Guid id,bool withIds = true)
        {
            if (withIds)
            {
                return Context.Clients
                    .Include(x => x.Identifiers)
                    .FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return Context.Clients
                    .FirstOrDefault(x => x.Id == id);
            }

        }

        public Client GetClientStates(Guid id)
        {
            return Context.Clients
                .Include(x => x.ClientStates)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PersonMatch> GetById(Guid id)
        {
            var personMatches = new List<PersonMatch>();

            var personIds = Context.Clients.Where(x => x.Id == id).Select(x => x.PersonId).ToList();

            var persons = Context.Persons.Where(x => personIds.Contains(x.Id))
                .Include(x => x.Clients).ThenInclude(c => c.Identifiers)
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
                    personMatches.Add(new PersonMatch(person, GetHit(person.Id, searchHits)));
                }
            }
            return personMatches;
        }

        public void UpdateIds(List<ClientIdentifier> identifiers)
        {
            var updates = new List<ClientIdentifier>();
            var inserts = new List<ClientIdentifier>();

            foreach (var clientIdentifier in identifiers)
            {

                var id = Context.ClientIdentifiers.AsNoTracking().FirstOrDefault(x => x.Id == clientIdentifier.Id);
                if (null != id)
                {
                    updates.Add(clientIdentifier);
                }
                else
                {
                    inserts.Add(clientIdentifier);
                }
            }
            using (var con = GetDbConnection())
            {
                con.BulkUpdate(updates);
                con.BulkInsert(inserts);
            }
        }
        public void UpdateTempRelations(List<ClientRelationship> identifiers)
        {
            var updates = new List<TempClientRelationship>();
            var inserts = new List<TempClientRelationship>();

            var tempRelations =  Mapper.Map<List<TempClientRelationship>>(identifiers);
            foreach (var clientIdentifier in tempRelations)
            {

                var id = Context.TempClientRelationships.AsNoTracking().FirstOrDefault(x => x.Id == clientIdentifier.Id);
                if (null != id)
                {
                    updates.Add(clientIdentifier);
                }
                else
                {
                    inserts.Add(clientIdentifier);
                }
            }
            using (var con = GetDbConnection())
            {
                con.BulkUpdate(updates);
                con.BulkInsert(inserts);
            }
        }

        public void UpdateClientState(Guid clientId, List<ClientState> clientStates)
        {   var statses = Context.ClientStates.AsNoTracking().Where(x=>x.ClientId==clientId).ToList();
            using (var con = GetDbConnection())
            {
                con.BulkDelete(statses);
                con.BulkInsert(clientStates);
            }
        }

        public void UpdateRelationships()
        {
            var updates = new List<ClientRelationship>();
            var inserts = new List<ClientRelationship>();

            var tempRelations = Context.TempClientRelationships.AsNoTracking().ToList();
            var relations = Mapper.Map<List<ClientRelationship>>(tempRelations);
            using (var con = GetDbConnection())
            {
                foreach (var clientRelationship in relations)
                {
                    var id = Context.ClientRelationships.AsNoTracking()
                        .FirstOrDefault(x => x.Id == clientRelationship.Id);


                        if (null != id)
                        {

                            updates.Add(id);
                        }
                        else
                        {
                            inserts.Add(clientRelationship);
                        }
                }
            }

            using (var con = GetDbConnection())
            {
                con.BulkUpdate(updates);
                foreach (var clientRelationship in inserts)
                {
                    try
                    {
                        con.BulkInsert(clientRelationship);
                    }
                    catch (Exception e)
                    {
                        Log.Debug(new string('*', 30));
                        Log.Debug($"{e}");
                        Log.Debug(new string('*',30));
                    }

                }
                con.Execute(@"
                DELETE FROM 
                    TempClientRelationships
                FROM  
                    TempClientRelationships INNER JOIN
                    ClientRelationships ON TempClientRelationships.Id = ClientRelationships.Id
                ");
            }
        }


        public void UpdateSyncStatus(IEnumerable<ClientStage> clientStages)
        {

            using (var con = GetDbConnection())
            {
                foreach (var c in clientStages)
                {
                    string sql = $@"
                            UPDATE {nameof(Client)}s 
                            SET 
                                {nameof(Client.SyncStatus)} =@SyncStatus,
                                {nameof(Client.SyncStatusDate)}=@StatusDate
                            WHERE 
                                {nameof(Client.Id)} = @ClientId;";

                    con.Execute(sql,
                        new {ClientId = c.ClientId, SyncStatus = SyncStatus.Synced, StatusDate = DateTime.Now});
                }
            }
        }

        private int GetHit(Guid personId, List<SearchHit> searchHits)
        {
            var found = searchHits.FirstOrDefault(x => x.ItemId == personId);

            return null == found ? 0 : found.Hits;
        }
    }
}
