using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Schedulers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Interface.Writers.Family;
using LiveHAPI.Sync.Core.Interface.Writers.Index;
using LiveHAPI.Sync.Core.Interface.Writers.Partner;
using LiveHAPI.Sync.Core.Loader;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using LiveHAPI.Sync.Core.Writer;
using LiveHAPI.Sync.Core.Writer.Family;
using LiveHAPI.Sync.Core.Writer.Index;
using LiveHAPI.Sync.Core.Writer.Partner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;
using Microsoft.Extensions.DependencyInjection;

namespace LiveHAPI.Sync.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;

        [OneTimeSetUp]
        public void Setup()
        {
            RegLicence();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config["connectionStrings:livehAPIConnection"];
            var hapiendpoint = config["endpoints:hapi"];
            ServiceProvider = new ServiceCollection()
                    .AddTransient<IRestClient>(s => new RestClient(hapiendpoint))
                .AddDbContext<LiveHAPIContext>(o => o.UseSqlServer(connectionString))

                .AddTransient<LiveHAPIContext>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IPracticeRepository, PracticeRepository>()
                .AddTransient<IPersonRepository, PersonRepository>()
                .AddTransient<ISubscriberTranslationRepository, SubscriberTranslationRepository>()
                .AddTransient<IClientRepository, ClientRepository>()
                .AddTransient<IClientRelationshipRepository, ClientRelationshipRepository>()

                .AddTransient<IClientStageRepository, ClientStageRepository>()
                .AddTransient<IClientStageRelationshipRepository, ClientStageRelationshipRepository>()
                .AddTransient<IClientPretestStageRepository, ClientPretestStageRepository>()
                .AddTransient<IContactsEncounterRepository, ContactsEncounterRepository>()
                .AddTransient<ISubscriberSystemRepository, SubscriberSystemRepository>()
                .AddTransient<IClientEncounterRepository, ClientEncounterRepository>()
                .AddTransient<IClientEncounterRepository, ClientEncounterRepository>()


                .AddTransient<IClientUserReader, ClientUserReader>()
                .AddTransient<IClientFacilityReader, ClientFacilityReader>()
                .AddTransient<IClientLookupReader, ClientLookupReader>()

                .AddTransient<IIndexClientMessageWriter, IndexClientMessageWriter>()
                .AddTransient<IFamilyClientMessageWriter, FamilyClientMessageWriter>()
                .AddTransient<IPartnerClientMessageWriter, PartnerClientMessageWriter>()
                .AddTransient<IFamilyWriter, FamilyWriter>()
                .AddTransient<IPartnerWriter, PartnerWriter>()
                .AddTransient<IDemographicsWriter, DemographicsWriter>()

                .AddTransient<IClientStageExtractor, ClientStageExtractor>()
                .AddTransient<IClientPretestStageExtractor, ClientPretestStageExtractor>()
                .AddTransient<IClientStageRelationshipExtractor, ClientStageRelationshipExtractor>()

                .AddTransient<IClientFamilyScreeningStageExtractor, ClientFamilyScreeningStageExtractor>()
                .AddTransient<IClientFamilyTracingStageExtractor, ClientFamilyTracingStageExtractor>()

                .AddTransient<IClientPartnerScreeningStageExtractor, ClientPartnerScreeningStageExtractor>()
                .AddTransient<IClientPartnerTracingStageExtractor, ClientPartnerTracingStageExtractor>()

                .AddTransient<IClientTracingStageExtractor, ClientTracingStageExtractor>()
                .AddTransient<IClientTestingStageExtractor, ClientTestingStageExtractor>()
                .AddTransient<IClientFinalTestStageExtractor, ClientFinalTestStageExtractor>()
                .AddTransient<IClientLinkageStageExtractor, ClientLinkageStageExtractor>()
                .AddTransient<IClientReferralStageExtractor, ClientReferralStageExtractor>()


                .AddTransient<IIndexClientMessageLoader, IndexClientMessageLoader>()
                .AddTransient<IFamilyClientMessageLoader, FamilyClientMessageLoader>()
                .AddTransient<IPartnerClientMessageLoader, PartnerClientMessageLoader>()

                .AddTransient<ISyncUserService, SyncUserService>()
                .AddTransient<ISyncFacilityService, SyncFacilityService>()
                .AddTransient<ISyncLookupService, SyncLookupService>()
                .AddTransient<ISyncUserService, SyncUserService>()

                .AddTransient<IExtractClientsService, ExtractClientsService>()
                .AddTransient<ISyncClientsService, SyncClientsService>()
                .BuildServiceProvider();


        }

        private void RegLicence()
        {
            try
            {
                string bulkConfigName = @"1755;701-ThePalladiumGroup";
                string bulkConfigCode = @"9005d618-3965-8877-97a5-7a27cbb21c8f";

                DapperPlusManager.AddLicense(bulkConfigName, bulkConfigCode);
                if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
            }
            catch (Exception e)
            {
                Log.Fatal($"{e}");
                throw;
            }
        }
    }
}
