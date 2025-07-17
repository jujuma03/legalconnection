using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.PAYMENT.Services.Culqi;
using LC.PAYMENT.Services.Culqi.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Client.Models.LegalCase;
using LC.WEB.Hubs.Interfaces;
using LC.WEB.Services.Hangfire.Interfaces;
using LC.WEB.Services.Hangfire.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN + "," +  ConstantHelpers.ROLES.CLIENT)]
    [Area("Client")]
    [Route("mis-casos")]
    public class LegalCaseController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILegalCaseLawyerService _legalCaseLawyerService;
        private readonly ILegalCaseService _legalCaseService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPaymentService _paymentService;
        private readonly IClientService _clientService;
        private readonly ICulqiService _culquiService;
        private readonly IHangfireService _hangfireService;
        private readonly ILawyerService _lawyerService;
        private readonly ISpecialityService _specialityService;
        private readonly IDistrictService _districtService;
        private readonly IConfigurationService _configurationService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILegalCaseObservationService _legalCaseObservationService;
        private readonly IHubContext _hubContext;
        private readonly ILawyerQualificationService _lawyerQualificationService;
        private readonly IEmailService _emailService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly ISpecialityThemeService _specialityThemeService;
        private readonly IPaginationService _paginationService;
        private readonly IProvinceService _provinceService;
        private readonly ILawyerSpecialityThemeService _lawyerSpecialityThemeService;

        public LegalCaseController(
            IUserService userService,
            ILegalCaseLawyerService legalCaseLawyerService,
            ILegalCaseService legalCaseService,
            IHttpContextAccessor httpContextAccessor,
            IPaymentService paymentService,
            IClientService clientService,
            ICulqiService culquiService,
            IHangfireService hangfireService,
            ILawyerService lawyerService,
            ISpecialityService specialityService,
            IDistrictService districtService,
            IConfigurationService configurationService,
            IEmailTemplateService emailTemplateService,
            ILegalCaseObservationService legalCaseObservationService,
            IHubContext hubContext,
            ILawyerQualificationService lawyerQualificationService,
            IEmailService emailService,
            ICloudStorageService cloudStorageService,
            ISpecialityThemeService specialityThemeService,
            IPaginationService paginationService,
            IProvinceService provinceService,
            ILawyerSpecialityThemeService lawyerSpecialityThemeService
            )
        {
            _userService = userService;
            _legalCaseLawyerService = legalCaseLawyerService;
            _legalCaseService = legalCaseService;
            _httpContextAccessor = httpContextAccessor;
            _paymentService = paymentService;
            _clientService = clientService;
            _culquiService = culquiService;
            _hangfireService = hangfireService;
            _lawyerService = lawyerService;
            _specialityService = specialityService;
            _districtService = districtService;
            _configurationService = configurationService;
            _emailTemplateService = emailTemplateService;
            _legalCaseObservationService = legalCaseObservationService;
            _hubContext = hubContext;
            _lawyerQualificationService = lawyerQualificationService;
            _emailService = emailService;
            _cloudStorageService = cloudStorageService;
            _specialityThemeService = specialityThemeService;
            _paginationService = paginationService;
            _provinceService = provinceService;
            _lawyerSpecialityThemeService = lawyerSpecialityThemeService;
        }

        [HttpGet("agregar")]
        public async Task<IActionResult> Add()
        {
            var user = await _userService.GetUserByClaim(User);
            var specialities = await _specialityService.GetAll();
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var maxlength = Convert.ToInt32(configuration.Value);
            var ubigeoQuery = _districtService.GetAsQueryable();
            var ubigeo = await ubigeoQuery.Where(x => x.Id == user.DistrictId)
                .Select(x => new
                {
                    departmentId = x.Province.DepartmentId,
                    department = x.Province.Department.Name,
                    provinceId = x.ProvinceId,
                    province = x.Province.Name
                })
                .FirstOrDefaultAsync();

            var model = new LegalCaseViewModel
            {
                PhoneNumber = user.PhoneNumber,
                Specialities = specialities.Select(x => new SpecialityViewModel
                {
                    Id = x.Id,
                    Name = x.ColloquialName
                }).ToList(),
                DescriptionMaxLength = maxlength,
            };

            if (ubigeo != null)
            {
                model.DepartmentId = ubigeo.departmentId;
                model.Department = ubigeo.department;
                model.ProvinceId = ubigeo.provinceId;
                model.Province = ubigeo.province;
            }

            return View(model);
        }

        [HttpGet("agregar-dirigido/abogado/{lawyerId}")]
        public async Task<IActionResult> AddDirected(Guid lawyerId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.Get(lawyerId);
            var lawyerSpecialityThemes = await _lawyerSpecialityThemeService.GetSpecialitiesByLawyer(lawyer.Id);
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var maxlength = Convert.ToInt32(configuration.Value);

            var ubigeoQuery = _districtService.GetAsQueryable();
            var ubigeo = await ubigeoQuery.Where(x => x.Id == user.DistrictId)
                .Select(x => new
                {
                    departmentId = x.Province.DepartmentId,
                    department = x.Province.Department.Name,
                    provinceId = x.ProvinceId,
                    province = x.Province.Name
                })
                .FirstOrDefaultAsync();

            var model = new LegalCaseViewModel
            {
                LawyerId = lawyer.Id,
                PhoneNumber = user.PhoneNumber,
                SpecialityThemes = lawyerSpecialityThemes
                .Select(x => new SpecialityThemeViewModel
                {
                    Id = x.SpecialityTheme.Id,
                    Name = x.SpecialityTheme.ColloquialName
                }).ToList(),
                DescriptionMaxLength = maxlength
            };

            if (ubigeo != null)
            {
                model.DepartmentId = ubigeo.departmentId;
                model.Department = ubigeo.department;
                model.ProvinceId = ubigeo.provinceId;
                model.Province = ubigeo.province;
            }

            return View(model);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AddLegalCase(LegalCaseViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var client = await _clientService.GetByUserId(user.Id);
            var workHourStartConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_START);
            var workHourEndConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_END);

            var workHourStart = ConvertHelpers.TimepickerToTimeSpan(workHourStartConfi.Value);
            var workEndStart = ConvertHelpers.TimepickerToTimeSpan(workHourEndConfi.Value);

            if (model.SpecialityThemeId == null || !model.SpecialityThemeId.Any())
                return BadRequest("Es necesario seleccionar, por lo menos, un asunto.");

            user.PhoneNumber = model.PhoneNumber;
            var count = await _legalCaseService.GetLegalCaseCountByType(ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL);
            var province = await _provinceService.Get(model.ProvinceId.Value);
            var speciality = await _specialityService.Get(model.SpecialitySelected);

            var entity = new LegalCase
            {
                ClientId = client.Id,
                Description = model.Description,
                ProvinceId = model.ProvinceId.Value,
                Code = $"01{count:0000}{province.Ubigeo}{speciality.Code}",
                Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS,
                LegalCaseSpecialityThemes = model.SpecialityThemeId.Select(y => new LegalCaseSpecialityTheme
                {
                    SpecialityThemeId = y
                })
                .ToList(),
                Type = ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL
            };

            if (model.File != null)
            {
                if (model.File.Length > 500000)
                    return BadRequest("El archivo puede pesar como máximo 500kb");

                var extension = Path.GetExtension(model.File.FileName);
                entity.UrlFile = await _cloudStorageService.UploadFile(
                    model.File.OpenReadStream(),
                    ConstantHelpers.CLOUD_CONTAINERS.LEGAL_CASE_FILE,
                     $"{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid()}",
                    extension
                    );
            }

            var currentTime = DateTime.UtcNow.ToDefaultTimeZone().TimeOfDay;
            await _legalCaseService.Insert(entity);

            if (!(currentTime >= workHourStart && currentTime <= workEndStart))
            {
                var emailTemplateModel = new StandardEmailModel
                {
                    Title = "¡TU CASO HA SIDO REGISTRADO!",
                    SubHeader = "Lamentablemente, nuestros asesores no están disponibles en este momento. Sin embargo,  en el transcurso de la mañana, se comunicarán contigo para mayor información.",
                    LinkName = "Ir al caso",
                    LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/mis-casos/{entity.Id}/detalle"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateModel);
                await _emailService.SendEmail("Caso Registrado", template, user.Email);
            }

            await _userService.Update(user);
            return Ok();
        }

        [HttpPost("agregar-digirido")]
        public async Task<IActionResult> AddDirectedLegalCase(LegalCaseViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var client = await _clientService.GetByUserId(user.Id);
            var lawyer = await _lawyerService.Get(model.LawyerId);
            var lawyerUser = await _userService.Get(lawyer.UserId);

            var workHourStartConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_START);
            var workHourEndConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_END);

            var workHourStart = ConvertHelpers.TimepickerToTimeSpan(workHourStartConfi.Value);
            var workEndStart = ConvertHelpers.TimepickerToTimeSpan(workHourEndConfi.Value);

            if (model.SpecialityThemeId == null || !model.SpecialityThemeId.Any())
                return BadRequest("Es necesario seleccionar, por lo menos, un asunto.");

            var count = await _legalCaseService.GetLegalCaseCountByType(ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED);
            var province = await _provinceService.Get(model.ProvinceId.Value);
            var firstSpecialityTheme = await _specialityThemeService.Get(model.SpecialityThemeId.FirstOrDefault());
            var speciality = await _specialityService.Get(firstSpecialityTheme.SpecialityId);

            user.PhoneNumber = model.PhoneNumber;
            var entity = new LegalCase
            {
                ClientId = client.Id,
                Description = model.Description,
                ProvinceId = model.ProvinceId.Value,
                Code = $"02{count:0000}{province.Ubigeo}{speciality.Code}",
                Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS,
                Type = ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED,
                LegalCaseSpecialityThemes = model.SpecialityThemeId.Select(y => new LegalCaseSpecialityTheme
                {
                    SpecialityThemeId = y
                })
                .ToList(),
                LegalCaseApplicantLawyers = new List<LegalCaseApplicantLawyer>
                {
                    new LegalCaseApplicantLawyer
                    {
                        LawyerId = model.LawyerId,
                        ResponseTime = 12,
                        Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.DIRECTED
                    }
                }
            };

            if (model.File != null)
            {
                if (model.File.Length > 500000)
                    return BadRequest("El archivo puede pesar como máximo 500kb");

                var extension = Path.GetExtension(model.File.FileName);
                entity.UrlFile = await _cloudStorageService.UploadFile(
                    model.File.OpenReadStream(),
                    ConstantHelpers.CLOUD_CONTAINERS.LEGAL_CASE_FILE,
                     $"{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid()}",
                    extension
                    );
            }

            var currentTime = DateTime.UtcNow.ToDefaultTimeZone().TimeOfDay;
            await _legalCaseService.Insert(entity);

            if (!(currentTime >= workHourStart && currentTime <= workEndStart))
            {
                var emailTemplateModel = new StandardEmailModel
                {
                    Title = "¡TU CASO HA SIDO REGISTRADO!",
                    SubHeader = "Lamentablemente, nuestros asesores no están disponibles en este momento. Sin embargo,  en el transcurso de la mañana, se comunicarán contigo para mayor información.",
                    LinkName = "Ir al caso",
                    LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/mis-casos/{entity.Id}/detalle"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateModel);
                await _emailService.SendEmail("Caso Registrado", template, user.Email);
            }

            await _userService.Update(user);
            return Ok();
        }

        public IActionResult Index()
            => View();

        [HttpGet("get-casos")]
        public async Task<IActionResult> GetLegalCases()
        {
            var user = await _userService.GetUserByClaim(User);
            var client = await _clientService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var result = await _legalCaseService.GetLegalCasesItemsToClient(parameters, client.Id);
            return PartialView("Partials/Views/LegalCasePartialView", result);
        }

        [HttpGet("{legalCaseId}/detalle")]
        public async Task<IActionResult> Detail(Guid legalCaseId)
        {
            var user = await _userService.GetUserByClaim(User);
            var client = await _clientService.GetByUserId(user.Id);

            if (!await _legalCaseService.ClientHasAccess(client.Id, legalCaseId))
                return BadRequest("No tiene acceso para esta información");

            var legalCase = await _legalCaseService.GetLegalCaseToClient(legalCaseId);
            return View(legalCase);
        }

        [HttpGet("get-caso-detalles")]
        public async Task<IActionResult> GetLegalCaseDetails(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.Get(legalCaseId);
            var queryUbigeo = _districtService.GetAsQueryable();
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var maxlength = Convert.ToInt32(configuration.Value);
            var ubigeo = await queryUbigeo.Where(x => x.ProvinceId == legalCase.ProvinceId)
                .Select(x => new
                {
                    provinceId = x.ProvinceId,
                    province = x.Province.Name,
                    departmentId = x.Province.DepartmentId,
                    department = x.Province.Department.Name
                })
                .FirstOrDefaultAsync();

            var specilityThemes = await _legalCaseService.GetSpecialityThemeByLegalCaseId(legalCase.Id);

            var data = new
            {
                legalCase.Description,
                themes = specilityThemes.Select(y => new
                {
                    id = y.Id,
                    text = y.ColloquialName
                }).ToList(),
                ubigeo,
                specialityName = specilityThemes.Select(y => y.Speciality.ColloquialName).FirstOrDefault(),
                speciality = specilityThemes.Select(y => y.Speciality.Id).FirstOrDefault(),
                DescriptionMaxLength = maxlength
            };

            return Ok(data);
        }

        [HttpPost("editar-caso")]
        public async Task<IActionResult> EditLegalCase(LegalCaseViewModel model)
        {
            var legalCase = await _legalCaseService.Get(model.Id);
            legalCase.Description = model.Description;
            legalCase.ProvinceId = model.ProvinceId.Value;

            if (model.File != null)
            {
                if (model.File.Length > 500000)
                    return BadRequest("El archivo puede pesar como máximo 500kb");

                var extension = Path.GetExtension(model.File.FileName);
                legalCase.UrlFile = await _cloudStorageService.UploadFile(
                    model.File.OpenReadStream(),
                    ConstantHelpers.CLOUD_CONTAINERS.LEGAL_CASE_FILE,
                     $"{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid()}",
                    extension
                    );
            }

            await _legalCaseService.UpdateLegalCaseSpecialityThemes(model.Id, model.SpecialityThemeId);
            await _legalCaseService.Update(legalCase);
            return Ok();
        }

        [HttpGet("get-postulaciones")]
        public async Task<IActionResult> GetApplicantLawyers(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.Get(legalCaseId);
            var result = await _legalCaseService.GetLegalCaseApplicantLawyers(legalCaseId);

            var details = result.Select(x => new LegalCaseLawyerViewModel
            {
                Description = x.Lawyer.AboutMe,
                FreeFirst = x.Lawyer.FreeFirst,
                Fee = x.Lawyer.Fee,
                FullName = $"{x.Lawyer.User.Name} {x.Lawyer.User.Surnames}",
                LawyerId = x.LawyerId,
                ResponseTime = x.ResponseTime,
                Specialities = string.Join(", ", x.Lawyer.LawyerSpecialityThemes.GroupBy(y => y.SpecialityTheme.Speciality.ColloquialName).Select(y => y.Key).ToList()),
                SpecialityThemes = x.Lawyer.LawyerSpecialityThemes.Select(y => y.SpecialityTheme.ColloquialName).ToList(),
                HiredCases = x.Lawyer.HiredCases,
                CreatedAt = x.Lawyer.User.CreatedAt.ToLocalDateTimeFormat(),
                LastConnection = x.Lawyer.User.LastConnection.ToLocalDateTimeFormat(),
                PhotoUrl = x.Lawyer.User.Picture
            }).ToList();

            foreach (var lawyer in details)
                lawyer.Qualification = await _lawyerQualificationService.GetTotalQualification(lawyer.LawyerId);

            var model = new LegalCaseDetailViewModel
            {
                Id = legalCase.Id,
                IsDirected = legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED,
                LegalCaseLawyers = details
            };

            return PartialView("Partials/Views/LegalCaseApplicantLawyersPartialView", model);
        }

        [HttpGet("get-abogados-aceptados")]
        public async Task<IActionResult> GetLegalCaseLawyers(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.Get(legalCaseId);
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_VACANCIES);
            var result = await _legalCaseService.GetLegalCaseLawyers(legalCaseId);
            var details = result.Select(x => new LegalCaseLawyerViewModel
            {
                Description = x.Lawyer.AboutMe,
                FreeFirst = x.Lawyer.FreeFirst,
                Fee = x.Fee,
                FullName = $"{x.Lawyer.User.Name} {x.Lawyer.User.Surnames}",
                LawyerId = x.LawyerId,
                ResponseTime = x.ResponseTime,
                Specialities = string.Join(", ", x.Lawyer.LawyerSpecialityThemes.GroupBy(y => y.SpecialityTheme.Speciality.ColloquialName).Select(y => y.Key).ToList()),
                SpecialityThemes = x.Lawyer.LawyerSpecialityThemes.Select(y => y.SpecialityTheme.ColloquialName).ToList(),
                Status = x.Status,
                HiredCases = x.Lawyer.HiredCases,
                CreatedAt = x.Lawyer.User.CreatedAt.ToLocalDateTimeFormat(),
                LastConnection = x.Lawyer.User.LastConnection.ToLocalDateTimeFormat(),
                PhotoUrl = x.Lawyer.User.Picture
            }).ToList();

            foreach (var lawyer in details)
            {
                var isFirtTime = await _legalCaseService.GetLegalCasesPaymentByClient(lawyer.LawyerId, legalCase.ClientId);
                lawyer.Qualification = await _lawyerQualificationService.GetTotalQualification(lawyer.LawyerId);
                lawyer.IsFree = (lawyer.FreeFirst && isFirtTime == 0) ? true : false;
            }

            var model = new LegalCaseDetailViewModel
            {
                Id = legalCase.Id,
                IsDirected = legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED,
                LegalCaseLawyers = details,
                MaxVacancies = Convert.ToInt32(configuration.Value)
            };
            return PartialView("Partials/Views/LegalCaseLawyersPartialView", model);
        }

        [HttpPost("seleccionar-abogado")]
        public async Task<IActionResult> AcceptLawyer(ApplicantViewModel model)
        {
            var entity = new LegalCaseLawyer
            {
                LawyerId = model.LawyerId,
                LegalCaseId = model.LegalCaseId,
            };

            var result = await _legalCaseService.AcceptApplicant(entity);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("remover-abogado")]
        public async Task<IActionResult> RemoveLawyer(ApplicantViewModel model)
        {
            var result = await _legalCaseService.RemoveLawyer(model.LegalCaseId, model.LawyerId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("procesar-pago")]
        public async Task<IActionResult> ProcessPayment(ApplicantViewModel model)
        {
            var resultId = "";
            if (!model.IsFreeFee)
            {
                var chagueModel = new CreateChargueModel
                {
                    Amount = $"{model.Amount}",
                    Email = model.Email,
                    Source_Id = model.Token
                };

                var result = await _culquiService.CreateChargue(chagueModel);

                if (result.StatusCode != HttpStatusCode.Created)
                    return BadRequest(result.UserMessage);

                resultId = result.Id;
            }

            var totalAmount = model.Amount / 100M;
            var baseAmount = totalAmount / (1 + (ConstantHelpers.IGV_PERCENTAGE / 100M));
            var igvAmount = totalAmount - baseAmount;
            var discountAmount = baseAmount * (ConstantHelpers.PROFIT_PERCENTAGE_PER_LEGALCASE / 100);
            var lawyerAmount = baseAmount - discountAmount;

            try
            {
                var payment = new Payment
                {
                    LawyerId = model.LawyerId,
                    LegalCaseId = model.LegalCaseId,
                    DiscountRate = ConstantHelpers.PROFIT_PERCENTAGE_PER_LEGALCASE,
                    IgvAmount = igvAmount,
                    BaseAmount = baseAmount,
                    DiscountAmount = discountAmount,
                    LawyerAmount = lawyerAmount,
                    TotalAmount = totalAmount,
                    OnlinePaymentId = resultId
                };

                var processResult = await _paymentService.ProcessPayment(payment);
                if (!processResult.Success)
                    return BadRequest(processResult.Message);

                var legalCase = await _legalCaseService.Get(model.LegalCaseId);
                var lawyer = await _lawyerService.Get(model.LawyerId);
                var userLawyer = await _userService.Get(lawyer.UserId);
                var client = await _clientService.Get(legalCase.ClientId);
                var userClient = await _userService.Get(client.UserId);

                var modelEmailToLawyer = new NewLawyerPaymentEmailModel
                {
                    ClientEmail = userClient.Email,
                    ClientFullName = $"{userClient.Name} {userClient.Surnames}",
                    Fee = lawyerAmount,
                    ClientPhoneNumber = userClient.PhoneNumber
                };

                var modelEmailToClient = new ReceiptEmailModel
                {
                    Title = "Constancia de Pago",
                    Message = "Tu pago fue exitoso!",
                    DocumentType = "Boleta de Venta",
                    NumberSerie = payment.NumberSerie,
                    RUC = "20155945860",
                    ClientDNI = userClient.Document,
                    IssueDate = payment.CreatedAt.ToLocalDateFormat(),
                    Currency = "SOLES",
                    TotalAmount = $"{payment.TotalAmount:F}",
                    LawyerEmail = userLawyer.Email,
                    LawyerName = $"{userLawyer.Name} {userLawyer.Surnames}",
                    LawyerPhoneNumber = userLawyer.PhoneNumber,
                    GoodbyeMessage = "Pronto el abogado se pondrá en contacto contigo."
                };

                var templateLawyer = await _emailTemplateService.GetNewLawyerPaymentEmailTemplate(modelEmailToLawyer);
                var templateClient = await _emailTemplateService.GetReceiptEmailTemplate(modelEmailToClient);

                await _emailService.SendEmail("Pago realizado", templateLawyer, userLawyer.Email);
                await _emailService.SendEmail("Constancia de Pago", templateClient, userClient.Email);
                await _hubContext.SendNotification("Pago Realizado", $"/abogado/casos/{model.LegalCaseId}", lawyer.UserId);

                if (legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL)
                {
                    await _hangfireService.ExecuteLEgalCaseDelayedTask(new ExecuteLegalCaseTask
                    {
                        LegalCaseId = legalCase.Id,
                        Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_TIME_SELECT_AND_PAYMENT_LAWYER
                    });
                }
                else
                {
                    await _hangfireService.ExecuteLEgalCaseDelayedTask(new ExecuteLegalCaseTask
                    {
                        LegalCaseId = legalCase.Id,
                        Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_TIME_TO_CLIENT_PAY
                    });
                }

                return Ok(processResult.Message);
            }
            catch (Exception ex)
            {
                var modelTemplateCustom = new StandardEmailModel
                {
                    Title = "Error al guardar en tabla pago",
                    SubHeader = $"Id Abogado : {model.LawyerId} / Id Caso : {model.LegalCaseId}, Error : {ex.Message}"
                };

                var templateCustom = await _emailTemplateService.GetStandardEmailTemplate(modelTemplateCustom);
                await _emailService.SendEmail("Error LC", templateCustom, "soporte@legalconnection.pe");

                return BadRequest("Error al procesar el pago. Contactar con soporte@legalconnection.pe");
            }
        }

        [HttpGet("get-segundos-restantes")]
        public async Task<IActionResult> GetRemainingTimeToSeleect(Guid legalCaseId)
        {
            var result = await _legalCaseService.GetRemainingCustomDateTimeToSelectLawyers(legalCaseId);
            return Ok(result);
        }

        [HttpPost("solicitar-revision")]
        public async Task<IActionResult> RequestReview(Guid legalCaseId)
        {
            var legalCase = await _legalCaseService.Get(legalCaseId);
            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CORRECTED_OBSERVATIONS;

            var observation = await _legalCaseObservationService.GetLastPendingObservationByType(legalCase.Id, ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.VALIDATION);
            observation.HasBeenCorrected = true;

            await _legalCaseService.Update(legalCase);
            await _legalCaseObservationService.Update(observation);

            return Ok();
        }

        [HttpGet("get-contacto-abogado")]
        public async Task<IActionResult> GetLawyerInfo(Guid lawyerId, Guid legalCaseId)
        {
            var validated = await _legalCaseLawyerService.AccessLegalCaseLawyerInfo(lawyerId, legalCaseId, null);

            if (validated.Success)
            {
                var lawyer = await _lawyerService.Get(lawyerId);
                var userLawyer = await _userService.Get(lawyer.UserId);

                var result = new
                {
                    name = $"{userLawyer.Name} {userLawyer.Surnames}",
                    email = userLawyer.Email,
                    phoneNumber = userLawyer.PhoneNumber,
                    dni = userLawyer.Document,
                    houseNumber = userLawyer.HouseNumber,
                };

                return Ok(result);
            }

            return BadRequest(validated.Message);
        }

        [HttpPost("eliminar-caso")]
        public async Task<IActionResult> DeleteLegalCase(Guid legalCaseId)
        {
            var user = await _userService.GetUserByClaim(User);
            var client = await _clientService.GetByUserId(user.Id);
            var result = await _legalCaseService.DeleteLegalCase(client.Id, legalCaseId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("registro-satisfactorio")]
        public IActionResult SuccessfulRegistration()
            => View();
    }
}
