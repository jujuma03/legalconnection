using LC.CORE.Helpers;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.WEB.Models.LawyerProfile;
using LC.CORE.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using LC.ENTITIES.Models;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.WEB.Hubs.Interfaces;

namespace LC.WEB.Controllers
{
    [Authorize]
    [Route("lc/abogado/perfil")]
    public class LawyerProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILawyerLanguageService _lawyerLanguageService;
        private readonly ILawyerService _lawyerService;
        private readonly ITemporalLawyerService _temporalLawyerService;
        private readonly ILawyerExperienceService _lawyerExperienceService;
        private readonly ILawyerStudyService _lawyerStudyService;
        private readonly IHubContext _hubContext;
        private readonly IDistrictService _districtService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly IClientService _clientService;
        private readonly ILawyerPublicationService _lawyerPublicationService;
        private readonly ILawyerObservationService _lawyerObservationService;
        private readonly ILawyerQualificationService _lawyerQualificationService;
        private readonly IPaginationService _paginationService;
        private readonly ILegalCaseLawyerService _legalCaseLawyerService;
        private readonly ILawyerSpecialityThemeService _lawyerSpecialityThemeService;

        public LawyerProfileController(
            IUserService userService,
            ILawyerLanguageService lawyerLanguageService,
            ILawyerService lawyerService,
            ITemporalLawyerService temporalLawyerService,
            ILawyerExperienceService lawyerExperienceService,
            ILawyerStudyService lawyerStudyService,
            IHubContext hubContext,
            IDistrictService districtService,
            IEmailTemplateService emailTemplateService,
            IEmailService emailService,
            IClientService clientService,
            ILawyerPublicationService lawyerPublicationService,
            ILawyerObservationService lawyerObservationService,
            ILawyerQualificationService lawyerQualificationService,
            IPaginationService paginationService,
            ILegalCaseLawyerService legalCaseLawyerService,
            ILawyerSpecialityThemeService lawyerSpecialityThemeService
            )
        {
            _userService = userService;
            _lawyerLanguageService = lawyerLanguageService;
            _lawyerService = lawyerService;
            _temporalLawyerService = temporalLawyerService;
            _lawyerExperienceService = lawyerExperienceService;
            _lawyerStudyService = lawyerStudyService;
            _hubContext = hubContext;
            _districtService = districtService;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _clientService = clientService;
            _lawyerPublicationService = lawyerPublicationService;
            _lawyerObservationService = lawyerObservationService;
            _lawyerQualificationService = lawyerQualificationService;
            _paginationService = paginationService;
            _legalCaseLawyerService = legalCaseLawyerService;
            _lawyerSpecialityThemeService = lawyerSpecialityThemeService;
        }

