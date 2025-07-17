using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Client.Models.Profile;
using LC.WEB.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Controllers
{
    [Area("Client")]
    [Route("directorio")]
    public class LawyerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDistrictService _districtService;
        private readonly ILawyerService _lawyerService;
        private readonly IPaginationService _paginationService;

        public LawyerController(
            ILawyerService lawyerService,
            IUserService userService,
            IDistrictService districtService,
            IPaginationService paginationService
            )
        {
            _userService = userService;
            _lawyerService = lawyerService;
            _districtService = districtService;
            _paginationService=paginationService;
        }
        public IActionResult Index()
        {
            if (ConstantHelpers.ENABLED_DIRECTORY)
            {
                return View();
            }

            return RedirectToAction("Index", "Portal",new { area="" });
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetLawyers(Guid sp, Guid d, Guid prv, Guid t,bool first, decimal min = -1, decimal max = -1, string stars = null)
        {
            var user = await _userService.GetUserByClaim(User);
            var districts = _districtService.GetAsQueryable();

            if ((d == Guid.Empty&&prv == Guid.Empty) && first)
            {
                var udis = user?.DistrictId??Guid.Empty;
                var district = districts
                     .Where(x => x.Id == udis)
                     .Select(x => new
                     {
                         x.ProvinceId,
                         x.Province.DepartmentId,
                     })
                     .FirstOrDefault();
                d = district?.DepartmentId ?? Guid.Empty;
                prv = district?.ProvinceId?? Guid.Empty;
            }

            var parameters = _paginationService.GetSentParameters();
            var uid = user?.Id ?? "";
            var lawyers = await _lawyerService.GetLawyersDirectoryData(parameters, uid, sp, d, prv, t, min, max, stars);
            var view = await this.RenderViewToStringAsync("/Areas/Client/Views/Lawyer/Partials/_LawyerItems.cshtml", lawyers);
            return Ok(view);
        }
    }
}
