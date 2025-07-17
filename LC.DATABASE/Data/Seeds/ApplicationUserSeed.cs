using LC.CORE.Helpers;
using LC.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public class ApplicationUserSeed
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, LegalConnectionContext context)
        {
            var districtId = await context.Districts.Select(x=>x.Id).FirstOrDefaultAsync();

            var admin = new ApplicationUser
            {
                UserName = "admin@legalconnection.pe",
                Email = "admin@legalconnection.pe",
                DistrictId = districtId,
                Name = "JAVIER",
                Surnames = "ARAGON MONTESDEOCA",
                PhoneNumber = "999999999",
                EmailConfirmed = true
            };

            var adviser = new ApplicationUser
            {
                UserName = "asesor@legalconnection.pe",
                Email = "asesor@legalconnection.pe",
                DistrictId = districtId,
                Name = "PATRICIA",
                Surnames = "DEL CASTILLO PADRON",
                PhoneNumber = "999999998",
                EmailConfirmed = true
            };

            //var lawyer = new ApplicationUser
            //{
            //    UserName = "abogado@legalconnection.pe",
            //    Email = "abogado@legalconnection.pe",
            //    DistrictId = districtId,
            //    Name = "ANDRES",
            //    Surnames = "ESCOBEDO POLO",
            //    PhoneNumber = "999999997",
            //    EmailConfirmed = true
            //};

            //var client = new ApplicationUser
            //{
            //    UserName = "cliente@legalconnection.pe",
            //    Email = "cliente@legalconnection.pe",
            //    DistrictId = districtId,
            //    Name = "ALFONSO",
            //    Surnames = "MARCH RABADAN",
            //    PhoneNumber = "999999996",
            //    EmailConfirmed = true
            //};

            //var layout_artist= new ApplicationUser
            //{
            //    UserName = "maquetador@legalconnection.pe",
            //    Email = "maquetador@legalconnection.pe",
            //    DistrictId = districtId,
            //    Name = "RICARDO",
            //    Surnames = "MARCH LOPS",
            //    PhoneNumber = "999999995",
            //    EmailConfirmed = true
            //};

            await userManager.CreateAsync(admin, "legalconnection");
            await userManager.CreateAsync(adviser, "legalconnection");
            //await userManager.CreateAsync(lawyer, "legalconnection");
            //await userManager.CreateAsync(client, "legalconnection");
            //await userManager.CreateAsync(layout_artist, "legalconnection");

            await userManager.AddToRoleAsync(admin, ConstantHelpers.ROLES.ADMIN);
            await userManager.AddToRoleAsync(adviser, ConstantHelpers.ROLES.ADVISER);
            //await userManager.AddToRoleAsync(lawyer, ConstantHelpers.ROLES.LAWYER);
            //await userManager.AddToRoleAsync(client, ConstantHelpers.ROLES.CLIENT);
            //await userManager.AddToRoleAsync(layout_artist, ConstantHelpers.ROLES.LAYOUT_ARTIST);

            //var lawyerEntity = new Lawyer
            //{
            //    UserId = lawyer.Id,
            //    Status = ConstantHelpers.ENTITIES.LAWYER.STATUS.PENDING
            //};

            //var clientEntity = new Client
            //{
            //    UserId = client.Id
            //};

            //await context.Lawyers.AddAsync(lawyerEntity);
            //await context.Clients.AddAsync(clientEntity);

            await context.SaveChangesAsync();
        }
    }
}
