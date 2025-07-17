using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Lawyer;
using LC.WEB.Hubs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN + "," + ConstantHelpers.ROLES.ADVISER)]
    [Area("Admin")]
    [Route("admin/abogados")]
    public class LawyerController : Controller
    {
        private readonly IDataTableService _dataTableService;
        private readonly IUserService _userService;
        private readonly IHubContext _hubContext;
        private readonly ILawyerInterviewService _lawyerInterviewService;
        private readonly ILawyerService _lawyerService;
        private readonly ITemporalLawyerService _temporalLawyerService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly ILawyerObservationService _lawyerObservationService;

        public LawyerController(
            IDataTableService dataTableService,
            IUserService userService,
            IHubContext hubContext,
            ILawyerInterviewService lawyerInterviewService,
            ILawyerService lawyerService,
            ITemporalLawyerService temporalLawyerService,
            IEmailTemplateService emailTemplateService,
            IEmailService emailService,
            ILawyerObservationService lawyerObservationService
            )
        {
            _dataTableService = dataTableService;
            _userService = userService;
            _hubContext = hubContext;
            _lawyerInterviewService = lawyerInterviewService;
            _lawyerService = lawyerService;
            _temporalLawyerService = temporalLawyerService;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _lawyerObservationService = lawyerObservationService;
        }

        [HttpGet("registrados")]
        public IActionResult Validated()
        {
            return View();
        }

        [HttpGet("nuevos")]
        public IActionResult Pending()
        {
            return View();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetLawyersDatatable(byte status, string search,bool? onlyNewLawyer = null)
        {
            var sentparameters = _dataTableService.GetSentParameters();
            var result = await _lawyerService.GetLawyersDatatable(sentparameters, status, search, onlyNewLawyer);
            return Ok(result);
        }

        [HttpGet("perfil/{lawyerId}")]
        public async Task<IActionResult> LawyerProfile(Guid lawyerId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            var user = await _userService.Get(lawyer.UserId);

            var model = new LawyerInfoViewModel
            {
                LawyerId = lawyer.Id,
                Status = lawyer.Status,
                RegisterDate = user.CreatedAt.ToLocalDateTimeFormat(),
                ValidatedDate = lawyer.ValidationDate.ToLocalDateTimeFormat()
            };

            if(lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.IN_EVALUATION)
            {
                var lastObservation = await _lawyerObservationService.GetLastObservationByType(lawyerId, ConstantHelpers.ENTITIES.LAWYER_OBSERVATION.PROCESS.VALIDATION_PROFILE);
                model.LawyerObservation = new LawyerObservationViewModel
                {
                    LawyerId = lawyer.Id
                };

                if(lastObservation != null)
                {
                    model.LawyerObservation.HasObservations = true;
                    model.LawyerObservation.HasBeenCorrected = lastObservation.HasBeenCorrected;
                    model.LawyerObservation.Observations = lastObservation.Observation;
                }
            }
            else if(lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.INTERVIEW_VALIDATED) 
            {
                var interviews = await _lawyerInterviewService.GetInterviewsByLawyer(lawyer.Id);
                model.RequestInterviews = interviews
                    .Select(x => new RequestInterviewViewModel
                    {
                        Date = x.Date.ToLocalDateFormat(),
                        EndRange = x.EndRange.ToLocalDateTimeFormatUtc(),
                        StartRange = x.StartRange.ToLocalDateTimeFormatUtc(),
                        Selected = x.Selected
                    })
                    .ToList();
            }
            else if(lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                model.ProfileWithChanges = lawyer.ProfileWithChanges;
            }

            return View(model);
        }

        [HttpPost("validar-perfil-abogado")]
        public async Task<IActionResult> AcceptLawyer(Guid lawyerId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            lawyer.Status = ConstantHelpers.ENTITIES.LAWYER.STATUS.PROFILE_VALIDATED;
            lawyer.ValidationDate = DateTime.UtcNow;
            await _lawyerService.Update(lawyer);
            return Ok();
        }

        [HttpPost("enviar-observaciones-abogado")]
        public async Task<IActionResult> SendObservations(LawyerObservationViewModel model)
        {
            var lawyer = await _lawyerService.Get(model.LawyerId);
            var user = await _userService.Get(lawyer.UserId);
            lawyer.ValidationDate = DateTime.UtcNow;
            var observation = new LawyerObservation
            {
                LawyerId = lawyer.Id,
                Observation = model.Observations,
                Process = ConstantHelpers.ENTITIES.LAWYER_OBSERVATION.PROCESS.VALIDATION_PROFILE
            };

            var modelEmail = new StandardEmailModel
            {
                Title = "Perfil con Observaciones",
                SubHeader = model.Observations,
                LinkName = "Ir a mi perfil",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/abogado/perfil"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
            await _emailService.SendEmail("Perfil con Observaciones", template, user.Email);
            await _lawyerObservationService.Insert(observation);
            await _lawyerService.Update(lawyer);
            await _hubContext.SendNotification("Perfil con Observaciones", $"/abogado/perfil", user.Id);
            return Ok();
        }

        [HttpPost("solicitar-entrevista")]
        public async Task<IActionResult> RequestInterview(LawyerInfoViewModel model)
        {
            if (model.RequestInterviews is null || !model.RequestInterviews.Any())
                return BadRequest("Es necesario registrar por lo menos una solicitud de entrevista.");

            var lawyer = await _lawyerService.Get(model.LawyerId);
            var user = await _userService.Get(lawyer.UserId);
            lawyer.ValidationDate = DateTime.UtcNow;
            lawyer.Status = ConstantHelpers.ENTITIES.LAWYER.STATUS.INTERVIEW_VALIDATED;
            await _lawyerService.Update(lawyer);

            var interviews = model.RequestInterviews
                .Select(x => new LawyerInterview
                {
                    Date = ConvertHelpers.DatepickerToUtcDateTime(x.Date),
                    StartRange = ConvertHelpers.TimepickerToUtcTimeSpan(x.StartRange),
                    EndRange = ConvertHelpers.TimepickerToUtcTimeSpan(x.EndRange),
                    LawyerId = model.LawyerId
                })
                .ToList();

            await _lawyerInterviewService.InsertRange(interviews);

            var modelEmail = new LawyerInterviewEmailModel
            {
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/abogado/perfil",
                LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                Details = interviews.Select(x => new LawyerInterviewDetailEmailModel
                {
                    Date = x.Date.ToLocalDateFormat(),
                    EndRange = x.EndRange.ToLocalDateTimeFormatUtc(),
                    StartRange = x.StartRange.ToLocalDateTimeFormatUtc()
                })
                .ToList()
            };

            var template = await _emailTemplateService.GetLawyerInterviewEmailTemplate(modelEmail);
            await _emailService.SendEmail("Solicitud de Entrevista", template, user.Email);
            return Ok();
        }

        [HttpPost("aceptar-abogado")]
        public async Task<IActionResult> ValidateLawyer(Guid lawyerId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            var user = await _userService.Get(lawyer.UserId);
            lawyer.Status = ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED;
            lawyer.ValidationDate = DateTime.UtcNow;

            if (string.IsNullOrEmpty(lawyer.CAL))
                return BadRequest("El abogado no ha llenado el campo CAL.");

            if (await _lawyerService.AnyByCAL(lawyer.CAL, lawyer.Id))
                return BadRequest("Se encontró un abogado registrado con el mismo CAL.");

            lawyer.Code = $"{DateTime.UtcNow.ToDefaultTimeZone().Year}{lawyer.CAL}";

            var modelEmail = new StandardEmailModel
            {
                Title = "Perfil Aceptado",
                SubHeader = "Hemos aceptado tu perfil profesional en base a la evaluación realizada, puesto que consideramos que juntos podemos crecer y promover los servicios legales a través de la tecnología. Recuerda que debes escoger el plan al que te suscribirás (free o premium). Escoge el que mejor se acomode a tu necesidad y comencemos a trabajar juntos. Ingresa al siguiente link para conocer sobre las nuevas funcionalidades para ti.",
                LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
            await _emailService.SendEmail("Perfil Aceptado", template, user.Email);
            await _lawyerService.Update(lawyer);
            await _hubContext.SendNotification("Perfil Aceptado", $"/abogado/perfil", user.Id);
            return Ok();
        }

        [HttpPost("rechazar-abogado")]
        public async Task<IActionResult> RejectLawyer(Guid lawyerId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            var user = await _userService.Get(lawyer.UserId);
            lawyer.Status = ConstantHelpers.ENTITIES.LAWYER.STATUS.REJECTED;
            lawyer.ValidationDate = DateTime.UtcNow;

            var modelEmail = new LawyerRejectedEmailModel
            {
                Lawyer = $"{user.Name} {user.Surnames}"
            };

            var template = await _emailTemplateService.GetLawyerRejectedEmailTemplate(modelEmail);
            await _emailService.SendEmail("Perfil Rechazado", template, user.Email);
            await _lawyerService.Update(lawyer);
            return Ok();
        }

        [HttpPost("aceptar-cambios-abogado")]
        public async Task<IActionResult> AcceptProfileWithChanges(Guid lawyerId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            await _temporalLawyerService.AcceptProfileChanges(lawyerId);

            await _hubContext.SendNotification("Cambios del perfil validados", "/abogado/perfil", lawyer.UserId);
            return Ok();
        }

        [HttpPost("rechazar-cambios-abogado")]
        public async Task<IActionResult> RejectProfileWithChanges(Guid lawyerId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            await _temporalLawyerService.RejectProfileChanges(lawyerId);

            await _hubContext.SendNotification("Cambios del perfil rechazados", "/abogado/perfil", lawyer.UserId);
            return Ok();
        }
    }
}
