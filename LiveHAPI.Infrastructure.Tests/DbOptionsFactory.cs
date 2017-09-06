using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LiveHAPI.Infrastructure.Tests
{
    public static class DbOptionsFactory
    {
        static DbOptionsFactory()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["Data:DefaultConnection:ConnectionString"];

            DbContextOptions = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public static DbContextOptions<LiveHAPIContext> DbContextOptions { get; }

    }
}