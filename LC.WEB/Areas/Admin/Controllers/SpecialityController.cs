using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Speciality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Area("Admin")]
    [Route("admin/especialidades")]
    public class SpecialityController : Controller
    {
        private readonly ISpecialityService _specialityService;
        private readonly ISpecialityThemeService _specialityThemeService;
        private readonly IDataTableService _dataTableService;

        public SpecialityController(
            ISpecialityService specialityService,
            ISpecialityThemeService specialityThemeService,
            IDataTableService dataTableService
            )
        {
            _specialityService = specialityService;
            _specialityThemeService = specialityThemeService;
            _dataTableService = dataTableService;
        }


        #region Especialidades
        public IActionResult Index()
            => View();

        [HttpGet("get-datatable")]
        public async Task<IActionResult> GetDatatable(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var datatable = await _specialityService.GetSpecialitiesDatatable(parameters, searchValue);
            return Ok(datatable);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> Add(SpecialityViewModel model)
        {
            if (await _specialityService.AnyByOfficialName(model.OfficialName))
                return BadRequest($"La especialidad con nombre oficial {model.OfficialName} ya se encuentra registrada.");

            if (await _specialityService.AnyByColloquialName(model.OfficialName))
                return BadRequest($"La especialidad con nombre coloquial {model.ColloquialName} ya se encuentra registrada.");

            if (await _specialityService.AnyByCode(model.Code))
                return BadRequest($"Ya se encuentra registrada una especialidad con código {model.Code}.");

            var entity = new Speciality
            {
                ColloquialName = model.ColloquialName,
                OfficialName = model.OfficialName,
                Code = model.Code
            };

            await _specialityService.Insert(entity);

            return Ok();
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Edit(SpecialityViewModel model)
        {
            if (await _specialityService.AnyByOfficialName(model.OfficialName, model.Id))
                return BadRequest($"La especialidad con nombre oficial {model.OfficialName} ya se encuentra registrada.");

            if (await _specialityService.AnyByColloquialName(model.OfficialName, model.Id))
                return BadRequest($"La especialidad con nombre coloquial {model.ColloquialName} ya se encuentra registrada.");

            if (await _specialityService.AnyByCode(model.Code, model.Id))
                return BadRequest($"Ya se encuentra registrada una especialidad con código {model.Code}.");

            var entity = await _specialityService.Get(model.Id.Value);
            entity.OfficialName = model.OfficialName;
            entity.ColloquialName = model.ColloquialName;
            entity.Code = model.Code;
            await _specialityService.Update(entity);
            return Ok();
        }

        [HttpGet("get-specaility")]
        public async Task<IActionResult> GetSpeciality(Guid id)
        {
            var entity = await _specialityService.Get(id);
            return Ok(entity);
        }

        [HttpPost("eliminar-especialidad")]
        public async Task<IActionResult> DeleteSpeciality(Guid id)
        {
            if (await _specialityService.HasSpecialityThemes(id))
                return BadRequest("La especialidad tiene temas asignados.");

            var entity = await _specialityService.Get(id);
            await _specialityService.Delete(entity);
            return Ok();
        }
        #endregion

        #region Temas

        [HttpGet("{specialityId}/temas")]
        public async Task<IActionResult> Themes(Guid specialityId)
        {
            var speciality = await _specialityService.Get(specialityId);
            var model = new ThemeViewModel
            {
                Speciality = speciality.OfficialName,
                SpecialityId = speciality.Id,
                Code = speciality.Code
            };

            return View(model);
        }

        [HttpGet("get-temas-datatable")]
        public async Task<IActionResult> GetThemesDatatable(Guid specialityId, string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var datatable = await _specialityThemeService.GetSpecialityThemesDatatable(parameters, specialityId, searchValue);
            return Ok(datatable);
        }

        [HttpPost("agregar-tema")]
        public async Task<IActionResult> AddTheme(ThemeViewModel model)
        {
            if (await _specialityThemeService.AnyByOfficialName(model.OfficialName))
                return BadRequest($"El tema con nombre oficial {model.OfficialName} ya se encuentra registrada.");

            if (await _specialityThemeService.AnyByColloquialName(model.OfficialName))
                return BadRequest($"El tema con nombre coloquial {model.ColloquialName} ya se encuentra registrada.");

            if (await _specialityThemeService.AnyByCode(model.Code))
                return BadRequest($"Ya se encuentra registrada una especialidad con código {model.Code}.");
            var entity = new SpecialityTheme
            {
                SpecialityId = model.SpecialityId,
                ColloquialName = model.ColloquialName,
                OfficialName = model.OfficialName,
                Code = model.Code
            };

            await _specialityThemeService.Insert(entity);

            return Ok();
        }

        [HttpPost("editar-tema")]
        public async Task<IActionResult> EditTheme(ThemeViewModel model)
        {
            if (await _specialityThemeService.AnyByOfficialName(model.OfficialName, model.Id))
                return BadRequest($"La especialidad con nombre oficial {model.OfficialName} ya se encuentra registrada.");

            if (await _specialityThemeService.AnyByColloquialName(model.OfficialName, model.Id))
                return BadRequest($"La especialidad con nombre coloquial {model.ColloquialName} ya se encuentra registrada.");

            if (await _specialityThemeService.AnyByCode(model.Code, model.Id))
                return BadRequest($"Ya se encuentra registrada una especialidad con código {model.Code}.");

            var entity = await _specialityThemeService.Get(model.Id.Value);
            entity.OfficialName = model.OfficialName;
            entity.ColloquialName = model.ColloquialName;
            entity.Code = model.Code;
            await _specialityThemeService.Update(entity);
            return Ok();
        }

        [HttpGet("get-tema")]
        public async Task<IActionResult> GetSpecialityTheme(Guid id)
        {
            var entity = await _specialityThemeService.Get(id);
            return Ok(entity);
        }

        [HttpPost("eliminar-tema")]
        public async Task<IActionResult> DeleteSpecialityTheme(Guid id)
        {
            if (await _specialityThemeService.HasLawyerAssigned(id))
                return BadRequest("Existen abogados asociados al tema.");

            var entity = await _specialityThemeService.Get(id);
            await _specialityThemeService.Delete(entity);
            return Ok();
        }
        #endregion
    }
}
