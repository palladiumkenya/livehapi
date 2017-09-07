using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LiveHAPI.Infrastructure
{
    public class LiveHAPIContext : DbContext
    {
        public LiveHAPIContext(DbContextOptions<LiveHAPIContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<PracticeType> PracticeTypes { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<IdentifierType> IdentifierTypes { get; set; }
        public DbSet<ProviderType> ProviderTypes { get; set; }

        public DbSet<County> Counties { get; set; }
        public DbSet<SubCounty> SubCounties { get; set; }

        public DbSet<Practice> Practices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAddress> PersonAddresss { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAttribute> ClientAttributes { get; set; }
        public DbSet<ClientIdentifier> ClientIdentifiers { get; set; }
        public DbSet<ClientRelationship> ClientRelationships { get; set; }
    }
}