using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiveHAPI
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            //TODO: Use Environment Variables

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                .AddJsonOptions(o =>o.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var connectionString = Startup.Configuration["connectionStrings:hAPIConnection"];
            services.AddDbContext<LiveHAPIContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
            services.AddScoped<IObsRepository, ObsRepository>();
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

            services.AddScoped<IMetaService, MetaService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IActivationService, ActivationService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IEncounterService, EncounterService>();
            services.AddScoped<IFormsService, FormsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,LiveHAPIContext context)
        {
            loggerFactory.AddLog4Net();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

//                                    if (!context.AllMigrationsApplied())
//                                    {
//                                        context.Database.Migrate();
//                                        context.EnsureSeeded();
//                                    }
                        

           context.EnsureSeeded();

            app.UseMvc();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<County,CountyDTO>();
            });

            //            app.Run(async (context) =>
            //            {
            //                await context.Response.WriteAsync("Hello World!");
            //            });
        }
    }
}
