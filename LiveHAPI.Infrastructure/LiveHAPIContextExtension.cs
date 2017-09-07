using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Infrastructure.Seeder;
using LiveHAPI.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LiveHAPI.Infrastructure
{
    public static class LiveHAPIContextExtension
    {

        public static bool AllMigrationsApplied(this LiveHAPIContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this LiveHAPIContext context)
        {
            context.SaveOrUpdateAll(InitialSeeder.ReadCsv<County>());
            context.SaveOrUpdateAll(InitialSeeder.ReadCsv<SubCounty>());
            context.SaveOrUpdateAll(InitialSeeder.ReadCsv<PracticeType>());
            context.SaveOrUpdateAll(InitialSeeder.ReadCsv<Practice>());
        }


        public static void AddOrUpdateItem(this LiveHAPIContext ctx, object entity)
        {
            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void SaveOrUpdateAll(this LiveHAPIContext context, IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                context.AddOrUpdateItem(entity);
            }
            context.SaveChanges();
        }
    }
}