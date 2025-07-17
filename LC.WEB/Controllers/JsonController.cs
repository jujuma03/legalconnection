using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.CORE.Structs;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [AllowAnonymous]
    public class JsonController : Controller
    {
        private readonly ISelect2Service _select2Service;
        private readonly ISpecialityThemeService _specialityThemeService;
        private readonly IDistrictService _districtService;
        private readonly IDepartmentService _departmentService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IProvinceService _provinceService;
        private readonly ILanguageService _languageService;
        private readonly ISpecialityService _specialityService;
        private readonly ILegalCaseService _legalCaseService;
        private readonly ILawyerService _lawyerService;
        private readonly ILegalCaseLawyerService _legalCaseLawyerService;

        private readonly IUserService _userService;
        public JsonController(
            ISelect2Service select2Service,
            ISpecialityThemeService specialityThemeService,
            IDistrictService districtService,
            IDepartmentService departmentService,
            ICloudStorageService cloudStorageService,
            IProvinceService provinceService,
            ILanguageService languageService,
            ISpecialityService specialityService,
            ILegalCaseService legalCaseService,
            ILawyerService lawyerService,
            ILegalCaseLawyerService legalCaseLawyerService,
            IUserService userService
            )
        {
            _select2Service = select2Service;
            _specialityThemeService = specialityThemeService;
            _districtService = districtService;
            _departmentService = departmentService;
            _cloudStorageService = cloudStorageService;
            _provinceService = provinceService;
            _languageService = languageService;
            _specialityService = specialityService;
            _legalCaseService = legalCaseService;
            _lawyerService = lawyerService;
            _legalCaseLawyerService = legalCaseLawyerService;
            _userService=userService;
        }

        [AllowAnonymous]
        [HttpGet("documentos/{*path}")]
        public async Task DownloadDocument(string path)
        {
            using (var mem = new MemoryStream())
            {
                await _cloudStorageService.Download(mem, path);

                // Download file
                var fileName = Path.GetFileName(path);
                var text = $"attachment;filename=\"{fileName.Normalize().Replace(' ', '_')}\"";
                HttpContext.Response.Headers["Content-Disposition"] = text;
                mem.Position = 0;
                await mem.CopyToAsync(HttpContext.Response.Body);
            }
        }

        [HttpGet("departamentos/get")]
        public async Task<IActionResult> GetDepartments(string q)
        {
            IEnumerable<Select2Structs.Result> result = await _departmentService.GetDepartmentsSelect2ClientSide(q);
            return Ok(new { items = result });
        }
        [HttpGet("departamentos/get/v3")]
        public async Task<IActionResult> GetDepartmentsV3(string q)
        {
            //var user = await _userService.GetUserByClaim(User);
            //var districts = _districtService.GetAsQueryable();

            //var dis = (user?.DistrictId?? Guid.Empty);
            //var district = districts
            //    .Where(x => x.Id == dis)
            //    .Select(x => new
            //    {
            //        x.Province.DepartmentId,
            //    }).FirstOrDefault();
            //var did = district?.DepartmentId ?? Guid.Empty;

            IEnumerable<Select2Structs.Result> result = await _departmentService.GetUsedDepartmentsSelect2ClientSide();
            return Ok(new { items = result });
        }

        [HttpGet("departamentos/get/v2")]
        public async Task<IActionResult> GetDepartmentsV2()
        {
            var parameters = _select2Service.GetRequestParameters();
            var result = await _departmentService.GetDepartmentSelect2(parameters, parameters.SearchTerm);
            return Ok(result);
        }

        [HttpGet("provincias/get/{did}")]
        public async Task<IActionResult> GetProvinces(Guid did, string q)
        {
            IEnumerable<Select2Structs.Result> result = await _provinceService.GetProvinceSelect2ClientSide(did);

            if (!string.IsNullOrEmpty(q))
            {
                result = result.Where(p => p.Text.Contains(q));
            }

            return Ok(new { items = result });
        }
        [HttpGet("provincias/get/v2")]
        public async Task<IActionResult> GetProvinces(Guid departmentId)
        {
            var parameters = _select2Service.GetRequestParameters();
            var result = await _provinceService.GetProvinceSelect2(parameters, departmentId, parameters.SearchTerm);
            return Ok(result);
        }
        [HttpGet("provincias/get/v3/{did}")]
        public async Task<IActionResult> GetProvincesV3(Guid did, string q, bool first = false)
        {
            var user = await _userService.GetUserByClaim(User);
            var districts = _districtService.GetAsQueryable();
            var dis = (user?.DistrictId?? Guid.Empty);

            var district = districts
                .Where(x => x.Id == dis)
                .Select(x => new
                {
                    x.ProvinceId,
                    x.Province.DepartmentId
                }).FirstOrDefault();

            if (did== Guid.Empty && first)
            {
                did = district?.DepartmentId ?? Guid.Empty;
            }

            IEnumerable<Select2Structs.Result> result = await _provinceService.GetProvinceSelect2ClientSide(did, district?.ProvinceId??Guid.Empty);

            if (!string.IsNullOrEmpty(q))
            {
                result = result.Where(p => p.Text.Contains(q));
            }

            return Ok(new { items = result });
        }

        [HttpGet("distritos/get/{pid}")]
        public async Task<IActionResult> GetDistricts(Guid pid, string q)
        {
            IEnumerable<Select2Structs.Result> result = await _districtService.GetDistrictsSelect2ClientSide(pid);

            if (!string.IsNullOrEmpty(q))
            {
                result = result.Where(d => d.Text.Contains(q));
            }

            return Ok(new { items = result });
        }

        [HttpGet("distritos/get/v2")]
        public async Task<IActionResult> GetDistrictsV2(Guid provinceId)
        {
            var parameters = _select2Service.GetRequestParameters();
            var result = await _districtService.GetDistrictsSelect2(parameters, provinceId, parameters.SearchTerm);
            return Ok(result);
        }

        [HttpGet("idiomas/get/v2")]
        public async Task<IActionResult> GetLanguagesV2()
        {
            var parameters = _select2Service.GetRequestParameters();
            var result = await _languageService.GetLanguagesSelect2(parameters, parameters.SearchTerm);
            return Ok(result);
        }

        [HttpGet("especialidades/get/v2")]
        public async Task<IActionResult> GetSpecialitiesV2(bool colloquialName)
        {
            var parameters = _select2Service.GetRequestParameters();
            var result = await _specialityService.GetSpecialitiesSelect2(parameters, parameters.SearchTerm, colloquialName);
            return Ok(result);
        }

        [HttpGet("especialidades-temas/get/v2")]
        public async Task<IActionResult> GetSpecialityThemesV2(Guid? specialityId, string specialitiesId, bool toProfile, bool colloquialName)
        {
            var parameters = _select2Service.GetRequestParameters();

            if (toProfile)
            {
                var ids = JsonConvert.DeserializeObject<List<Guid>>(specialitiesId);
                var result_toProfile = await _specialityThemeService.GetSpecialityThemesSelect2(parameters, parameters.SearchTerm, null, ids, colloquialName);
                return Ok(result_toProfile);
            }

            var result = await _specialityThemeService.GetSpecialityThemesSelect2(parameters, parameters.SearchTerm, specialityId, null, colloquialName);
            return Ok(result);
        }
        [HttpPost("especialidades-temas/get/v3")]//para el registrar abogado
        public async Task<IActionResult> GetSpecialityThemesV3(string values)
        {
            var ids = values.Split(",").Select(x => Guid.Parse(x)).ToList();
            var result = await _specialityThemeService.GetSpecialityThemesBySpecialitiesId(ids);
            var view = await this.RenderViewToStringAsync("/Views/Account/Partials/_SpecialtyThemesPartial.cshtml", result);

            return Ok(view);
        }

        [HttpGet("get-precio-consulta")]
        public async Task<IActionResult> GetLawyerFee(Guid lawyerId, Guid legalCaseId)
        {
            var legalCaselawyer = await _legalCaseLawyerService.Get(legalCaseId, lawyerId);
            var legalCase = await _legalCaseService.Get(legalCaseId);
            var lawyer = await _lawyerService.Get(lawyerId);

            var time = await _legalCaseService.GetLegalCasesPaymentByClient(lawyerId, legalCase.ClientId);

            if (lawyer.FreeFirst && time == 0)
            {
                return Ok(0);
            }

            if (legalCaselawyer is null)
                return BadRequest("No se pudo obtener los honorarios del abogado seleccionado.");

            var result = Math.Round(legalCaselawyer.Fee * (1 + ConstantHelpers.IGV_PERCENTAGE / 100), 2) * 100;
            return Ok(result);
        }
    }
}
