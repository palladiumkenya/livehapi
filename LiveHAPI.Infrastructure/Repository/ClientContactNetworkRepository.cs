using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Builders;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Builder;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Custom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientContactNetworkRepository : BaseRepository<ClientContactNetwork, Guid>,
        IClientContactNetworkRepository
    {
        public ClientContactNetworkRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Task Clear()
        {
            if (!DbSet.Any())
                return Task.CompletedTask;
           
            Context.RemoveRange(DbSet);
            return Context.SaveChangesAsync();
        }

        public async Task Generate()
        {
            var networks = new List<ClientContactNetwork>();
            var relationships = Context.ClientStageRelationships.AsNoTracking().ToList();
            var clients = Context.ClientStages.AsNoTracking().ToList();

            foreach (var primaryContactId in relationships.Select(x => x.IndexClientId).Distinct())
            {
                var client = clients.SingleOrDefault(x => x.Id == primaryContactId);
                if (null != client)
                {
                    var builder = new ClientContactNetworkBuilder();
                    builder.CreatePrimary(Contact.CreatePrimary(client));


                    var relations = relationships.Where(x => x.IndexClientId == primaryContactId).ToList();
                    foreach (var relation in relations)
                    {
                        var secondaryClient = clients.SingleOrDefault(x => x.Id == relation.SecondaryClientId);
                        if (null != secondaryClient)
                            builder.AddSecondaryContact(Contact.CreateSecondary(secondaryClient, relation));
                    }

                    networks.AddRange(builder.Build());
                }
            }

            if (networks.Any())
                await Context.AddRangeAsync(networks);

            await Context.SaveChangesAsync();
        }

        public Task UpdateTree()
        {
            var updateList=new List<ClientContactNetwork>();
            
            var clientTree = LoadTree().Where(x=>!x.ClientContactNetworkId.IsNullOrEmpty()) .ToList();

            var clients = DbSet.Include(x => x.Networks)
                .Where(x => x.IsPrimary && x.ClientContactNetworkId.IsNullOrEmpty())
                .ToList();

            foreach (var client in clients)
            {
                var update = clientTree.FirstOrDefault(x => x.Id == client.Id);
                if (null != update)
                {
                    client.ClientContactNetworkId = update.ClientContactNetworkId;
                    updateList.Add(client);
                }

            }

            if (updateList.Any())
            {
                Context.UpdateRange(updateList);
                return Context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

        public IEnumerable<ClientContactNetwork> LoadAll()
        {
            return DbSet.Include(x => x.Networks).AsNoTracking();
        }

        private IEnumerable<ClientContactNetwork> LoadTree()
        {
            var all = DbSet.AsNoTracking()
                .Include(x => x.Networks)
                .ToList();

            var children = all.Where(x => x.IsPrimary).SelectMany(x => x.Networks).ToList();

            foreach (var network in all.Where(x => x.IsPrimary))
            {
                var parent = children.FirstOrDefault(x => x.ClientId == network.ClientId);
                if (null != parent && network.ClientContactNetworkId.IsNullOrEmpty())
                    network.ClientContactNetworkId = parent.ClientContactNetworkId;
            }

            return all.Where(x => !x.ClientContactNetworkId.IsNullOrEmpty());
        }
    }
}