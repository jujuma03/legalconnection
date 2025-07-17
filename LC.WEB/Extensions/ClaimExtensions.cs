using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LC.WEB.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(v => v.Type == ClaimTypes.Email).Value;
            }

            return "";
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                List<Claim> a = user.Claims.ToList();
                return user.Claims.FirstOrDefault(v => v.Type == "MainName").Value;
            }

            return "";
        }

        public static string GetPictureUrl(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                List<Claim> a = user.Claims.ToList();
                var result = user.Claims.FirstOrDefault(v => v.Type == "PictureUrl").Value;
                return result;
            }

            return "";
        }

        public static string GetFullName(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                List<Claim> a = user.Claims.ToList();
                return user.Claims.FirstOrDefault(v => v.Type == ClaimTypes.UserData).Value;
            }

            return "";
        }

        public static bool LawyerIfValidated(this ClaimsPrincipal user)
        {
            var result = false;

            if (user.Identity.IsAuthenticated)
            {
                if (user.IsInRole(ConstantHelpers.ROLES.LAWYER))
                {
                    List<Claim> a = user.Claims.ToList();
                    var claim = user.Claims.FirstOrDefault(v => v.Type == "LawyerValidated").Value;
                    Boolean.TryParse(claim, out result);
                }
            }

            return result;
        }
    }
}
