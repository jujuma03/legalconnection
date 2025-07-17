using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Benefit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/beneficios")]
    [Area("Admin")]
    public class BenefitController : Controller
    {
        private readonly IDataTableService _dataTableService;
        private readonly IBenefitService _benefitService;

        public BenefitController(
            IDataTableService dataTableService,
            IBenefitService benefitService
            )
        {
            _dataTableService = dataTableService;
            _benefitService = benefitService;
        }

        public IActionResult Index()
            => View();

        [HttpGet("get")]
        public async Task<IActionResult> GetBenefitsDatatable(string searchValue)
        {
            var sentparameters = _dataTableService.GetSentParameters();
            var result = await _benefitService.GetBenefitsDatatable(sentparameters, searchValue);
            return Ok(result);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AddBenefit(BenefitViewModel model)
        {
            var entity = new Benefit
            {
                Description = model.Description
            };

            await _benefitService.Insert(entity);
            return Ok();
        }

        [HttpGet("get-beneficio")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _benefitService.Get(id);
            return Ok(entity);
        }

        [HttpPost("editar")]
        public async Task<IActionResult> EditBenefit(BenefitViewModel model)
        {
            var entity = await _benefitService.Get(model.Id.Value);
            entity.Description = model.Description;

            await _benefitService.Update(entity);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> DeleteBenefit(Guid id)
        {
            var entity = await _benefitService.Get(id);
            await _benefitService.Delete(entity);
            return Ok();
        }
    }
}
