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
using Serilog;

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
            var x = new CsvHelper.Configuration.CsvConfiguration();
            x.Delimiter = "|";
            x.TrimFields = true;
            x.TrimHeaders = true;
            x.WillThrowOnMissingField = false;

            using (var transaction = context.Database.BeginTransaction())
            {
                if (!context.Counties.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<County>());
                if (!context.SubCounties.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<SubCounty>());
                if (!context.PracticeTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<PracticeType>());
                if (!context.MasterFacilities.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<MasterFacility>());
                if (!context.Practices.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Practice>());

                if (!context.Actions.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Core.Model.QModel.Action>());
                if (!context.Conditions.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Condition>());
                if (!context.KeyPops.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<KeyPop>());
                if (!context.MaritalStatuses.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<MaritalStatus>());
                if (!context.ProviderTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<ProviderType>());
                if (!context.RelationshipTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<RelationshipType>());
                if (!context.ConceptTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<ConceptType>());
                if (!context.EncounterTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<EncounterType>());
                if (!context.IdentifierTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<IdentifierType>());
                if (!context.ValidatorTypes.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<ValidatorType>());
                if (!context.Validators.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Validator>());

                if (!context.Categories.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Category>());
                if (!context.Items.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Item>());
                if (!context.CategoryItems.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<CategoryItem>());
                if (!context.Persons.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Person>());
                if (!context.PersonNames.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<PersonName>());
                if (!context.Providers.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Provider>());
                if (!context.Users.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<User>());

                if (!context.Modules.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Module>());
                if (!context.Forms.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Form>());
                if (!context.Programs.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<FormProgram>());
                if (!context.Concepts.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Concept>());
                if (!context.Questions.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<Question>());
                if (!context.QuestionBranches.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<QuestionBranch>());
                if (!context.QuestionRemoteTransformations.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<QuestionRemoteTransformation>());
                if (!context.QuestionReValidations.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<QuestionReValidation>());
                if (!context.QuestionTransformation.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<QuestionTransformation>());
                if (!context.QuestionValidations.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<QuestionValidation>());

                if (!context.SubscriberSystems.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<SubscriberSystem>());
                if (!context.SubscriberConfigs.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<SubscriberConfig>());
                if (!context.SubscriberMaps.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<SubscriberMap>());

               if (!context.SubscriberTranslations.Any())
                    context.BulkInsert(InitialSeeder.ReadCsv<SubscriberTranslation>());
               
                if (!context.SubscriberTranslations.Any(s => s.IsUpdated()))
                {
                    context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<County>());
                    context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Category>());
                    context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<Item>());
                    context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<CategoryItem>());
                    context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<KeyPop>());

                    context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<SubscriberTranslation>());
                    Log.Error(new string('*', 50));
                    Log.Error($"        {Shared.Defualts.SyncVersion}   ");
                    Log.Error(new string('*', 50));
                }

                context.BulkInsertOrUpdate(InitialSeeder.ReadCsv<SubscriberCohort>());

                transaction.Commit();
            }
        }

        public static void CreateViews(this LiveHAPIContext context)
        {
            try
            {
                context.Database.ExecuteSqlCommand(
                    @"
               
                IF OBJECT_ID('dbo.vBookedFamilyContacts') IS NULL
                    BEGIN
                        EXECUTE('
	                        create view vBookedFamilyContacts
	                        as
                            SELECT        
	                            ObsMemberScreenings.Id, Encounters.ClientId, 
	                            ObsMemberScreenings.BookingDate, ObsMemberScreenings.Eligibility, 
	                            Encounters.PracticeId, Encounters.ProviderId, 
	                            Encounters.EncounterTypeId, EncounterTypes.Name,
                                ObsMemberScreenings.BookingMet, ObsMemberScreenings.DateBookingMet, ObsMemberScreenings.TraceId
                            FROM            
	                            ObsMemberScreenings INNER JOIN 
	                            Encounters ON 	ObsMemberScreenings.EncounterId = Encounters.Id INNER JOIN
                                EncounterTypes ON Encounters.EncounterTypeId = EncounterTypes.Id
                            WHERE        
	                            (ObsMemberScreenings.Eligibility = ''B25ECCD4-852F-11E7-BB31-BE2E44B06B34'') AND 
                                ((ObsMemberScreenings.BookingMet IS NULL) OR (ObsMemberScreenings.BookingMet = 0))
                                ')
                    END

                IF OBJECT_ID('dbo.vBookedPartnerContacts') IS NULL
                    BEGIN
                        EXECUTE('
	                        create view vBookedPartnerContacts
	                        as
                            SELECT        
	                            ObsPartnerScreenings.Id, Encounters.ClientId, 
	                            ObsPartnerScreenings.BookingDate, ObsPartnerScreenings.Eligibility, 
	                            Encounters.PracticeId, Encounters.ProviderId, 
	                            Encounters.EncounterTypeId, EncounterTypes.Name,
                                ObsPartnerScreenings.BookingMet, ObsPartnerScreenings.DateBookingMet, ObsPartnerScreenings.TraceId
                            FROM            
	                            ObsPartnerScreenings INNER JOIN 
	                            Encounters ON 	ObsPartnerScreenings.EncounterId = Encounters.Id INNER JOIN
                                EncounterTypes ON Encounters.EncounterTypeId = EncounterTypes.Id
                            WHERE        
	                            (ObsPartnerScreenings.Eligibility = ''B25ECCD4-852F-11E7-BB31-BE2E44B06B34'') AND 
                                ((ObsPartnerScreenings.BookingMet IS NULL) OR (ObsPartnerScreenings.BookingMet = 0))
                                ')
                    END


                IF OBJECT_ID('dbo.vReferredContacts') IS NULL
                    BEGIN
                        EXECUTE('
	                        create view vReferredContacts
	                        as
                            SELECT        
	                            ObsLinkages.Id, ObsLinkages.DateEnrolled, ObsLinkages.DatePromised, Encounters.ClientId, Encounters.PracticeId,Encounters.ProviderId,
                                Encounters.EncounterTypeId, EncounterTypes.Name
                            FROM            
	                            ObsLinkages INNER JOIN
	                            Encounters ON ObsLinkages.EncounterId = Encounters.Id INNER JOIN
                                EncounterTypes ON Encounters.EncounterTypeId = EncounterTypes.Id
                            WHERE       
	                            (NOT (ObsLinkages.DatePromised IS NULL)) AND (ObsLinkages.DateEnrolled IS NULL)
                                ')
                    END



            ");

            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }

//            try
//            {
//                context.Database.ExecuteSqlCommand(
//                    @"
//               
//                IF OBJECT_ID('dbo.vReferredContacts') IS NULL
//                    BEGIN
//                        EXECUTE('
//	                        create view vReferredContacts
//	                        as
//                            SELECT        
//	                            ObsMemberScreenings.Id, Encounters.ClientId, 
//	                            ObsMemberScreenings.BookingDate, ObsMemberScreenings.Eligibility, 
//	                            Encounters.PracticeId, Encounters.ProviderId, 
//	                            Encounters.EncounterTypeId, EncounterTypes.Name
//                            FROM            
//	                            ObsMemberScreenings INNER JOIN 
//	                            Encounters ON 	ObsMemberScreenings.EncounterId = Encounters.Id INNER JOIN
//                                EncounterTypes ON Encounters.EncounterTypeId = EncounterTypes.Id
//                            WHERE        
//	                            (ObsMemberScreenings.Eligibility = 'B25ECCD4-852F-11E7-BB31-BE2E44B06B34')
//                                ')
//                    END
//            ");
//
//            }
//            catch (Exception e)
//            {
//                Log.Debug($"{e}");
//            }
        }
    }
}