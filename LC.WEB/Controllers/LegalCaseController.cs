using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Extensions;
using LC.WEB.Models.LegalCase;
using LC.WEB.Services.Google.Interfaces;
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
    [AllowAnonymous]
    [Route("casos-legales")]
    public class LegalCaseController : Controller
    {
        private readonly ISpecialityService _specialityService;
        private readonly IConfigurationService _configurationService;
        private readonly IGoogleService _googleService;
        private readonly IUserService _userService;
        private readonly IProvinceService _provinceService;
        private readonly ILegalCaseService _legalCaseService;
        private readonly ILawyerService _lawyerService;
        private readonly IClientService _clientService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ISpecialityThemeService _specialityThemeService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly ILawyerSpecialityThemeService _lawyerSpecialityThemeService;

        public LegalCaseController(
            ISpecialityService specialityService,
            IConfigurationService configurationService,
            IGoogleService googleService,
            IUserService userService,
            IProvinceService provinceService,
            ILegalCaseService legalCaseService,
            ILawyerService lawyerService,
            SignInManager<ApplicationUser> signInManager,
            IClientService clientService,
            ICloudStorageService cloudStorageService,
            RoleManager<ApplicationRole> roleManager,
            ISpecialityThemeService specialityThemeService,
            IEmailTemplateService emailTemplateService,
            IEmailService emailService,
            ILawyerSpecialityThemeService lawyerSpecialityThemeService
            )
        {
            _specialityService = specialityService;
            _configurationService = configurationService;
            _googleService = googleService;
            _userService = userService;
            _provinceService = provinceService;
            _legalCaseService = legalCaseService;
            _lawyerService = lawyerService;
            _clientService = clientService;
            _cloudStorageService = cloudStorageService;
            _roleManager = roleManager;
            _specialityThemeService = specialityThemeService;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _lawyerSpecialityThemeService = lawyerSpecialityThemeService;
            _signInManager = signInManager;
        }

        [HttpGet("agregar")]
        public async Task<IActionResult> Add()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Add", "LegalCase", new { area = "Client" });

            var specialities = await _specialityService.GetAll();
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var maxlength = Convert.ToInt32(configuration.Value);

            var model = new LegalCaseViewModel
            {
                Specialities = specialities.Select(x => new SpecialityViewModel
                {
                    Id = x.Id,
                    Name = x.ColloquialName
                }).ToList(),
                DescriptionMaxLength = maxlength,
            };

            return View(model);
        }

        [HttpGet("agregar-dirigido/abogado/{lawyerId}")]
        public async Task<IActionResult> AddDirected(Guid lawyerId)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("AddDirected", "LegalCase", new { area = "Client" , lawyerId = lawyerId});

            var lawyer = await _lawyerService.Get(lawyerId);
            var lawyerUser = await _userService.Get(lawyer.UserId);
            var lawyerSpecialityThemes = await _lawyerSpecialityThemeService.GetSpecialitiesByLawyer(lawyer.Id);
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var maxlength = Convert.ToInt32(configuration.Value);

            var model = new LegalCaseViewModel
            {
                LawyerFullName = $"{lawyerUser.Surnames} {lawyerUser.Name}",
                LawyerId = lawyer.Id,
                SpecialityThemes = lawyerSpecialityThemes
                .Select(x => new SpecialityThemeViewModel
                {
                    Id = x.SpecialityTheme.Id,
                    Name = x.SpecialityTheme.ColloquialName
                }).ToList(),
                DescriptionMaxLength = maxlength
            };

            return View(model);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AddLegalCase(LegalCaseViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Client.GoogleTokenId))
            {
                var userGoogle = await _googleService.GetUserByGoogleTokenId(model.Client.GoogleTokenId);

                if (userGoogle is null)
                    return BadRequest("Error al obtener las credenciales de la cuenta google. Por favor volver a intentarlo.");

                model.Client.Name = userGoogle.FirstName;
                model.Client.Surnames = userGoogle.LastName;
                model.Client.Email = userGoogle.Email;
                model.Client.Password = Guid.NewGuid().ToString();
            }

            if (model.SpecialityThemeId == null || !model.SpecialityThemeId.Any())
                return BadRequest("Es necesario seleccionar, por lo menos, un asunto.");

            if (model.File != null)
            {
                if (model.File.Length > 500000)
                    return BadRequest("El archivo insertado en el segundo paso puede pesar como máximo 500kb.");
            }

            if (await _userService.AnyByUsername(model.Client.Email))
                return BadRequest($"El correo electrónico {model.Client.Email} ya se encuentra registrado.");

            var user = new ApplicationUser
            {
                UserName = model.Client.Email,
                Email = model.Client.Email,
                Name = model.Client.Name,
                Surnames = model.Client.Surnames,
                EmailConfirmed = !string.IsNullOrEmpty(model.Client.GoogleTokenId),
                PhoneNumber = model.PhoneNumber,
                RegisterBy = string.IsNullOrEmpty(model.Client.GoogleTokenId) ? ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC : ConstantHelpers.ENTITIES.USER.REGISTER_BY.GOOGLE,
                FullName = $"{model.Client.Name.ToUpper()} { (!string.IsNullOrEmpty(model.Client.Surnames) ? model.Client.Surnames.ToUpper() : "")}"
            };

            var result = await _userService.Insert(user, model.Client.Password);

            var client = new Client();

            if (result.Succeeded)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.CLIENT });
                await _userService.AddToRole(user, ConstantHelpers.ROLES.CLIENT);

                client = new Client
                {
                    UserId = user.Id
                };

                await _clientService.Insert(client);

                if (string.IsNullOrEmpty(model.Client.GoogleTokenId))
                {
                    var code = await _userService.GenerateEmailConfimationToken(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    var standardTemplateModel = new StandardEmailModel
                    {
                        LinkName = "Confirmar Correo",
                        LinkRedirect = callbackUrl,
                        SubHeader = "Es necesario confimar el correo electrónico para continuar con el proceso.",
                        Title = "Bienvenido a LEGAL CONNECTION",
                    };

                    var emailTemplate = await _emailTemplateService.GetStandardEmailTemplate(standardTemplateModel);
                    await _emailService.SendEmail("Confirmación de Correo Electrónico", emailTemplate, model.Client.Email);
                }
                else
                {
                    var standardTemplateModel = new StandardEmailModel
                    {
                        LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                        LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/login",
                        SubHeader = "Gracias por registrarte.",
                        Title = $"Bienvenido a {ConstantHelpers.PROJECT.ToUpper()}",
                    };

                    var emailTemplate = await _emailTemplateService.GetStandardEmailTemplate(standardTemplateModel);
                    await _emailService.SendEmail($"Bienvenido a {ConstantHelpers.PROJECT}", emailTemplate, user.Email);
                }
            }
            else
            {
                return BadRequest("Error al registrar el cliente.");
            }

            var workHourStartConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_START);
            var workHourEndConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_END);

            var workHourStart = ConvertHelpers.TimepickerToTimeSpan(workHourStartConfi.Value);
            var workEndStart = ConvertHelpers.TimepickerToTimeSpan(workHourEndConfi.Value);

            var count = await _legalCaseService.GetLegalCaseCountByType(ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED);
            var province = await _provinceService.Get(model.ProvinceId.Value);
            var firstSpecialityTheme = await _specialityThemeService.Get(model.SpecialityThemeId.FirstOrDefault());
            var speciality = await _specialityService.Get(firstSpecialityTheme.SpecialityId);

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


            var emailTemplateToAsesor = new StandardEmailModel
            {
                Title = "¡UN CASO HA SIDO REGISTRADO!",
                SubHeader = "Se ha registrado un caso de postulación",
                LinkName = "Ir al caso",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/admin/casos-legales/{entity.Id}/detalles"
            };
            var templateToAsesor = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateToAsesor);
            await _emailService.SendEmail("Caso Registrado", templateToAsesor, new string[] { ConstantHelpers.EMAIL_ORGANITATION.ADVISER, ConstantHelpers.EMAIL_ORGANITATION.SUPPORTTECHNICAL});


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

            var resultText = string.Empty;

            if (string.IsNullOrEmpty(model.Client.GoogleTokenId))
            {
                resultText = "Para continuar con el proceso es necesario confirmar tu cuenta mediante el enlace enviado a su correo. Pronto un asesor se contactará con usted para darle detalles de su caso.";
            }
            else
            {
                resultText = "Gracias por su registro ahora podrá seguir paso a paso el avance de su caso a través de su cuenta. Un asesor pronto se comunicará con usted.";
                await _signInManager.SignInAsync(user, false);
            }

            return Ok(resultText);
        }

        [HttpPost("agregar-digirido")]
        public async Task<IActionResult> AddDirectedLegalCase(LegalCaseViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Client.GoogleTokenId))
            {
                var userGoogle = await _googleService.GetUserByGoogleTokenId(model.Client.GoogleTokenId);

                if (userGoogle is null)
                    return BadRequest("Error al obtener las credenciales de la cuenta google. Por favor volver a intentarlo.");

                model.Client.Name = userGoogle.FirstName;
                model.Client.Surnames = userGoogle.LastName;
                model.Client.Email = userGoogle.Email;
                model.Client.Password = Guid.NewGuid().ToString();
            }

            if (model.SpecialityThemeId == null || !model.SpecialityThemeId.Any())
                return BadRequest("Es necesario seleccionar, por lo menos, un asunto.");

            if (model.File != null)
            {
                if (model.File.Length > 500000)
                    return BadRequest("El archivo insertado en el segundo paso puede pesar como máximo 500kb.");
            }

            if (await _userService.AnyByUsername(model.Client.Email))
                return BadRequest($"El correo electrónico {model.Client.Email} ya se encuentra registrado.");

            var user = new ApplicationUser
            {
                UserName = model.Client.Email,
                Email = model.Client.Email,
                Name = model.Client.Name,
                Surnames = model.Client.Surnames,
                EmailConfirmed = !string.IsNullOrEmpty(model.Client.GoogleTokenId),
                PhoneNumber = model.PhoneNumber,
                RegisterBy = string.IsNullOrEmpty(model.Client.GoogleTokenId) ? ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC : ConstantHelpers.ENTITIES.USER.REGISTER_BY.GOOGLE,
                FullName = $"{model.Client.Name.ToUpper()} {(!string.IsNullOrEmpty(model.Client.Surnames) ? model.Client.Surnames.ToUpper() : "")}"
            };

            var result = await _userService.Insert(user, model.Client.Password);

            var client = new Client();

            if (result.Succeeded)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.CLIENT });
                await _userService.AddToRole(user, ConstantHelpers.ROLES.CLIENT);

                client = new Client
                {
                    UserId = user.Id
                };

                await _clientService.Insert(client);

                if (string.IsNullOrEmpty(model.Client.GoogleTokenId))
                {
                    var code = await _userService.GenerateEmailConfimationToken(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    var standardTemplateModel = new StandardEmailModel
                    {
                        LinkName = "Confirmar Correo",
                        LinkRedirect = callbackUrl,
                        SubHeader = "Es necesario confimar el correo electrónico para continuar con el proceso.",
                        Title = "Bienvenido a LEGAL CONNECTION",
                    };

                    var emailTemplate = await _emailTemplateService.GetStandardEmailTemplate(standardTemplateModel);
                    await _emailService.SendEmail("Confirmación de Correo Electrónico", emailTemplate, model.Client.Email);
                }
                else
                {
                    var standardTemplateModel = new StandardEmailModel
                    {
                        LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                        LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/login",
                        SubHeader = "Gracias por registrarte.",
                        Title = $"Bienvenido a {ConstantHelpers.PROJECT.ToUpper()}",
                    };

                    var emailTemplate = await _emailTemplateService.GetStandardEmailTemplate(standardTemplateModel);
                    await _emailService.SendEmail($"Bienvenido a {ConstantHelpers.PROJECT}", emailTemplate, user.Email);
                }
            }
            else
            {
                return BadRequest("Error al registrar el cliente.");
            }

            var workHourStartConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_START);
            var workHourEndConfi = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_END);

            var workHourStart = ConvertHelpers.TimepickerToTimeSpan(workHourStartConfi.Value);
            var workEndStart = ConvertHelpers.TimepickerToTimeSpan(workHourEndConfi.Value);

            var count = await _legalCaseService.GetLegalCaseCountByType(ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED);
            var province = await _provinceService.Get(model.ProvinceId.Value);
            var firstSpecialityTheme = await _specialityThemeService.Get(model.SpecialityThemeId.FirstOrDefault());
            var speciality = await _specialityService.Get(firstSpecialityTheme.SpecialityId);

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


            var emailTemplateToAsesor = new StandardEmailModel
            {
                Title = "¡UN CASO HA SIDO REGISTRADO!",
                SubHeader = "Se ha registrado un caso de contacto directo",
                LinkName = "Ir al caso",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/admin/casos-legales/{entity.Id}/detalles"
            };
            var templateToAsesor = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateToAsesor);
            await _emailService.SendEmail("Caso Registrado", templateToAsesor, new string[] { ConstantHelpers.EMAIL_ORGANITATION.ADVISER, ConstantHelpers.EMAIL_ORGANITATION.SUPPORTTECHNICAL });


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

            var resultText = string.Empty;

            if (string.IsNullOrEmpty(model.Client.GoogleTokenId))
            {
                resultText = "Para continuar con el proceso es necesario confirmar tu cuenta mediante el enlace enviado a su correo. Pronto un asesor se contactará con usted para darle detalles de su caso.";
            }
            else
            {
                resultText = "Gracias por su registro ahora podrá seguir paso a paso el avance de su caso a través de su cuenta. Un asesor pronto se comunicará con usted.";
                await _signInManager.SignInAsync(user, false);
            }

            return Ok(resultText);
        }
    }
}
