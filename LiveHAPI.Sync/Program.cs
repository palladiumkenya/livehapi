using Serilog;
using Serilog.Core;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Sync.Core;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface;
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
        private static IStartup _startup;
        public static IServiceProvider ServiceProvider { get; private set; }
        public  static ISyncConfigScheduler SyncConfigScheduler { get; private set; }
        public static HapiSettingsView HapiSettingsView { get; private set; }
        
        static void Main(string[] args)
        {

            bool hapiOffline = true;
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            System.AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.RollingFile("logs\\hapisync-{Date}.txt")
                .CreateLogger();

            Log.Debug("initializing Sync v[1.0.0.0] ...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var hapiendpoint = configuration["endpoints:hapi"];
            _startup = new SyncStartup(hapiendpoint);
            
            Log.Debug($"connecting to LiveHAPI on [{hapiendpoint}]");
            while (hapiOffline)
            {
                try
                {
                    HapiSettingsView = _startup.LoadSettings().Result;
                    if (null != HapiSettingsView) 
                    {
                        Log.Debug($"LiveHAPI CONNECTED");
                        Log.Debug($"verifying LiveHAPI settings...");
                        if (HapiSettingsView.IsVerifed)
                        {
                            Log.Debug($"LiveHAPI settings [OK]");
                            Log.Debug($"starting sync...");
                            hapiOffline = false;
                        }
                        else
                        {
                            Log.Error($"invalid LiveHAPI settings ! please open {hapiendpoint} and verify and save all settings");
                            Log.Error($"Sync will retry in 30 secs...");
                            Thread.Sleep(30000);    
                        }
                    }
                    else
                    {
                        Log.Error($"LiveHAPI connection FAILED");
                        Log.Error($"Sync will retry in 30 secs...");
                        Thread.Sleep(30000);
                    }
                }
                catch (Exception e)
                {
                    Log.Fatal(new string('*', 50));
                    Log.Fatal(e, "Sync Requires LiveHAPI to be online!");
                    Log.Fatal(new string('*', 50));
                }
            }

            var endpoint = HapiSettingsView.Url;
            //var endpoint = configuration["endpoints:iqcare"];
            var connectionString = HapiSettingsView.Connection;
            //var connectionString = configuration.GetConnectionString("hAPIConnection");
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
                Log.Fatal($"{e}");
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

            Log.Debug("loading Sync...");

            try
            {
                SyncConfigScheduler = ServiceProvider.GetService<ISyncConfigScheduler>();
                SyncConfigScheduler.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e,"Sync startup error");
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