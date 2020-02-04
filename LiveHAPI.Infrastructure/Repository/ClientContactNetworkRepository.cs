using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LiveHAPI.Core.Interfaces.Builders;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Builder;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Custom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Z.Dapper.Plus;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientContactNetworkRepository : BaseRepository<ClientContactNetwork, Guid>,
        IClientContactNetworkRepository
    {
        public ClientContactNetworkRepository(LiveHAPIContext context) : base(context)
        {
        }

        public async Task Clear()
        {
            await GetDbConnection().ExecuteAsync($"TRUNCATE TABLE {nameof(Context.ClientContactNetworks)};");
        }

        public  Task Generate()
        {
            var relationships = Context.ClientStageRelationships.AsNoTracking();
            var clients = Context.ClientStages.AsNoTracking();

            foreach (var primaryContactId in relationships.Select(x => x.IndexClientId).Distinct())
            {
                var networks = new List<ClientContactNetwork>();
                var client = clients.SingleOrDefault(x => x.Id == primaryContactId);
                if (null != client)
                {
                    var builder = new ClientContactNetworkBuilder();

                    //    check exisiting primary
                    var existingPrimary = Context.ClientContactNetworks.AsNoTracking()
                        .FirstOrDefault(x => x.ClientId == client.ClientId);

                    if (null != existingPrimary)
                    {
                        builder.UsePrimary(existingPrimary);
                    }
                    else
                    {
                        builder.CreatePrimary(Contact.CreatePrimary(client));
                    }

                    var relations = relationships.Where(x => x.IndexClientId == primaryContactId).ToList();
                    foreach (var relation in relations)
                    {
                        var secondaryClient = clients.SingleOrDefault(x => x.Id == relation.SecondaryClientId);
                        if (null != secondaryClient)
                            builder.AddSecondaryContact(Contact.CreateSecondary(secondaryClient, relation));
                    }

                    networks.AddRange(builder.Build());
                }

                if (networks.Any())
                {
                    GetDbConnection().BulkInsert(networks);
                }
            }
            return Task.CompletedTask;

        }

        public IEnumerable<ClientContactNetwork> LoadAll()
        {
            return DbSet.Include(x => x.Networks).AsNoTracking();
        }

        public int GetAllCount()
        {
            return LoadAll()
                .Where(x => x.IsPrimary)
                .Select(x => x.Id)
                .Count();
        }
    }
}
