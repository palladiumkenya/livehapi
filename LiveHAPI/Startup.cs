﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace LiveHAPI
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
//                .AddJsonOptions(o =>
//                {
//                    if (o.SerializerSettings.ContractResolver != null)
//                    {
//                        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
//                        castedResolver.NamingStrategy=null;
//                    }
//                });

            var connectionString = @"Server=(localdb)\\mssqllocaldb;Database=LiveHAPI;Trusted_Connection=True;";
            services.AddDbContext<LiveHAPIContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            loggerFactory.AddDebug(LogLevel.Error);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
//            app.Run(async (context) =>
//            {
//                await context.Response.WriteAsync("Hello World!");
//            });
        }
    }
}
