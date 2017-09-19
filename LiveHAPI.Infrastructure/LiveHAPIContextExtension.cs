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
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Core.Model.Subscriber;
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
            var x= new CsvHelper.Configuration.CsvConfiguration();
            x.Delimiter = "|";
            x.TrimFields = true;
            x.TrimHeaders = true;
            x.WillThrowOnMissingField = false;


            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<County>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<SubCounty>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<PracticeType>());

                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<MasterFacility>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Practice>());

                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Core.Model.QModel.Action>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Condition>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<KeyPop>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<MaritalStatus>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<ProviderType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<RelationshipType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<ConceptType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<EncounterType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<IdentifierType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<ValidatorType>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Validator>());

                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Category>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Item>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<CategoryItem>());
                
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Person>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<PersonName>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Provider>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<User>());

                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Module>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Form>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<FormProgram>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Concept>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Question>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<QuestionBranch>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<QuestionRemoteTransformation>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<QuestionReValidation>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<QuestionTransformation>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<QuestionValidation>());

                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<SubscriberSystem>());
                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<SubscriberConfig>());

                transaction.Commit();
            }
        }
    }
}