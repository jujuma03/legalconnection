using LC.CORE.Helpers;
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
    public class LawyerValidatedFilterAttribute : TypeFilterAttribute
    {
        public LawyerValidatedFilterAttribute() : base(typeof(LawyerValidateFilterAttributeImp)) { }

        private class LawyerValidateFilterAttributeImp : IAsyncActionFilter
        {
            protected readonly ClaimsPrincipal _principal;
            private readonly IUserService _userService;
            private readonly ILawyerService _lawyerService;

            public LawyerValidateFilterAttributeImp
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
                var validation = true;

                if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
                {
                    if (!lawyer.TermsAndConditions)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                        _ = next();
                        validation = false;
                    }
                }
                else
                {
                    if (context.Controller.GetType() != typeof(LC.WEB.Areas.Lawyer.Controllers.ProfileController))
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Profile" }, { "action", "Index" }, { "area", "Lawyer" } });
                        _ = next();
                        validation = false;
                    }
                }

                if (validation)
                {
                    await next();
                }
            }
        }
    }
}
