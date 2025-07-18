using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LC.DATABASE.Data
{
    public class LegalConnectionContextFactory : IDesignTimeDbContextFactory<LegalConnectionContext>
    {
        public LegalConnectionContextFactory()
        {

        }

        public LegalConnectionContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<LegalConnectionContext>();
            builder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds);
                options.EnableRetryOnFailure();
            });

            return new LegalConnectionContext(builder.Options);
        }
    }
}
