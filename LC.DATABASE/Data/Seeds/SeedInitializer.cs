using LC.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public static class SeedInitializer
    {
        public static async Task Run(LegalConnectionContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await DepartmentSeed.Seed(context);
            await ProvinceSeed.Seed(context);
            await DistrictSeed.Seed(context);
            await PlanSeed.Seed(context);
            await ApplicationRoleSeed.Seed(roleManager);
            await ApplicationUserSeed.Seed(userManager, context);
            await LanguageSeed.Seed(context);
            await SpecialitySeed.Seed(context);
            await ConfigurationSeed.Seed(context);
        }

    }
}
