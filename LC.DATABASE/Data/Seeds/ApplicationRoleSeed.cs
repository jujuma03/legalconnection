using LC.CORE.Helpers;
using LC.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public class ApplicationRoleSeed
    {
        public static async Task Seed(RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.ADMIN });
            await roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.ADVISER});
            await roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.CLIENT});
            await roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.LAWYER });
            await roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.LAYOUT_ARTIST });
            await roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.TREASURER });
        }
    }
}
