using LC.CORE.Helpers;
using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LC.WEB.Factories
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly LegalConnectionContext _context;

        public ClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            LegalConnectionContext conetxt,
            IOptions<IdentityOptions> optionsAccessor
            )
            :base(userManager,roleManager,optionsAccessor)
        {
            _context = conetxt;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            identity.AddClaims(new[] {
                new Claim(ClaimTypes.UserData, $"{user.Name} {user.Surnames}"),
                new Claim("MainName", $"{user.Name}"),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim("PictureUrl", user.Picture ?? ""),
            });

            if (principal.IsInRole(ConstantHelpers.ROLES.LAWYER))
            {
                var lawyer = await _context.Lawyers.Where(x => x.User.UserName == user.UserName).FirstOrDefaultAsync();
                if(lawyer != null)
                {
                    identity.AddClaim(new Claim("LawyerValidated", $"{lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED}"));
                }
            }

            return principal;
        }
    }
}
