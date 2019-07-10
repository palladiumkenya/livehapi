using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Z.Dapper.Plus;
using Microsoft.Extensions.DependencyInjection;

namespace LiveHAPI.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;

        [OneTimeSetUp]
        public void Init()
        {
            InitLicences();
            InitDI();
        }

        private void InitDI()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:livehAPIConnection"];

                var services = new ServiceCollection()
                    .AddDbContext<LiveHAPIContext>(o => o.UseSqlServer(connectionString));
                      services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
            services.AddScoped<IObsRepository, ObsRepository>();

            services.AddScoped<IObsTraceResultRepository, ObsTraceResultRepository>();
            services.AddScoped<IObsTestResultRepository, ObsTestResultRepository>();
            services.AddScoped<IObsFinalTestResultRepository, ObsFinalTestResultRepository>();
            services.AddScoped<IObsLinkageRepository, ObsLinkageRepository>();
            services.AddScoped<IObsMemberScreeningRepository, ObsMemberScreeningRepository>();
            services.AddScoped<IObsFamilyTraceResultRepository, ObsFamilyTraceResultRepository>();
            services.AddScoped<IObsPartnerScreeningRepository, ObsPartnerScreeningRepository>();
            services.AddScoped<IObsPartnerTraceResultRepository, ObsPartnerTraceResultRepository>();

            services.AddScoped<IPersonNameRepository, PersonNameRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPracticeActivationRepository, PracticeActivationRepository>();
            services.AddScoped<IPracticeRepository, PracticeRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ISubCountyRepository, SubCountyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICountyRepository, CountyRepository>();
            services.AddScoped<IEncounterRepository, EncounterRepository>();
            services.AddScoped<ILookupRepository, LookupRepository>();
            services.AddScoped<ISubscriberSystemRepository, SubscriberSystemRepository>();
            services.AddScoped<ISubscriberConfigRepository, SubscriberConfigRepository>();
            services.AddScoped<IUserSummaryRepository, UserSummaryRepository>();
            services.AddScoped<IClientSummaryRepository, ClientSummaryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IPSmartStoreRepository, PSmartStoreRepository>();
            services.AddScoped<IInvalidMessageRepository, InvalidMessageRepository>();
            services.AddScoped<IClientStageRepository, ClientStageRepository>();
            services.AddScoped<IClientContactNetworkRepository, ClientContactNetworkRepository>();
            ServiceProvider = services.BuildServiceProvider();
        }

        public static void InitDb()
        {
            var context = ServiceProvider.GetService<LiveHAPIContext>();
            context.Database.Migrate();
            context.EnsureSeeded();
            context.CreateViews();

        }
        private void InitLicences()
        {
            DapperPlusManager.AddLicense("1755;701-ThePalladiumGroup", "9005d618-3965-8877-97a5-7a27cbb21c8f");
            if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }
        }
    }
}
