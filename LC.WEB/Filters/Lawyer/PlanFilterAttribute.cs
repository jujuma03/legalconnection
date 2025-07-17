using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LC.WEB.Filters.Lawyer
{
    public class PlanFilterAttribute : TypeFilterAttribute
    {
        public PlanFilterAttribute() : base(typeof(PlanFilterAttributeImp)) { }

        private class PlanFilterAttributeImp : IAsyncActionFilter
        {
            protected readonly ClaimsPrincipal _principal;
            private readonly IUserService _userService;
            private readonly ILawyerService _lawyerService;
            public PlanFilterAttributeImp
                (
                    IHttpContextAccessor httpContextAccessor,
                    IUserService userService,
                    ILawyerService lawyerService
                )
            {
                _principal = httpContextAccessor.HttpContext.User;
                _userService = userService;
                _lawyerService = lawyerService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var user = await _userService.GetUserByClaim(_principal);
                var lawyer = await _lawyerService.GetByUserId(user.Id);

                if (lawyer.FreeUser)
                {
                    await next();
                }
                else
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "YourAccount" }, { "action", "Index" }, { "area", "Lawyer" } });
                    _ = next();
                }
            }
        }
    }
}
