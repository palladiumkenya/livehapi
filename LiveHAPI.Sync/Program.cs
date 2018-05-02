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
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Profiles;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LiveHAPI.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.RollingFile("logs\\hapisync-{Date}.txt")
                .CreateLogger();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var endpoint = configuration["endpoints:iqcare"];
            var connectionString = configuration.GetConnectionString("hAPIConnection");

            Log.Debug($"configured endpoint");
            Log.Debug(new string('-', 40));
            Log.Debug($"    {endpoint}");
            Log.Debug(new string('-', 40));
            Log.Debug($"configured connection");
            Log.Debug(new string('-', 40));
            Log.Debug($"    {connectionString}");
            Log.Debug(new string('-', 40));

//            var connectionString = Startup.Configuration["connectionStrings:hAPIConnection"];
//            services.AddDbContext<LiveHAPIContext>(o => o.UseSqlServer(connectionString));
//
//            var emrconnectionString = Startup.Configuration["connectionStrings:EMRConnection"];
//            services.AddDbContext<EMRContext>(o => o.UseSqlServer(emrconnectionString));

            var serviceProvider = new ServiceCollection()

                .AddSingleton<IRestClient>(new RestClient(configuration["endpoints:iqcare"]))
                .AddDbContext<LiveHAPIContext>(o => o.UseSqlServer(connectionString))
                
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IPracticeRepository, PracticeRepository>()
                .AddScoped<IPersonRepository, PersonRepository>()

                .AddScoped<IClientUserReader, ClientUserReader>()
                .AddScoped<IClientFacilityReader, ClientFacilityReader>()

                .AddScoped<ISyncUserService, SyncUserService>()
                .AddScoped<ISyncFacilityService, SyncFacilityService>()

                .BuildServiceProvider();

            Log.Debug("starting Sync...");

            Mapper.Initialize(cfg => { cfg.AddProfile<ClientProfile>(); });

            //readusers
            var reader = serviceProvider.GetService<IClientUserReader>();
            var users = reader.Read().Result.ToList();
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }

            Log.Debug(new string('-', 40));
            //readfacs
            var facReader = serviceProvider.GetService<IClientFacilityReader>();
            var facilities = facReader.Read().Result.ToList();
            foreach (var facility in facilities)
            {
                Console.WriteLine(facility);
            }

            Console.ReadKey();
        }
    }
}