using AutoMapper;
using ClosedXML.Excel;
using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.LegalCase;
using LC.WEB.Hubs.Interfaces;
using LC.WEB.Services.Hangfire.Interfaces;
using LC.WEB.Services.Hangfire.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize( Roles = ConstantHelpers.ROLES.ADMIN + "," + ConstantHelpers.ROLES.ADVISER)]
    [Area("Admin")]
    [Route("admin/casos-legales")]
    public class LegalCaseController : Controller
    {
        private readonly ILegalCaseService _legalCaseService;
        private readonly IClientService _clientService;
        private readonly IHangfireService _hangfireService;
        private readonly IRoleService _roleService;
        private readonly ILegalCaseObservationService _legalCaseObservationService;
        private readonly ILegalCaseResponseService _legalCaseResponseService;
        private readonly IDataTableService _dataTableService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ILawyerService _lawyerService;
        private readonly IHubContext _hubContext;
        private readonly ILegalCaseQuestionService _legalCaseQuestionService;
        private readonly IEmailTemplateService _emailTemplateService;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public LegalCaseController(
            ILegalCaseService legalCaseService,
            IClientService clientService,
            IHangfireService hangfireService,
            IRoleService roleService,
            ILegalCaseObservationService legalCaseObservationService,
            ILegalCaseResponseService legalCaseResponseService,
            IDataTableService dataTableService,
            IUserService userService,
            IEmailService emailService,
            ILawyerService lawyerService,
            IHubContext hubContext,
            ILegalCaseQuestionService legalCaseQuestionService,
            IEmailTemplateService emailTemplateService,
            IHostingEnvironment hostingEnvironment,
            IMapper mapper
            )
        {
            _legalCaseService = legalCaseService;
            _clientService = clientService;
            _hostingEnvironment = hostingEnvironment;
            _hangfireService = hangfireService;
            _roleService = roleService;
            _legalCaseObservationService = legalCaseObservationService;
            _legalCaseResponseService = legalCaseResponseService;
            _dataTableService = dataTableService;
            _userService = userService;
            _emailService = emailService;
            _lawyerService = lawyerService;
            _hubContext = hubContext;
            _legalCaseQuestionService = legalCaseQuestionService;
            _emailTemplateService = emailTemplateService;
            _mapper = mapper;
        }

        public IActionResult Index()
            => View();

        [HttpGet("postulaciones")]
        public IActionResult Postulations()
        => View();

        [HttpGet("get-casos")]
        public async Task<IActionResult> GetLegalCases(byte type, byte? status = null, string dateStart = null, string dateEnd = null, string search = null)
        {
            var parameters = _dataTableService.GetSentParameters();
            DateTime? startDatetime = null;
            DateTime? endDatetime = null;
            if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
            {
                startDatetime = CORE.Helpers.ConvertHelpers.DatepickerToUtcDateTime(dateStart).Date;
                endDatetime = CORE.Helpers.ConvertHelpers.DatepickerToUtcDateTime(dateEnd).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            

            var result = await _legalCaseService.GetLegalCasesDatatable(parameters, status,type, startDatetime, endDatetime, search);
            return Ok(result);
        }

        [HttpGet("get-casos/get-excel")]
        public async Task<IActionResult> GetLegalCasesExcel(byte type, byte? status = null, string dateStart = null, string dateEnd = null, string search = null)
        {
            DateTime? startDatetime = null;
            DateTime? endDatetime = null;
            if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
            {
                startDatetime = CORE.Helpers.ConvertHelpers.DatepickerToUtcDateTime(dateStart).Date;
                endDatetime = CORE.Helpers.ConvertHelpers.DatepickerToUtcDateTime(dateEnd).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            var data = await _legalCaseService.GetLegalCasesExport(status, type, startDatetime, endDatetime, search);

            var result = _mapper.Map<List<CaseViewModel>>(data);
            var normalizedDate = DateTime.UtcNow.ToDefaultTimeZone();
            var model = new LegalCaseExcelViewModel()
            {
                Day = $"{normalizedDate.Day}",
                Month = ConstantHelpers.MONTHS.VALUES[normalizedDate.Month].ToLower(),
                Year = $"{normalizedDate.Year}",
                Today = DateTime.UtcNow.ToDefaultTimeZone(),
                Logo = Path.Combine(_hostingEnvironment.WebRootPath, $@"images/logo/logo-report.png"),
                JsPath = Path.Combine(_hostingEnvironment.WebRootPath, @"js\lib\jsbarcode\JsBarcode.all.min.js"),
                Result = result
            };

            var dt = new System.Data.DataTable
            {
                TableName = "Casos Legales - Postulación"
            };

            dt.Columns.Add("Codigo");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Dni");
            dt.Columns.Add("Celular");
            dt.Columns.Add("Correo");
            dt.Columns.Add("Fecha de Ingreso");
            dt.Columns.Add("Especialidad");
            dt.Columns.Add("Temas");
            dt.Columns.Add("Abogado");
            dt.Columns.Add("Estado");

            foreach (var item in model.Result)
            {
                dt.Rows.Add(item.Code, item.ClientFullName, item.Dni, item.PhoneNumber,
                    item.Email, item.CreatedAt, item.Speciality, item.SpecialityThemes, item.Lawyer, item.StatusName);
            }

            dt.AcceptChanges();

            var img = Path.Combine(_hostingEnvironment.WebRootPath, $@"images/logo/logo-report.png");

            var fileName = $"Reporte de Casos por Postulacion.xlsx";
            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                var ws = wb.Worksheet(dt.TableName);
                ws.AddHeaderToWorkSheet($"Casos Legales por Postulacioon", img);

                using (var stream = new MemoryStream())
                {
                    HttpContext.Response.Headers["Set-Cookie"] = "fileDownload=true; path=/";
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        [HttpGet("{legalCaseId}/detalles")]
        public async Task<IActionResult> Detail(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.GetLegalCaseToAdmin(legalCaseId);
            return View(legalCase);
        }

        [HttpPost("{legalCaseId}/aceptar")]
        public async Task<IActionResult> AcceptLegalCase(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.Get(legalCaseId);
            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATED;
            legalCase.ValidationDate = DateTime.UtcNow;
            var client = await _clientService.Get(legalCase.ClientId);
            var user = await _userService.Get(client.UserId);

            var emailTemplateModel = new StandardEmailModel
            {
                Title = "¡CASO ACEPTADO!",
                SubHeader = "Su caso ha sido aceptado. Pronto le enviaremos los perfiles de los abogados interesados en su caso. Gracias por su preferencia.",
                LinkName = "Ir al caso",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/mis-casos/{legalCase.Id}/detalle"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateModel);
            await _emailService.SendEmail("Caso Aceptado", template, user.Email);
            await _legalCaseService.Update(legalCase);
            await _hubContext.SendNotification("Caso Aceptado", $"/mis-casos/{legalCase.Id}/detalle", user.Id);

            if(legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL)
            {
                await _hangfireService.CreateLegalCaseDelayedTask(new CreateLegalCaseTask
                {
                    LegalCaseId = legalCase.Id
                });
            }
            else
            {
                await _hangfireService.CreateDirectLegalCaseDelayedTask(new CreateLegalCaseTask
                {
                    LegalCaseId = legalCase.Id
                });
            }

            return Ok();
        }

        [HttpPost("{legalCaseId}/derivar")]
        public async Task<IActionResult> DeriveLegalCase(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.Get(legalCaseId);

            var lawyers = await _lawyerService.GetLawyerToDerivatedLegalCase(legalCase.Id);

            if (legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL)
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER;
                legalCase.DerivedDate = DateTime.UtcNow;
                if (lawyers.Any())
                {
                    var emailTemplateModel = new StandardEmailModel
                    {
                        Title = "¡Nuevo Caso!",
                        SubHeader = "Ha llegado un caso nuevo a tu bandeja de entrada. Ingresa aqui para visualizar el detalle y postula si es de tu interés.",
                        LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                        LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}"
                    };

                    var template = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateModel);
                    await _emailService.SendEmail("Nuevo Caso", template, lawyers.Select(x => x.User.Email).ToArray());
                    await _hubContext.SendNotification("Nuevo caso", $"/abogado/casos/{legalCase.Id}", lawyers.Select(x => x.UserId).ToArray());
                }
            }
            else
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER;
                legalCase.DerivedDate = DateTime.UtcNow;

                if (lawyers.Any())
                {
                    var emailModel = new StandardEmailModel
                    {
                        LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                        LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/abogado/casos/{legalCase.Id}",
                        SubHeader = "Te han contactado directamente para revisar los detalles del caso.Por favor, ingresa al siguiente enlace",
                        Title = "Nuevo Contacto Directo"
                    };

                    var templateEmail = await _emailTemplateService.GetStandardEmailTemplate(emailModel);
                    await _emailService.SendEmail("Nuevo Contacto Directo", templateEmail, lawyers.Select(y => y.User.Email).ToArray());
                    await _hubContext.SendNotification("Nuevo contacto directo", $"/abogado/casos/{legalCase.Id}", lawyers.Select(y => y.UserId).ToArray());
                }
            }

            await _legalCaseService.Update(legalCase);
            return Ok();
        }

        [HttpPost("rechazar")]
        public async Task<IActionResult> RejectLegalCase(RejectViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var roles = await _userService.GetRolesAsync(user);
            var role = await _roleService.GetRoleByName(roles.FirstOrDefault());

            var legalCase = await _legalCaseService.Get(model.LegalCaseId);
            var client = await _clientService.Get(legalCase.ClientId);
            var userClient = await _userService.Get(client.UserId);

            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.REJECTED;
            legalCase.ValidationDate = DateTime.UtcNow;

            var legalCaseObservation = new LegalCaseObservation
            {
                LegalCaseId = model.LegalCaseId,
                Observation = model.Observations,
                Process = ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.VALIDATION,
                CreatorUserId = user.Id,
                CreatorRoleId = role.Id
            };

            await _legalCaseObservationService.Insert(legalCaseObservation);
            await _legalCaseService.Update(legalCase);
            var emailTemplateModel = new StandardEmailModel
            {
                Title = "Caso con Observaciones",
                SubHeader = $"Lamentamos comunicarte que tu caso no ha sido validado. Por favor, revisa el detalle del mismo: {model.Observations}. Recuerda que puedes editar la información para volver a solicitar a un abogado.",
                LinkName = "Ir al caso",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/mis-casos/{legalCase.Id}/detalle"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateModel);
            await _emailService.SendEmail("Caso con Observaciones", template, userClient.Email);
            await _hubContext.SendNotification("Caso con observaciones", $"/mis-casos/{legalCase.Id}/detalle", userClient.Id);

            return Ok();
        }

        [HttpGet("{legalCaseId}/preguntas")]
        public async Task<IActionResult> Question(Guid legalCaseId)
        {
            var questions = await _legalCaseQuestionService.GetLegalCaseQuestions(legalCaseId);
            var legalCase = await _legalCaseService.Get(legalCaseId);

            var model = new LegalCaseViewModel
            {
                Id = legalCaseId,
                Status = legalCase.Status,
                LegalCaseQuestions = questions.Select(x=> new LegalCaseQuestionViewModel
                {
                    Description = x.Description,
                    QuestionId = x.Id,
                    ResponseId = x.LegalCaseResponses.Select(y=>y.Id).FirstOrDefault(),
                    Response = x.LegalCaseResponses.Select(x=>x.Description).FirstOrDefault()
                }).ToList()
            };

            if (model.LegalCaseQuestions.All(x => !string.IsNullOrEmpty(x.Response)))
                model.Completed = true;

            return View(model);
        }

        [HttpPost("preguntas/guardar")]
        public async Task<IActionResult> Save(LegalCaseViewModel model)
        {
            foreach (var item in model.LegalCaseQuestions)
            {
                if (item.ResponseId.HasValue && item.ResponseId != Guid.Empty)
                {
                    var response = await _legalCaseResponseService.Get(item.ResponseId.Value);
                    response.Description = item.Response;
                    await _legalCaseResponseService.Update(response);

                }
                else
                {
                    var entity = new LegalCaseResponse
                    {
                        Description = item.Response,
                        LegalCaseId = model.Id,
                        LegalCaseQuestionId = item.QuestionId
                    };

                    await _legalCaseResponseService.Insert(entity);
                }
            }

            return Ok();
        }

        [HttpGet("get-observacion")]
        public async Task<IActionResult> GetObservations(Guid legalCaseId)
        {
            var result = await _legalCaseObservationService.GetLastPendingObservationByType(legalCaseId, ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.VALIDATION);
            return Ok(result.Observation);
        }

        [HttpGet("get-legalCase")]
        public async Task<IActionResult> GetlegalCase(Guid legalCaseId)
        {
            var result = await _legalCaseObservationService.GetLastPendingObservationByType(legalCaseId, ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.VALIDATION);
            return Ok(result.Observation);
        }
    }
}
