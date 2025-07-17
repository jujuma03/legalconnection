using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Lawyer.Models.LegalCase;
using LC.WEB.Controllers;
using LC.WEB.Hubs.Interfaces;
using LC.WEB.Services.Hangfire.Interfaces;
using LC.WEB.Services.Hangfire.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.LAWYER)]
    [Area("Lawyer")]
    [Route("abogado/casos")]
    public class LegalCaseController : LawyerBaseController
    {
        private readonly ILegalCaseService _legalCaseService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly ILawyerService _lawyerService;
        private readonly IDistrictService _districtService;
        private readonly IEmailService _emailService;
        private readonly IHubContext _hubContext;
        private readonly IRoleService _roleService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ISpecialityThemeService _specialityThemeService;
        private readonly ISpecialityService _specialityService;
        private readonly IClientService _clientService;
        private readonly IPaginationService _paginationService;
        private readonly ILegalCaseLawyerService _legalCaseLawyerService;
        private readonly IHangfireService _hangfireService;
        private readonly IConfigurationService _configurationService;
        private readonly ILegalCaseObservationService _legalCaseObservationService;

        public LegalCaseController(
            ILegalCaseService legalCaseService,
            IUserService userService,
            IDepartmentService departmentService,
            ILawyerService lawyerService,
            IDistrictService districtService,
            IEmailService emailService,
            IHubContext hubContext,
            IRoleService roleService,
            IEmailTemplateService emailTemplateService,
            ISpecialityThemeService specialityThemeService,
            ISpecialityService specialityService,
            IClientService clientService,
            IPaginationService paginationService,
            ILegalCaseLawyerService legalCaseLawyerService,
            IHangfireService hangfireService,
            IConfigurationService configurationService,
            ILegalCaseObservationService legalCaseObservationService
            )
        {
            _legalCaseService = legalCaseService;
            _userService = userService;
            _departmentService = departmentService;
            _lawyerService = lawyerService;
            _districtService = districtService;
            _emailService = emailService;
            _hubContext = hubContext;
            _roleService = roleService;
            _emailTemplateService = emailTemplateService;
            _specialityThemeService = specialityThemeService;
            _specialityService = specialityService;
            _clientService = clientService;
            _paginationService = paginationService;
            _legalCaseLawyerService = legalCaseLawyerService;
            _hangfireService = hangfireService;
            _configurationService = configurationService;
            _legalCaseObservationService = legalCaseObservationService;
        }
        public async Task<IActionResult> Index(byte? searchType)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var model = await _lawyerService.GetInboxDetail(lawyer.Id);
            ViewBag.DefaultSearchType = searchType.HasValue && searchType != 0 ? searchType : 1;
            return View(model);
        }

        [HttpGet("get-items")]
        public async Task<IActionResult> GetLegalCaseItems(byte searchType)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var model = await _legalCaseService.GetLegalCaseItemsToLawyer(parameters, lawyer.Id, searchType);
            return PartialView("Partials/Views/LegalCaseItemPartialView", model);
        }

        [HttpGet("{legalCaseId}")]
        public async Task<IActionResult> Detail(Guid legalCaseId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (!await _legalCaseService.LawyerHasAccess(lawyer.Id, legalCaseId))
                return BadRequest("No se puede ver los detalles del caso seleccionado");

            var model = await _legalCaseService.GetLegalCaseToLawyer(legalCaseId, lawyer.Id);
            return View(model);
        }

        [HttpGet("get-caso")]
        public async Task<IActionResult> GetLegalCase(Guid legalCaseId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var legalCase = await _legalCaseService.GetLegalCaseToLawyerCustomModel(legalCaseId,lawyer.Id);
            return PartialView("Partials/Views/LegalCaseDetailPartialView", legalCase);
        }

        [HttpPost("postular-caso")]
        public async Task<IActionResult> PostulateLegalCase(PostulateViewModal model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var entity = new LegalCaseApplicantLawyer
            {
                LawyerId = lawyer.Id,
                ApplicationDate = DateTime.UtcNow,
                LegalCaseId = model.LegalCaseId,
                ResponseTime = model.ResponseTime
            };

            var result = await _legalCaseService.Postulate(entity);

            if(await _legalCaseService.LegalCaseApplicantsCompleted(model.LegalCaseId))
            {
                await _hangfireService.ExecuteLEgalCaseDelayedTask(new ExecuteLegalCaseTask
                {
                    LegalCaseId = model.LegalCaseId,
                    Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_APPLICATION_TIME
                });
            }

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("aceptar-caso")]
        public async Task<IActionResult> AcceptCase(PostulateViewModal model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var legalCase = await _legalCaseService.Get(model.LegalCaseId);
            var client = await _clientService.Get(legalCase.ClientId);
            var userClient = await _userService.Get(client.UserId);

            var entity = new LegalCaseLawyer
            {
                LawyerId = lawyer.Id,
                LegalCaseId = model.LegalCaseId,
                ResponseTime = model.ResponseTime
            };

            var result = await _legalCaseService.AcceptCase(entity);

            if (!result.Success)
                return BadRequest(result.Message);

            await _hubContext.SendNotification("El abogado aceptó el caso directo", $"/mis-casos/{legalCase.Id}/detalle", client.UserId);

            var configurationPayTime = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_PAY_LAWYER);
            var max_time_to_client_pay_lawyer = Convert.ToInt32(configurationPayTime.Value);

            var modelTemplate = new StandardEmailModel
            {
                LinkName = "Ir al Caso",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/mis-casos/{legalCase.Id}/detalle",
                SubHeader = $"Se detallo la siguiente información : Tarifa - S/.{lawyer.Fee:F}. Recuerda que tienes hasta {max_time_to_client_pay_lawyer} horas para realizar el depósito de la consulta; de lo contrario, tu caso será anulado.",
                Title = "El abogado ha aceptado tu caso"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(modelTemplate);
            await _emailService.SendEmail("Caso Directo", template, userClient.Email);

            await _hangfireService.ExecuteLEgalCaseDelayedTask(new ExecuteLegalCaseTask
            {
                LegalCaseId = model.LegalCaseId,
                Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_TIME_TO_LAWYER_ACCEPT
            });

            return Ok();
        }

        [HttpPost("cerrar-caso")]
        public async Task<IActionResult> CloseCase(PostulateViewModal model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var result = await _legalCaseService.CloseLegalCase(model.LegalCaseId, lawyer.Id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("archivar-caso")]
        public async Task<IActionResult> FiledLegalCase(Guid legalCaseId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var result = await _legalCaseService.FiledLegalCase(lawyer.Id, legalCaseId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("reportar-abandono")]
        public async Task<IActionResult> ReportAbandonment(ReportAbandonmentViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var role = await _roleService.GetRoleByName(ConstantHelpers.ROLES.LAWYER);
            var legalCase = await _legalCaseService.Get(model.LegalCaseId);

            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var legalCaseLawyers = await _legalCaseLawyerService.Get(model.LegalCaseId, lawyer.Id);
            if (legalCaseLawyers == null)
                return BadRequest("No se encontró el caso asociado al abogado");
            legalCaseLawyers.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.ABANDONMENT;


            var legalCaseObservation = new LegalCaseObservation
            {
                LegalCaseId = legalCase.Id,
                Observation = model.Observation,
                Process = ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.ABANDONMENT,
                CreatorRoleId = role.Id,
                CreatorUserId = user.Id
            };

            await _legalCaseObservationService.Insert(legalCaseObservation);
            await _legalCaseService.Update(legalCase);


            return Ok();
        }

        [HttpPost("rechazar-caso")]
        public async Task<IActionResult> RejectLegalCase(Guid legalCaseId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var legalCase = await _legalCaseService.Get(legalCaseId);

            var result = await _legalCaseService.RejectDirectedLegalCase(lawyer.Id, legalCaseId);
            if (!result.Success)
                return BadRequest(result.Message);

            var client = await _clientService.Get(legalCase.ClientId);

            await _hubContext.SendNotification("Caso Rechazado por abogado", $"/mis-casos/{legalCaseId}/detalle", client.UserId);
            return Ok();
        }
    }
}
