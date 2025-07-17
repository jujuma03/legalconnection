using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.DATABASE.Data;
using LC.DATABASE.Data.Seeds;
using LC.ENTITIES.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LC.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

            //Seeder
            //using (IServiceScope scope = hostBuilder.Services.CreateScope())
            //{
            //    IServiceProvider serviceProvider = scope.ServiceProvider;

            //    try
            //    {
            //        LegalConnectionContext context = serviceProvider.GetRequiredService<LegalConnectionContext>();
            //        RoleManager<ApplicationRole> roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            //        UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //        context.Database.EnsureCreated();
            //        SeedInitializer.Run(context, roleManager, userManager).Wait();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception("An error occurred while seeding the database.", e);
            //    }
            //}

            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