        [HttpGet("{lawyerId}")]
        [HttpGet("{lawyerId}/{returnUrl}")]
        public async Task<IActionResult> Index(Guid lawyerId, Guid? legalCaseId)
        {
            ViewBag.LawyerId = lawyerId;
            ViewBag.LegalCaseId = legalCaseId;
            var lawyer = await _lawyerService.Get(lawyerId);

            var model = new LawyerInfoViewModel
            {
                RegisterDate = lawyer.CreatedAt.ToLocalDateTimeFormat(),
                Status = lawyer.Status,
                ValidationDate = lawyer.ValidationDate.ToLocalDateTimeFormat()
            };

            return View(model);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetProfile(Guid lawyerId, Guid? legalCaseId)
        {
            var lawyer = await _lawyerService.Get(lawyerId);
            var user = await _userService.Get(lawyer.UserId);
            var hiredCases = await _lawyerService.GetHiredLegalCases(lawyer.Id);
            var totalLawyerExperience = await _lawyerExperienceService.GetTotalExperienceByLawyerId(lawyerId);
            var featuredStudy = await _lawyerStudyService.GetFeaturedStudy(lawyer.Id);

            var lawyerSpecialityThemes = await _lawyerSpecialityThemeService.GetSpecialitiesByLawyer(lawyer.Id);
            var experiences = await _lawyerExperienceService.GetLawyerExperiencesByLawyerId(lawyer.Id);
            var studies = await _lawyerStudyService.GetLawyerStudiesByLawyerId(lawyer.Id);
            var publications = await _lawyerPublicationService.GetLawyerPublications(lawyer.Id, ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.CONFIRMED);
            var languages = await _lawyerLanguageService.GetLanguagesByLawyerId(lawyer.Id);
            var ubigeoQuery = _districtService.GetAsQueryable();
            var ubigeoData = await ubigeoQuery.Where(x => x.Id == user.DistrictId)
                .Select(x => new
                {
                    district = x.Name,
                    districtId = x.Id,
                    province = x.Province.Name,
                    provinceId = x.ProvinceId,
                    department = x.Province.Department.Name,
                    departmentId = x.Province.DepartmentId
                })
                .FirstOrDefaultAsync();

            var qualification = await _lawyerQualificationService.GetTotalQualification(lawyer.Id);
            var qualificationQuantity = await _lawyerQualificationService.QualificationQuantity(lawyer.Id);
            var qualifications = await _lawyerQualificationService.GetLawyerQualificationToProfile(lawyer.Id);

            var information = new LawyerInfoViewModel
            {
                RegisterDate = lawyer.CreatedAt.ToLocalDateTimeFormat(),
                Status = lawyer.Status,
                ValidationDate = lawyer.ValidationDate.ToLocalDateTimeFormat(),
                LegalCaseId = legalCaseId,
            };

            var model = new ProfileViewModel
            {
                LawyerId = lawyer.Id,
                LawyerInformation = information,
                BasicInformation = new BasicInformationViewModel
                {
                    CAL = lawyer.CAL,
                    Fee = lawyer.Fee,
                    HiredCases = hiredCases,
                    Specialities = lawyerSpecialityThemes
                    .GroupBy(x => x.SpecialityTheme.Speciality)
                    .Select(x => new SpecialityViewModel
                    {
                        Id = x.Key.Id,
                        Text = x.Key.OfficialName,
                        Themes = x.Select(y => new SpecialityThemeViewModel
                        {
                            Id = y.SpecialityThemeId,
                            Text = y.SpecialityTheme.OfficialName,
                        }).ToList()

                    }).ToList(),
                    SpecialityThemes = lawyerSpecialityThemes
                    .Select(x => new SpecialityThemeViewModel
                    {
                        Id = x.SpecialityThemeId,
                        Text = x.SpecialityTheme.OfficialName
                    }).ToList(),
                    FreeFirst = lawyer.FreeFirst
                },
                PersonalInformation = new PersonalInformationViewModel
                {
                    BirthDate = user.BirthDate.ToLocalDateFormat(),
                    DNI = user.Document,
                    Department = ubigeoData.department,
                    District = ubigeoData.district,
                    Email = user.Email,
                    HouseNumber = user.HouseNumber,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Province = ubigeoData.province,
                    Sex = user.Sex,
                    Surnames = user.Surnames,
                    TotalExperience = totalLawyerExperience,
                    PhotoUrl = user.Picture,
                    FeaturedStudy = featuredStudy,
                    LastConnection = user.LastConnection.ToLocalDateTimeFormat(),
                    RegistrationDate = user.CreatedAt.ToLocalDateTimeFormat(),
                    Qualification = qualification,
                    QualificationQuantity = qualificationQuantity
                },
                AboutMe = lawyer.AboutMe,
                Experiences = experiences
                    .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.NEW)
                    .Select(x => new ExperienceViewModel
                    {
                        Company = x.Company,
                        Description = x.Description,
                        Position = x.Position,
                        WorkArea = x.WorkArea,
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        PhotoUrl = x.PhotoUrl
                    })
                    .ToList(),
                Languages = languages
                .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.NEW)
                    .Select(x => new LanguageViewModel
                    {
                        Name = x.Language.Name,
                        Level = x.Level
                    })
                    .ToList(),
                Publications = publications
                    .Select(x => new PublicationViewModel
                    {
                        PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                        Description = x.Description,
                        Title = x.Title,
                        Topic = x.Topic,
                        PhotoUrl = x.PhotoUrl
                    })
                    .ToList(),
                Studies = studies
                .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.NEW)
                    .Select(x => new StudyViewModel
                    {
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        Description = x.Description,
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        Grade = ConstantHelpers.ENTITIES.LAWYER_STUDY.GRADE.VALUES[x.Grade],
                        Ubication = x.Ubication,
                        Mention = x.Mention
                    })
                    .ToList(),
                Qualifications = qualifications
                    .Select(x => new QualificationViewModel
                    {
                        Qualification = x.Qualification,
                        Client = $"{x.Client.User.Name} {x.Client.User.Surnames}",
                        ClientPicture = x.Client.User.Picture,
                        Commentary = x.Commentary
                    })
                    .ToList()
            };

            if ((User.IsInRole(ConstantHelpers.ROLES.ADMIN) || User.IsInRole(ConstantHelpers.ROLES.ADVISER)) && lawyer.ProfileWithChanges)
            {
                var temporalLawyer = await _temporalLawyerService.GetTemporalLawyer(lawyer.Id);

                var ubigeoDataTemporal = await ubigeoQuery.Where(x => x.Id == temporalLawyer.DistrictId)
                    .Select(x => new
                    {
                        district = x.Name,
                        districtId = x.Id,
                        province = x.Province.Name,
                        provinceId = x.ProvinceId,
                        department = x.Province.Department.Name,
                        departmentId = x.Province.DepartmentId
                    })
                    .FirstOrDefaultAsync();

                model.BasicInformation.CAL = temporalLawyer.CAL;
                model.BasicInformation.Fee = temporalLawyer.Fee;
                model.BasicInformation.FreeFirst = temporalLawyer.FreeFirst;

                model.PersonalInformation.BirthDate = temporalLawyer.BirthDate.ToLocalDateFormat();
                model.PersonalInformation.DNI = temporalLawyer.Document;
                model.PersonalInformation.Department = ubigeoDataTemporal.department;
                model.PersonalInformation.District = ubigeoDataTemporal.district;
                model.PersonalInformation.HouseNumber = temporalLawyer.HouseNumber;
                model.PersonalInformation.Name = temporalLawyer.Name;
                model.PersonalInformation.PhoneNumber = temporalLawyer.PhoneNumber;
                model.PersonalInformation.Province = ubigeoDataTemporal.province;
                model.PersonalInformation.Sex = temporalLawyer.Sex;
                model.PersonalInformation.Surnames = temporalLawyer.Surnames;
                model.PersonalInformation.PhotoUrl = temporalLawyer.Picture;

                model.AboutMe = temporalLawyer.AboutMe;

                //Experiencies

                model.Experiences = new List<ExperienceViewModel>();
                model.Experiences.AddRange(experiences
                    .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.DELETED && x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED)
                    .Select(x => new ExperienceViewModel
                    {
                        Company = x.Company,
                        Description = x.Description,
                        Position = x.Position,
                        WorkArea = x.WorkArea,
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        PhotoUrl = x.PhotoUrl
                    })
                    .ToList());

                foreach (var item in experiences.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED).ToList())
                {
                    var temp = await _temporalLawyerService.GetTemporalLawyerExperience(item.Id);
                    model.Experiences.Add(new ExperienceViewModel
                    {
                        Company = temp.Company,
                        Description = temp.Description,
                        EndDate = temp.EndDate.ToLocalDateFormat(),
                        PhotoUrl = temp.PhotoUrl,
                        Position = temp.Position,
                        StartDate = temp.StartDate.ToLocalDateFormat(),
                        WorkArea = temp.WorkArea
                    });
                }

                //Studies
                model.Studies = new List<StudyViewModel>();
                model.Studies.AddRange(studies
                    .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.DELETED && x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED)
                    .Select(x => new StudyViewModel
                    {
                        Description = x.Description,
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        Grade = ConstantHelpers.ENTITIES.LAWYER_STUDY.GRADE.VALUES[x.Grade],
                        Mention = x.Mention,
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        Ubication = x.Ubication
                    })
                    .ToList());

                foreach (var item in studies.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED).ToList())
                {
                    var temp = await _temporalLawyerService.GetTemporalLawyerStudy(item.Id);
                    model.Studies.Add(new StudyViewModel
                    {
                        Description = temp.Description,
                        EndDate = temp.EndDate.ToLocalDateFormat(),
                        Grade = ConstantHelpers.ENTITIES.LAWYER_STUDY.GRADE.VALUES[temp.Grade],
                        Mention = temp.Mention,
                        StartDate = temp.StartDate.ToLocalDateFormat(),
                        Ubication = temp.Ubication
                    });
                }

                //Languages
                model.Languages = new List<LanguageViewModel>();
                model.Languages.AddRange(languages
                    .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.DELETED && x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.UPDATED)
                    .Select(x => new LanguageViewModel
                    {
                        Level = x.Level,
                        Name = x.Language.Name
                    })
                    .ToList());

                foreach (var item in languages.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.UPDATED).ToList())
                {
                    var temp = await _temporalLawyerService.GetTemporalLawyerLanguage(item.Id);
                    model.Languages.Add(new LanguageViewModel
                    {
                        Level = temp.Level,
                        Name = temp.Language.Name
                    });
                }
            }

            if (User.IsInRole(ConstantHelpers.ROLES.CLIENT))
            {
                var clientUser = await _userService.GetUserByClaim(User);
                var client = await _clientService.GetByUserId(clientUser.Id);

                model.ViewContact = legalCaseId == null ? true : false;

                var validate = await _legalCaseLawyerService.AccessLegalCaseLawyerInfo(lawyerId, null, client.Id);
                if (validate.Success)
                    model.CanAccessToViewInfo = true;
            }

            return PartialView("Partials/Views/LawyerProfilePartialView", model);

        }

        [HttpGet("get-all-califaciones/{lawyerId}")]
        public async Task<IActionResult> GetAllQualifactions(Guid lawyerId)
        {
            var parameters = _paginationService.GetSentParameters();
            var model = await _lawyerQualificationService.GetLawyerQualifaction(parameters, lawyerId);
            return PartialView("Partials/Views/LawyerQualificationPartialView_v2", model);
        }


        [HttpGet("get-all-publicaciones/{lawyerId}")]
        public async Task<IActionResult> GetPublications(Guid lawyerId)
        {
            var parameters = _paginationService.GetSentParameters();

            var publications = await _lawyerPublicationService.GetLawyerPublications(parameters, lawyerId);

            return PartialView("Partials/Views/LawyerPublicationPartialView_v2", publications);
        }
    }
}
