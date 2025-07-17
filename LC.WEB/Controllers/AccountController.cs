using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Implementations;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Extensions;
using LC.WEB.Models.Account;
using LC.WEB.Models.Regiser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILawyerService _lawyerService;
        private readonly IConfigurationService _configurationService;
        private readonly IClientService _clientService;
        private readonly IEmailService _emailService;
        private readonly ISpecialityService _specialityService;
        private readonly IPlanService _planService;
        private readonly ILawyerSpecialityThemeService _lawyerSpecialityThemeService;

        public AccountController(
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService,
            IEmailTemplateService emailTemplateService,
            ILawyerService lawyerService,
            IConfigurationService configurationService,
            IClientService clientService,
            IEmailService emailService,
            ISpecialityService specialityService,
            IPlanService planService,
            ILawyerSpecialityThemeService lawyerSpecialityThemeService
             )
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailTemplateService = emailTemplateService;
            _lawyerService = lawyerService;
            _configurationService = configurationService;
            _clientService = clientService;
            _emailService = emailService;
            _specialityService=specialityService;
            _planService = planService;
            _lawyerSpecialityThemeService =lawyerSpecialityThemeService;
        }

        #region LOGIN

        [HttpGet("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var error = TempData["Error"] != null ? Convert.ToBoolean(TempData["Error"]) : false;
            var message = TempData["ErrorMessage"];

            if (error)
            {
                ViewBag.Error = message ?? "Intento inválido";
                return View();
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(PortalController.Index), "Portal");
            }

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest("Todos los campos son requeridos");

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userService.FindByNameAsync(model.UserName);
                user.LastConnection = DateTime.UtcNow;

                await _userService.Update(user);

                var roles = await _userService.GetRolesAsync(user);
                if (!string.IsNullOrEmpty(returnUrl))
                    return Ok(returnUrl);
                if (roles.Contains(ConstantHelpers.ROLES.LAWYER))
                {
                    var lawyer = await _lawyerService.GetByUserId(user.Id);
                    if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.REJECTED)
                    {
                        await _signInManager.SignOutAsync();
                        await HttpContext.SignOutAsync();
                        return BadRequest("Cuenta bloqueado.");
                    }

                    return Ok("/abogado/perfil");
                }
                if (roles.Contains(ConstantHelpers.ROLES.CLIENT))
                    return Ok("/mis-casos");
                if (roles.Contains(ConstantHelpers.ROLES.ADMIN) || roles.Contains(ConstantHelpers.ROLES.ADVISER))
                    return Ok("/admin/casos-legales/postulaciones");

                return Ok("/");
            }

            return BadRequest("El correo y/o contraseña son incorrectos.");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(PortalController.Index), "Portal");
        }

        #endregion

        #region REGISTER

        #region LAWYER

        [HttpGet("/registrar-abogado")]
        [AllowAnonymous]
        public async Task<IActionResult> LawyerRegister()
        {
            var configurationMaxSpeciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_SPECIALITY);
            var configurationMaxThemesBySpeciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY);
            var maxSpecialities = Convert.ToInt32(configurationMaxSpeciality.Value);
            var maxThemesBySpeciality = Convert.ToInt32(configurationMaxThemesBySpeciality.Value);

            var spec = await _specialityService.GetAll();
            var model = new LawyerViewModel()
            {
                MaxThemesBySpeciality  = maxThemesBySpeciality,
                MaxSpecialities = maxSpecialities,
                SpecialitiesData= spec.Select(x => new SpecialityViewModel
                {
                    Id= x.Id,
                    Selected = false,
                    Specialty =x.OfficialName
                }).ToList()
            };
            return View(model);
        }

        [HttpPost("/registrar-abogado")]
        [AllowAnonymous]
        public async Task<IActionResult> LawyerRegister(LawyerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.AnyByUsername(model.Email))
                    return BadRequest($"El correo electrónico {model.Email} ya se encuentra registrado.");

                var validate = await _lawyerSpecialityThemeService.ValidateSpecialityTheme(model.SpecialityThemes);

                if (!validate.Success)
                    return BadRequest(validate.Message);

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    DistrictId = model.DistrictId,
                    Name = model.NameOrOffice,
                    Surnames = model.Surnames,
                    PhoneNumber = model.PhoneNumber,
                    Document = model.Dni,
                    RegisterBy = ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC,
                    FullName = $"{model.NameOrOffice.ToUpper()} {(!string.IsNullOrEmpty(model.Surnames) ? model.Surnames.ToUpper() : "")}"
                };

                var result = await _userService.Insert(user, model.Password);

                if (result.Succeeded)
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.LAWYER });
                    await _userService.AddToRole(user, ConstantHelpers.ROLES.LAWYER);

                    var freeLegalCases = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.FREE_CONSULTING);

                    var lawyer = new Lawyer
                    {
                        UserId = user.Id,
                        PublicProfile = true,
                        FreeUser = true,
                        FreeLegalCases = Convert.ToInt32(freeLegalCases.Value)
                    };

                    if (await _planService.ExistFreePlan())
                    {
                        var planFree = await _planService.GetFreePlan();

                        lawyer.LawyerPlanDetail = new LawyerPlanDetail
                        {
                            PlanId = planFree.Id
                        };
                    }

                    await _lawyerService.Insert(lawyer);

                    var themes = model.SpecialityThemes.Select(x => new LawyerSpecialityTheme
                    {
                        SpecialityThemeId = x,
                        LawyerId = lawyer.Id
                    })
                    .ToList();

                    await _lawyerSpecialityThemeService.InsertRange(themes);

                    var code = await _userService.GenerateEmailConfimationToken(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    var emailTemplateModel = new ConfirmationEmailModel
                    {
                        LinkName = "Confirmar Correo",
                        LinkRedirect = callbackUrl,
                        Lawyer = $"{user.Name} {user.Surnames}",
                        Title = $"Bienvenido a {ConstantHelpers.PROJECT}",
                        
                    };

                    var emailTemplate = await _emailTemplateService.GetConfirmationEmailTemplate(emailTemplateModel);
                    await _emailService.SendEmail("Confirmación de Correo Electrónico", emailTemplate, model.Email);

                    var emailTemplateToAsesor = new StandardEmailModel
                    {
                        Title = "¡UN ABOGADO HA SIDO REGISTRADO!",
                        SubHeader = $"Se ha registrado un Abogado, pero aún no ha confirmado su cuenta. Nombre: {user.FullName}, Telefono {user.PhoneNumber}, Correo {user.Email}",
                        LinkName = "",
                        LinkRedirect = $""
                    };
                    var templateToAsesor = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateToAsesor);
                    await _emailService.SendEmail("Abogado Registrado", templateToAsesor, new string[] { ConstantHelpers.EMAIL_ORGANITATION.ADVISER, ConstantHelpers.EMAIL_ORGANITATION.SUPPORTTECHNICAL });

                    return Ok();
                }
            }

            return BadRequest("Por favor verifique sus datos");
        }

        [AllowAnonymous]
        [HttpGet("/abogado/registro-exitoso")]
        public IActionResult LawyerSuccessfulRegistration()
        {
            return View();
        }

        #endregion

        #region CLIENT

        [HttpGet("/registrar")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("/registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> ClientRegister(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.AnyByUsername(model.Email))
                    return BadRequest($"El correo electrónico {model.Email} ya se encuentra registrado.");

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Surnames = model.Surnames,
                    Document = model.Dni,
                    DocumentType = model.DocumentType,
                    RegisterBy = ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC,
                    FullName = $"{model.Name.ToUpper()} {(!string.IsNullOrEmpty(model.Surnames) ? model.Surnames.ToUpper() : "")}"
                };

                var result = await _userService.Insert(user, model.Password);

                if (result.Succeeded)
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.CLIENT });
                    await _userService.AddToRole(user, ConstantHelpers.ROLES.CLIENT);

                    var client = new Client
                    {
                        UserId = user.Id
                    };
                    await _clientService.Insert(client);

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
                    await _emailService.SendEmail("Confirmación de Correo Electrónico", emailTemplate, model.Email);
                    return Ok();
                }
            }

            return BadRequest("Por favor verifique sus datos");
        }

        [AllowAnonymous]
        [HttpGet("/cliente/registro-exitoso")]
        public IActionResult ClientSuccessfulRegistration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("/cliente/registro-caso-exitoso")]
        public IActionResult ClientSuccessfulLegalCaseRegistration()
        {
            return View();
        }

        #endregion
        #endregion

        #region Confirm Email
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            var user = await _userService.Get(userId);

            if (user == null)
                return RedirectToAction("Index", "Error", new { statusCode = 500 });

            //if (user.EmailConfirmed)
            //    return RedirectToAction("Index", "Error", new { statusCode = 423 });

            var result = await _userService.ConfirmEmail(user, code);

            var roles = await _userService.GetRolesAsync(user);

            if (result.Succeeded)
            {
                var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.NEW_LAWYER_MAX_HOUR_TIME_VALIDATION_PROCESS);
                var model = new ConfirmEmailViewModel
                {
                    FullName = $"{user.Name} {user.Surnames}",
                    ProccessValidationMaxHours = Convert.ToInt32(configuration.Value),
                    Role = roles.FirstOrDefault()
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { statusCode = 500 });
            }
        }
        #endregion

        [HttpGet("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                var code = await _userService.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(action: nameof(AccountController.ResetPassword),
                                        controller: "Account",
                                        values: new { user.Id, code },
                                        protocol: Request.Scheme);

                var modelEmail = new StandardEmailModel
                {
                    LinkRedirect = callbackUrl,
                    LinkName = "Reiniciar Contraseña",
                    SubHeader = "Para reinciar su contraseña por favor ingrese al siguiente enlace",
                    Title = "REINICAR CONTRASEÑA"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                await _emailService.SendEmail("Reiniciar Contraseña", template, user.UserName);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet("ForgotPasswordConfirmation")]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet("ResetPassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }

            var message = "";
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }

            var model = new ResetPasswordViewModel { Code = code, Message = message };
            return View(model);
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userService.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                TempData["message"] = "Ingresar un email válido";
                return RedirectToAction("ResetPassword", new { code = model.Code });
            }
            var result = await _userService.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            else
            {
                TempData["message"] = "Token inválido";
                return RedirectToAction("ResetPassword", new { code = model.Code });
            }
        }

        [HttpGet("ResetPasswordConfirmation")]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            string redirectUrl = Url.Action(nameof(ExternalResponse), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public IActionResult ExternalRegister(string provider, string returnUrl = null)
        {
            string redirectUrl = Url.Action(nameof(ExternalRegisterResponse), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        //[HttpPost("/respuesta-login-externo")]
        public async Task<IActionResult> ExternalResponse(string returnurl = "/")
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login), new { returnUrl = returnurl });

            var olduser = await _userService.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
            if (olduser != null /*&& olduser.RegisterBy != ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC*/)
            {
                //if (olduser.RegisterBy == ConstantHelpers.ENTITIES.USER.REGISTER_BY.GOOGLE)
                //{
                    await _signInManager.SignInAsync(olduser, false);
                    return RedirectToLocal(returnurl);
                //}
            }
            TempData["Error"] = true;
            return RedirectToAction(nameof(Login), new { returnUrl = returnurl });
        }

        [AllowAnonymous]
        //[HttpPost("/respuesta-login-externo")]
        public async Task<IActionResult> ExternalRegisterResponse(string returnurl = "/")
        {
            var userResponse = await _signInManager.GetExternalLoginInfoAsync();

            if (userResponse == null)
                return RedirectToAction(nameof(Login), new { returnUrl = returnurl });

            var name = userResponse.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = userResponse.Principal.FindFirstValue(ClaimTypes.Surname);
            var email = userResponse.Principal.FindFirstValue(ClaimTypes.Email);

            var userSystem = await _userService.FindByEmailAsync(email);

            if (userSystem is null)
            {
                if (email != null)
                {
                    if (await _userService.AnyByUsername(email))
                    {
                        TempData["Error"] = true;
                        TempData["ErrorMessage"] = "El Correo Electrónico ya ha se encuentra registrado.";
                        return RedirectToAction(nameof(Register), new { returnUrl = returnurl });
                    }

                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        Name = name,
                        EmailConfirmed = true,
                        Surnames = string.IsNullOrEmpty(surname)? "": surname,
                        RegisterBy = ConstantHelpers.ENTITIES.USER.REGISTER_BY.GOOGLE,
                        FullName = $"{name.ToUpper()} {(!string.IsNullOrEmpty(surname) ? surname.ToUpper() : "")}"
                    };

                    var result = await _userService.Insert(user, Guid.NewGuid().ToString());

                    if (result.Succeeded)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole { Name = ConstantHelpers.ROLES.CLIENT });
                        await _userService.AddToRole(user, ConstantHelpers.ROLES.CLIENT);

                        var client = new Client
                        {
                            UserId = user.Id
                        };
                        await _clientService.Insert(client);

                        var standardTemplateModel = new StandardEmailModel
                        {
                            LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                            LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/login",
                            SubHeader = "Gracias por registrarte.",
                            Title = $"Bienvenido a {ConstantHelpers.PROJECT.ToUpper()}",
                        };

                        var emailTemplate = await _emailTemplateService.GetStandardEmailTemplate(standardTemplateModel);
                        await _emailService.SendEmail($"Bienvenido a {ConstantHelpers.PROJECT}", emailTemplate, email);
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction(nameof(ClientSuccessfulRegistration));
                        //return RedirectToLocal(returnurl);
                    }

                    TempData["Error"] = true;
                    return RedirectToAction(nameof(Register), new { returnUrl = returnurl });
                }

                TempData["Error"] = true;
                return RedirectToAction(nameof(Register), new { returnUrl = returnurl });
            }
            else
            {
                if (userSystem.RegisterBy == ConstantHelpers.ENTITIES.USER.REGISTER_BY.GOOGLE)
                {
                    await _signInManager.SignInAsync(userSystem, false);
                    return RedirectToLocal(returnurl);
                }
                else
                {
                    TempData["Error"] = true;
                    return RedirectToAction(nameof(Register), new { returnUrl = returnurl });
                }
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(PortalController.Index), "Home");
            }
        }
    }
}
