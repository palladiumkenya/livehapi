using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper;

namespace LiveHAPI.Infrastructure.Seeder
{
    public static class InitialSeeder
    {
        public static List<T> ReadCsv<T>() where T : class
        {
            var name = typeof(T).Name;
            var namespce = "LiveHAPI.Infrastructure.Seed";

            var resourceName =  $"{namespce}.{name}.csv";
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            List<T> records;
            using (StreamReader reader = new StreamReader(stream))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = "|";
                csv.Configuration.TrimFields = true;
                csv.Configuration.TrimHeaders = true;
                csv.Configuration.WillThrowOnMissingField = false;
                records = csv.GetRecords<T>().ToList();
            }
            return records;
        }
    }
}
