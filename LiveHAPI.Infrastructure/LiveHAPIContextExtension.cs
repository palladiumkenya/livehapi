using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

            if (!context.Counties.Any())
            {
                var counties = new List<County>();
                counties.Add(new County(1,"Nairobi"));
                counties.Add(new County(2, "Bomet"));
                //                var counties = JsonConvert.DeserializeObject<List<County>>(
                //                    File.ReadAllText("seed" + Path.DirectorySeparatorChar + "Counties.json"));
                context.AddRange(counties);
                context.SaveChanges();
            }

            //Ensure we have some status
            if (!context.SubCounties.Any())
            {
                var subCounties=new List<SubCounty>();
                subCounties.Add(new SubCounty("Kibera",1,1));
                subCounties.Add(new SubCounty("Langata",2, 1));

                subCounties.Add(new SubCounty("Sotik", 3, 2));
                

                //                var subCounties = JsonConvert.DeserializeObject<List<SubCounty>>(
                //                    File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "status.json"));
                context.AddRange(subCounties);
                context.SaveChanges();

            }

        }
    }
}