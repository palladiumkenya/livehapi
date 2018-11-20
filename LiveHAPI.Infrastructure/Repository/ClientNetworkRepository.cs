using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Builders;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Builder;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientNetworkRepository : BaseRepository<ClientNetwork, Guid>, IClientNetworkRepository
    {
        public ClientNetworkRepository(LiveHAPIContext context) : base(context)
        {
        }

        public async  Task Generate()
        {
            if (DbSet.Any())
            {
                Context.RemoveRange(DbSet);
                Context.SaveChanges();
            }

            var networks = new List<ClientNetwork>();

            var relationships = Context.ClientStageRelationships.AsNoTracking().ToList();
            var clients = Context.ClientStages.AsNoTracking().ToList();

            foreach (var primaryContactId in relationships.Select(x => x.IndexClientId).Distinct())
            {
                var client = clients.SingleOrDefault(x => x.Id == primaryContactId);
                if (null != client)
                {
                    var builder = new ClientNetworkBuilder();
                    builder.CreatePrimary(Contact.CreatePrimary(client));


                    var relations = relationships.Where(x => x.IndexClientId == primaryContactId).ToList();
                    foreach (var relation in relations)
                    {
                        var secondaryClient = clients.SingleOrDefault(x => x.Id == relation.SecondaryClientId);
                        if (null != secondaryClient)
                            builder.AddSecondaryContact(Contact.CreateSecondary(client, relation));
                    }


                    networks.AddRange(builder.Build());
                }
            }

            if (networks.Any())
                await Context.AddRangeAsync(networks);

            await Context.SaveChangesAsync();
        }

        public IQueryable<ClientNetwork> GetAll()
        {
            return DbSet.AsNoTracking();
        }
    }
}