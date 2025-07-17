using LC.CORE.Services;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [Authorize]
    [Route("inicio")]
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly ILawyerService _lawyerService;

        public HomeController(
            IUserService userService,
            SignInManager<ApplicationUser> signInManager,
            ILawyerService lawyerService
            )
        {
            _userService = userService;
            _lawyerService = lawyerService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            ViewBag.TermsAndConditions = lawyer.TermsAndConditions;
            return View();
        }

        [HttpPost("aceptar-terminos-condiciones")]
        public async Task<IActionResult> AcceptTermsAndConditions()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            lawyer.TermsAndConditions = true;
            await _lawyerService.Update(lawyer);

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
