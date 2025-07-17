using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LC.WEB.Areas.Report.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Area("Report")]
    [Route("reportes/usuarios-nuevos")]
    public class NewUserController : Controller
    {
        private readonly IDataTableService _dataTableService;
        private readonly IUserService _userService; 
        public NewUserController(IDataTableService dataTableService,
            IUserService userService)
        {
            _dataTableService = dataTableService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _userService.GetUserNewDatatable(parameters, searchValue);
            return Ok(result);
        }
    }
}
