using Serilog;
using Serilog.Core;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using AutoMapper;
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
using LiveHAPI.Sync.Core.Loader;
using LiveHAPI.Sync.Core.Profiles;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using LiveHAPI.Sync.Core.Writer;
using LiveHAPI.Sync.Schedulers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Logging;
using Z.Dapper.Plus;

namespace LiveHAPI.Sync
{
    class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public  static ISyncConfigScheduler SyncConfigScheduler { get; private set; }
        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.RollingFile("logs\\hapisync-{Date}.txt")
                .CreateLogger();

            Log.Debug("initializing Sync...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var endpoint = configuration["endpoints:iqcare"];
            var connectionString = configuration.GetConnectionString("hAPIConnection");
            var syncConfigInterval = configuration["syncInterval:config"];
            var syncClientInterval = configuration["syncInterval:clients"];
            var bulkConfigName = configuration["bulkConfig:name"];
            var bulkConfigCode = configuration["bulkConfig:code"];

            Log.Debug($"configured endpoint");
            Log.Debug(new string('-', 40));
            Log.Debug($"    {endpoint}");
            Log.Debug(new string('-', 40));
            Log.Debug($"configured connection");
            Log.Debug(new string('-', 40));
            Log.Debug($"    {connectionString}");
            Log.Debug(new string('-', 40));

            try
            {
                DapperPlusManager.AddLicense(bulkConfigName, bulkConfigCode);
                if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }

            ServiceProvider = new ServiceCollection()

                .AddSingleton<IRestClient>(new RestClient(endpoint))
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
                .AddTransient<ISyncClientsService, SyncClientsService>()
                .AddTransient<IExtractClientsService, ExtractClientsService>()
            
                .AddSingleton<ISyncConfigScheduler>(new SyncConfigScheduler(syncConfigInterval,syncClientInterval))
            
                .BuildServiceProvider();

            Mapper.Initialize(cfg => { cfg.AddProfile<ClientProfile>(); });

            Log.Debug("starting Sync...");

            try
            {
                SyncConfigScheduler = ServiceProvider.GetService<ISyncConfigScheduler>();
                SyncConfigScheduler.Run();
            }
            catch (Exception e)
            {
                Log.Error("Sync startup error");
                Log.Error($"{e}");
            }

            Console.ReadLine();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Log.Debug("stopping Sync...");
            SyncConfigScheduler.Shutdown();
            Log.Debug("Sync stopped");
        }
    }
}