using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EFCore.BulkExtensions;
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
            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<County>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<SubCounty>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<PracticeType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Practice>());
                transaction.Commit();
            }
        }
    }
}