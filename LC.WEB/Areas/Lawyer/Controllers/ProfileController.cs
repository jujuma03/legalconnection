using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Lawyer.Models.Profile;
using LC.WEB.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.LAWYER)]
    [Area("Lawyer")]
    [Route("abogado/perfil")]
    public class ProfileController : LawyerBaseController
    {
        private readonly IUserService _userService;
        private readonly ILawyerService _lawyerService;
        private readonly ILawyerSpecialityThemeService _lawyerSpecialityThemeService;
        private readonly IDistrictService _districtService;
        private readonly ILawyerExperienceService _lawyerExperienceService;
        private readonly IConfigurationService _configurationService;
        private readonly ILawyerStudyService _lawyerStudyService;
        private readonly IPaginationService _paginationService;
        private readonly ILawyerObservationService _lawyerObservationService;
        private readonly ILawyerPublicationService _lawyerPublicationService;
        private readonly ILawyerLanguageService _lawyerLanguageService;
        private readonly ITemporalLawyerService _temporalLawyerService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly ILawyerQualificationService _lawyerQualificationService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILawyerInterviewService _lawyerInterviewService;

        public ProfileController(
            IUserService userService,
            ILawyerService lawyerService,
            ILawyerSpecialityThemeService lawyerSpecialityThemeService,
            IDistrictService districtService,
            ILawyerExperienceService lawyerExperienceService,
            IConfigurationService configurationService,
            ILawyerStudyService lawyerStudyService,
            IPaginationService paginationService,
            ILawyerObservationService lawyerObservationService,
            ILawyerPublicationService lawyerPublicationService,
            ILawyerLanguageService lawyerLanguageService,
            ITemporalLawyerService temporalLawyerService,
            ICloudStorageService cloudStorageService,
            ILawyerQualificationService lawyerQualificationService,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            ILawyerInterviewService lawyerInterviewService
            )
        {
            _userService = userService;
            _lawyerService = lawyerService;
            _lawyerSpecialityThemeService = lawyerSpecialityThemeService;
            _districtService = districtService;
            _lawyerExperienceService = lawyerExperienceService;
            _configurationService = configurationService;
            _lawyerStudyService = lawyerStudyService;
            _paginationService = paginationService;
            _lawyerObservationService = lawyerObservationService;
            _lawyerPublicationService = lawyerPublicationService;
            _lawyerLanguageService = lawyerLanguageService;
            _temporalLawyerService = temporalLawyerService;
            _cloudStorageService = cloudStorageService;
            _lawyerQualificationService = lawyerQualificationService;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _lawyerInterviewService = lawyerInterviewService;
        }

        public async Task<IActionResult> Index()
        {
            var maxSpecialityConfiguration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_SPECIALITY);
            var maxSpecialityThemesBySpeciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY);

            var maxSpeciality = Convert.ToInt32(maxSpecialityConfiguration.Value);
            var maxThemeBySpeciality = Convert.ToInt32(maxSpecialityThemesBySpeciality.Value);
            var maxThemes = maxThemeBySpeciality * maxSpeciality;

            ViewBag.MaxSpeciality = maxSpeciality;
            ViewBag.MaxThemes = maxThemes;

            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var lawyerSpecialityThemes = await _lawyerSpecialityThemeService.GetSpecialitiesByLawyer(lawyer.Id);
            var featuredStudy = await _lawyerStudyService.GetFeaturedStudy(lawyer.Id);
            var hiredCases = await _lawyerService.GetHiredLegalCases(lawyer.Id);
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

            var model = new ProfileViewModel
            {
                Status = lawyer.Status,
                LawyerId = lawyer.Id,
                FreeUser = lawyer.FreeUser,
                PublicProfile = lawyer.PublicProfile,
                PersonalInformation = new PersonalInformationViewModel
                {
                    LawyerId = lawyer.Id,
                    BirthDate = user.BirthDate.ToLocalDateFormat(),
                    DNI = user.Document,
                    Department = ubigeoData.department,
                    DepartmentId = ubigeoData.departmentId,
                    District = ubigeoData.district,
                    DistrictId = ubigeoData.districtId,
                    Email = user.Email,
                    HouseNumber = user.HouseNumber,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Province = ubigeoData.province,
                    ProvinceId = ubigeoData.provinceId,
                    Sex = user.Sex,
                    Surnames = user.Surnames,
                    PhotoUrl = user.Picture,
                    FeaturedStudy = featuredStudy,
                    LastConnection = user.LastConnection.ToLocalDateTimeFormat(),
                    RegistrationDate = user.CreatedAt.ToLocalDateTimeFormat(),
                    Qualification = qualification,
                    QualificationQuantity = qualificationQuantity
                },
                BasicInformation = new BasicInformationViewModel
                {
                    FreeLegalCases = lawyer.FreeLegalCases,
                    LawyerId = lawyer.Id,
                    CAL = lawyer.CAL,
                    Fee = lawyer.Fee,
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
                        Text = x.SpecialityTheme.OfficialName,
                        Id = x.SpecialityTheme.Id
                    }).ToList(),
                    HiredCases = hiredCases,
                    FreeFirst = lawyer.FreeFirst
                },
                AboutMe = lawyer.AboutMe,
            };


            if (lawyer.ProfileWithChanges)
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
            }


            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.IN_EVALUATION)
            {
                var lawyerObservations = await _lawyerObservationService.GetLastObservationByType(lawyer.Id, ConstantHelpers.ENTITIES.LAWYER_OBSERVATION.PROCESS.VALIDATION_PROFILE);
                if (lawyerObservations != null)
                {
                    model.LawyerObservation = new LawyerObservationViewModel
                    {
                        Observation = lawyerObservations.Observation,
                        HasBeenCorrected = lawyerObservations.HasBeenCorrected
                    };
                }
            }
            else if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.INTERVIEW_VALIDATED)
            {
                var interviews = await _lawyerInterviewService.GetInterviewsByLawyer(lawyer.Id);
                model.LawyerInterviews = interviews
                    .Select(x => new LawyerInterviewViewModel
                    {
                        Id = x.Id,
                        Date = x.Date.ToLocalDateFormat(),
                        EndRange = x.EndRange.ToLocalDateTimeFormatUtc(),
                        StartRange = x.StartRange.ToLocalDateTimeFormatUtc(),
                        Selected = x.Selected
                    })
                    .ToList();
            }

            return View(model);
        }

        #region -- Información Personal --

        [HttpGet("get-informacion")]
        public async Task<IActionResult> GetLawyerInformation()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
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

            var model = new ProfileViewModel
            {
                BasicInformation = new BasicInformationViewModel
                {
                    LawyerId = lawyer.Id,
                },
                PersonalInformation = new PersonalInformationViewModel
                {
                    Department = ubigeoData.department,
                    DepartmentId = ubigeoData.departmentId,
                    District = ubigeoData.district,
                    DistrictId = ubigeoData.districtId,
                    Email = user.Email,
                    Name = user.Name,
                    Surnames = user.Surnames,
                    Province = ubigeoData.province,
                    ProvinceId = ubigeoData.provinceId,
                    PhotoUrl = user.Picture,
                }
            };

            return Ok(model);
        }

        [HttpPost("actualizar-informacion")]
        public async Task<IActionResult> UpdateLawyerInformation(ProfileViewModel model)
        {
            var lawyer = await _lawyerService.Get(model.LawyerId);
            var user = await _userService.Get(lawyer.UserId);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                var temporalLawyer = await _temporalLawyerService.GetTemporalLawyer(lawyer.Id);
                temporalLawyer.DistrictId = model.PersonalInformation.DistrictId;
                temporalLawyer.Name = model.PersonalInformation.Name;
                temporalLawyer.CAL = model.BasicInformation.CAL;
                temporalLawyer.Surnames = model.PersonalInformation.Surnames;
                lawyer.ProfileWithChanges = true;

                if (model.PersonalInformation.Photo != null)
                {
                    if (!string.IsNullOrEmpty(user.Picture))
                    {
                        await _cloudStorageService.TryDelete(user.Picture, ConstantHelpers.CLOUD_CONTAINERS.PROFILE);
                    }

                    if (!string.IsNullOrEmpty(model.PersonalInformation.urlLawyerPhotoCropImg))
                    {
                        var imgArray1 = model.PersonalInformation.urlLawyerPhotoCropImg.Split(";");
                        var imgArray2 = imgArray1[1].Split(",");

                        var newImage = Convert.FromBase64String(imgArray2[1]);

                        using (var stream = new MemoryStream(newImage))
                        {
                            var extension = Path.GetExtension(model.PersonalInformation.Photo.FileName);
                            temporalLawyer.Picture = await _cloudStorageService.UploadFile(
                                stream,
                                ConstantHelpers.CLOUD_CONTAINERS.PROFILE,
                                 $"{Guid.NewGuid()}",
                                extension
                                );
                        }
                    }
                }

                await _temporalLawyerService.SaveTemporalLawyer(temporalLawyer);
            }
            else
            {
                user.DistrictId = model.PersonalInformation.DistrictId;
                user.Name = model.PersonalInformation.Name;
                user.Surnames = model.PersonalInformation.Surnames;
                lawyer.CAL = model.BasicInformation.CAL;

                if (model.PersonalInformation.Photo != null)
                {
                    if (!string.IsNullOrEmpty(user.Picture))
                    {
                        await _cloudStorageService.TryDelete(user.Picture, ConstantHelpers.CLOUD_CONTAINERS.PROFILE);
                    }

                    if (!string.IsNullOrEmpty(model.PersonalInformation.urlLawyerPhotoCropImg))
                    {
                        var imgArray1 = model.PersonalInformation.urlLawyerPhotoCropImg.Split(";");
                        var imgArray2 = imgArray1[1].Split(",");

                        var newImage = Convert.FromBase64String(imgArray2[1]);
                        using (var stream = new MemoryStream(newImage))
                        {
                            var extension = Path.GetExtension(model.PersonalInformation.Photo.FileName);
                            user.Picture = await _cloudStorageService.UploadFile(
                                stream,
                                ConstantHelpers.CLOUD_CONTAINERS.PROFILE,
                                 $"{Guid.NewGuid()}",
                                extension
                                );
                        }
                    }
                    var identity = User.Identity as ClaimsIdentity;
                    var existingClaim = identity.FindFirst("PictureUrl");

                    if (existingClaim != null)
                        identity.RemoveClaim(existingClaim);

                    identity.AddClaim(new Claim("PictureUrl", user.Picture));
                }

                await _userService.Update(user);
            }

            return Ok(user.Picture);
        }

        [HttpGet("get-informacion-personal")]
        public async Task<IActionResult> GetPersonalInformation()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var model = new ProfileViewModel
            {
                LawyerId = lawyer.Id,
                PersonalInformation = new PersonalInformationViewModel
                {
                    LawyerId = lawyer.Id,
                    BirthDate = user.BirthDate == DateTime.MinValue ? DateTime.UtcNow.AddYears(20).ToLocalDateFormat() : user.BirthDate.ToLocalDateFormat(),
                    DNI = user.Document,
                    HouseNumber = user.HouseNumber,
                    PhoneNumber = user.PhoneNumber,
                    Sex = user.Sex,
                },
                BasicInformation = new BasicInformationViewModel
                {
                    CAL = lawyer.CAL,
                }
            };

            return Ok(model);
        }

        [HttpPost("actualizar-informacion-personal")]
        public async Task<IActionResult> UpdatePersonalInformation(ProfileViewModel model)
        {
            var lawyer = await _lawyerService.Get(model.LawyerId);
            var user = await _userService.Get(lawyer.UserId);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                var temporalLawyer = await _temporalLawyerService.GetTemporalLawyer(lawyer.Id);
                temporalLawyer.BirthDate = ConvertHelpers.DatepickerToUtcDateTime(model.PersonalInformation.BirthDate);
                temporalLawyer.Document = model.PersonalInformation.DNI;
                temporalLawyer.HouseNumber = model.PersonalInformation.HouseNumber;
                temporalLawyer.PhoneNumber = model.PersonalInformation.PhoneNumber;
                temporalLawyer.Sex = model.PersonalInformation.Sex;

                await _temporalLawyerService.SaveTemporalLawyer(temporalLawyer);
            }
            else
            {
                user.BirthDate = ConvertHelpers.DatepickerToUtcDateTime(model.PersonalInformation.BirthDate);
                user.Document = model.PersonalInformation.DNI;
                user.HouseNumber = model.PersonalInformation.HouseNumber;
                user.PhoneNumber = model.PersonalInformation.PhoneNumber;
                user.Sex = model.PersonalInformation.Sex;

                await _lawyerService.Update(lawyer);
                await _userService.Update(user);
            }

            return Ok();
        }

        [HttpGet("get-especialidades")]
        public async Task<IActionResult> GetSpecialityThemes()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var data = await _lawyerSpecialityThemeService.GetSpecialitiesByLawyer(lawyer.Id);

            var specialities = data.GroupBy(x => x.SpecialityTheme.Speciality)
                .Select(x => new
                {
                    id = x.Key.Id,
                    text = x.Key.OfficialName
                })
                .ToArray();

            var specialityThemes = data.
                Select(x => new
                {
                    id = x.SpecialityThemeId,
                    text = x.SpecialityTheme.OfficialName
                })
                .ToArray();

            var result = new
            {
                specialities,
                specialityThemes
            };

            return Ok(result);
        }

        [HttpPost("actualizar-especialidades")]
        public async Task<IActionResult> UpdateSpecialityThemes(BasicInformationViewModel model)
        {
            var valid = await _lawyerSpecialityThemeService.ValidateSpecialityTheme(model.SpecialityThemesId);

            if (!valid.Success)
                return BadRequest(valid.Message);

            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var specialities = await _lawyerSpecialityThemeService.GetSpecialitiesByLawyer(lawyer.Id);
            await _lawyerSpecialityThemeService.DeleteRange(specialities.Select(x => x));
            var lawyerSpecialityThemes = model.SpecialityThemesId
                .Select(x => new LawyerSpecialityTheme
                {
                    SpecialityThemeId = x,
                    LawyerId = lawyer.Id
                })
                .ToList();
            await _lawyerSpecialityThemeService.InsertRange(lawyerSpecialityThemes);
            return Ok();
        }

        [HttpGet("get-valor-consulta")]
        public async Task<IActionResult> GetFee()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var model = new BasicInformationViewModel
            {
                Fee = lawyer.Fee,
                FreeFirst = lawyer.FreeFirst
            };
            return Ok(model);
        }

        [HttpPost("actualizar-valor-consulta")]
        public async Task<IActionResult> UpdateFee(BasicInformationViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                var temporalLawyer = await _temporalLawyerService.GetTemporalLawyer(lawyer.Id);
                temporalLawyer.Fee = model.Fee;
                temporalLawyer.FreeFirst = model.FreeFirst;

                await _temporalLawyerService.SaveTemporalLawyer(temporalLawyer);
            }
            else
            {
                lawyer.Fee = model.Fee;
                lawyer.FreeFirst = model.FreeFirst;
                await _lawyerService.Update(lawyer);
            }

            return Ok();
        }

        [HttpGet("get-sobremi")]
        public async Task<IActionResult> GetAboutMe()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            return Ok(lawyer.AboutMe);
        }

        [HttpPost("actualizar-sobremi")]
        public async Task<IActionResult> UpdateAboutMe(ProfileViewModel model)
        {
            var lawyer = await _lawyerService.Get(model.LawyerId);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                var temporalLawyer = await _temporalLawyerService.GetTemporalLawyer(lawyer.Id);
                temporalLawyer.AboutMe = model.AboutMe;
                await _temporalLawyerService.SaveTemporalLawyer(temporalLawyer);
            }
            else
            {
                lawyer.AboutMe = model.AboutMe;
                await _lawyerService.Update(lawyer);
            }

            return Ok();
        }

        [HttpPost("actualizar-visibilidad-perfil")]
        public async Task<IActionResult> UpdateProfileVisibility(bool publicProfile)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            lawyer.PublicProfile = publicProfile;
            await _lawyerService.Update(lawyer);
            return Ok();
        }

        #endregion

        #region -- Experiencia --

        [HttpGet("get-experiencias")]
        public async Task<IActionResult> GetExperiences()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var experiences = await _lawyerExperienceService.GetLawyerExperiencesByLawyerId(lawyer.Id);

            var model = new List<ExperienceViewModel>();

            if (lawyer.ProfileWithChanges)
            {
                model = experiences
                    .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.DELETED && x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED)
                    .OrderByDescending(x => x.StartDate)
                    .Select(x => new ExperienceViewModel
                    {
                        Id = x.Id,
                        Company = x.Company,
                        Description = x.Description,
                        Position = x.Position,
                        WorkArea = x.WorkArea,
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        PhotoUrl = x.PhotoUrl
                    })
                    .ToList();

                foreach (var item in experiences.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED).ToList())
                {
                    var temp = await _temporalLawyerService.GetTemporalLawyerExperience(item.Id);
                    model.Add(new ExperienceViewModel
                    {
                        Id = item.Id,
                        Company = temp.Company,
                        Description = temp.Description,
                        EndDate = temp.EndDate.ToLocalDateFormat(),
                        PhotoUrl = temp.PhotoUrl,
                        Position = temp.Position,
                        StartDate = temp.StartDate.ToLocalDateFormat(),
                        WorkArea = temp.WorkArea
                    });
                }
            }
            else
            {
                model = experiences
                    .OrderByDescending(x => x.StartDate)
                    .Select(x => new ExperienceViewModel
                    {
                        Id = x.Id,
                        Company = x.Company,
                        Description = x.Description,
                        Position = x.Position,
                        WorkArea = x.WorkArea,
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        PhotoUrl = x.PhotoUrl
                    })
                    .ToList();
            }

            return PartialView("Partials/Views/LawyerExperiencePartialView", model);
        }

        [HttpGet("get-experiencia")]
        public async Task<IActionResult> GetExperience(Guid experienceId)
        {
            var experience = await _lawyerExperienceService.Get(experienceId);

            var model = new ExperienceViewModel
            {
                Company = experience.Company,
                Description = experience.Description,
                EndDate = experience.EndDate.ToLocalDateFormat(),
                Id = experience.Id,
                Position = experience.Position,
                StartDate = experience.StartDate.ToLocalDateFormat(),
                WorkArea = experience.WorkArea,
                PhotoUrl = experience.PhotoUrl
            };

            if (experience.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED)
            {
                var temp = await _temporalLawyerService.GetTemporalLawyerExperience(experience.Id);
                model.Company = temp.Company;
                model.Description = temp.Description;
                model.EndDate = temp.EndDate.ToLocalDateFormat();
                model.Position = temp.Position;
                model.StartDate = temp.StartDate.ToLocalDateFormat();
                model.WorkArea = temp.WorkArea;
                model.PhotoUrl = temp.PhotoUrl;
            }

            return Ok(model);
        }

        [HttpPost("agregar-experiencia")]
        public async Task<IActionResult> AddExperience(ExperienceViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var entity = new LawyerExperience
            {
                Company = model.Company,
                Description = model.Description,
                LawyerId = lawyer.Id,
                Position = model.Position,
                StartDate = ConvertHelpers.DatepickerToUtcDateTime(model.StartDate),
                WorkArea = model.WorkArea
            };


            if (model.Photo != null)
            {
                var extension = Path.GetExtension(model.Photo.FileName);
                entity.PhotoUrl = await _cloudStorageService.UploadFile(
                    model.Photo.OpenReadStream(),
                    ConstantHelpers.CLOUD_CONTAINERS.LAWYER_EXPERIENCES,
                    $"{Guid.NewGuid()}",
                    extension
                    );
            }

            if (string.IsNullOrEmpty(model.EndDate))
            {
                entity.EndDate = null;
            }
            else
            {
                entity.EndDate = ConvertHelpers.DatepickerToUtcDateTime(model.EndDate);
            }


            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                entity.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.NEW;
                lawyer.ProfileWithChanges = true;
            }

            await _lawyerExperienceService.Insert(entity);
            return Ok();
        }

        [HttpPost("actualizar-experiencia")]
        public async Task<IActionResult> UpdateExperience(ExperienceViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var experience = await _lawyerExperienceService.Get(model.Id.Value);
            var temporalExperience = await _temporalLawyerService.GetTemporalLawyerExperience(experience.Id);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                experience.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED;
                temporalExperience.Company = model.Company;
                temporalExperience.Description = model.Description;
                temporalExperience.Position = model.Position;
                temporalExperience.StartDate = ConvertHelpers.DatepickerToUtcDateTime(model.StartDate);
                temporalExperience.WorkArea = model.WorkArea;
                lawyer.ProfileWithChanges = true;

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    temporalExperience.EndDate = null;
                }
                else
                {
                    temporalExperience.EndDate = ConvertHelpers.DatepickerToUtcDateTime(model.EndDate);
                }


                if (model.Photo != null)
                {
                    var extension = Path.GetExtension(model.Photo.FileName);
                    temporalExperience.PhotoUrl = await _cloudStorageService.UploadFile(
                        model.Photo.OpenReadStream(),
                        ConstantHelpers.CLOUD_CONTAINERS.LAWYER_EXPERIENCES,
                        $"{Guid.NewGuid()}",
                        extension
                        );
                }
            }
            else
            {
                experience.Company = model.Company;
                experience.Description = model.Description;
                experience.Position = model.Position;
                experience.StartDate = ConvertHelpers.DatepickerToUtcDateTime(model.StartDate);
                experience.WorkArea = model.WorkArea;

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    experience.EndDate = null;
                }
                else
                {
                    experience.EndDate = ConvertHelpers.DatepickerToUtcDateTime(model.EndDate);
                }


                if (model.Photo != null)
                {
                    if (!string.IsNullOrEmpty(experience.PhotoUrl))
                        await _cloudStorageService.TryDelete(experience.PhotoUrl, ConstantHelpers.CLOUD_CONTAINERS.LAWYER_EXPERIENCES);

                    var extension = Path.GetExtension(model.Photo.FileName);
                    experience.PhotoUrl = await _cloudStorageService.UploadFile(
                        model.Photo.OpenReadStream(),
                        ConstantHelpers.CLOUD_CONTAINERS.LAWYER_EXPERIENCES,
                        $"{Guid.NewGuid()}",
                        extension
                        );
                }
            }

            await _lawyerExperienceService.Update(experience);
            return Ok();
        }

        [HttpPost("eliminar-experiencia")]
        public async Task<IActionResult> DeleteExperience(Guid experienceId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var experience = await _lawyerExperienceService.Get(experienceId);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                experience.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.DELETED;
                lawyer.ProfileWithChanges = true;
                await _lawyerExperienceService.Update(experience);
            }
            else
            {
                if (!string.IsNullOrEmpty(experience.PhotoUrl))
                    await _cloudStorageService.TryDelete(experience.PhotoUrl, ConstantHelpers.CLOUD_CONTAINERS.LAWYER_EXPERIENCES);
                await _lawyerExperienceService.Delete(experience);
            }

            return Ok();
        }

        [HttpGet("get-total-experiencia")]
        public async Task<IActionResult> GetTotalExperience()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var result = await _lawyerExperienceService.GetTotalExperienceByLawyerId(lawyer.Id);
            return Ok(result);
        }

        #endregion

        #region  -- Formación Académica --

        [HttpGet("get-estudios")]
        public async Task<IActionResult> GetStudies()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var studies = await _lawyerStudyService.GetLawyerStudiesByLawyerId(lawyer.Id);

            var model = new List<StudyViewModel>();

            if (lawyer.ProfileWithChanges)
            {
                model = studies
                .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.DELETED && x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED)
                .OrderByDescending(x => x.StartDate)
                .Select(x => new StudyViewModel
                {
                    Id = x.Id,
                    StartDate = x.StartDate.ToLocalDateFormat(),
                    Description = x.Description,
                    EndDate = x.EndDate.ToLocalDateFormat(),
                    Grade = x.Grade,
                    Ubication = x.Ubication,
                    Mention = x.Mention
                })
                .ToList();

                foreach (var item in studies.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED).ToList())
                {
                    var temp = await _temporalLawyerService.GetTemporalLawyerStudy(item.Id);
                    model.Add(new StudyViewModel
                    {
                        Id = item.Id,
                        Description = temp.Description,
                        EndDate = temp.EndDate.ToLocalDateFormat(),
                        Grade = temp.Grade,
                        Mention = temp.Mention,
                        StartDate = temp.StartDate.ToLocalDateFormat(),
                        Ubication = temp.Ubication
                    });
                }
            }
            else
            {
                model = studies
                    .OrderByDescending(x => x.StartDate)
                    .Select(x => new StudyViewModel
                    {
                        Id = x.Id,
                        StartDate = x.StartDate.ToLocalDateFormat(),
                        Description = x.Description,
                        EndDate = x.EndDate.ToLocalDateFormat(),
                        Grade = x.Grade,
                        Ubication = x.Ubication,
                        Mention = x.Mention
                    })
                    .ToList();
            }

            return PartialView("Partials/Views/LawyerStudyPartialView", model);
        }

        [HttpGet("get-estudio")]
        public async Task<IActionResult> GetStudy(Guid studyId)
        {
            var study = await _lawyerStudyService.Get(studyId);
            var model = new StudyViewModel
            {
                Description = study.Description,
                Mention = study.Mention,
                EndDate = study.EndDate.ToLocalDateFormat(),
                Grade = study.Grade,
                Id = study.Id,
                StartDate = study.StartDate.ToLocalDateFormat(),
                Ubication = study.Ubication
            };

            if (study.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED)
            {
                var temp = await _temporalLawyerService.GetTemporalLawyerStudy(study.Id);
                model.Description = temp.Description;
                model.Mention = temp.Mention;
                model.EndDate = temp.EndDate.ToLocalDateFormat();
                model.Grade = temp.Grade;
                model.StartDate = temp.StartDate.ToLocalDateFormat();
                model.Ubication = temp.Ubication;
            }

            return Ok(model);
        }

        [HttpPost("agregar-estudio")]
        public async Task<IActionResult> AddStudy(StudyViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var entity = new LawyerStudy
            {
                Description = model.Description,
                Grade = model.Grade,
                LawyerId = lawyer.Id,
                StartDate = ConvertHelpers.DatepickerToUtcDateTime(model.StartDate),
                Ubication = model.Ubication,
                Mention = model.Mention
            };

            if (string.IsNullOrEmpty(model.EndDate))
            {
                entity.EndDate = null;
            }
            else
            {
                entity.EndDate = ConvertHelpers.DatepickerToUtcDateTime(model.EndDate);
            }

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                entity.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.NEW;
                lawyer.ProfileWithChanges = true;
            }

            await _lawyerStudyService.Insert(entity);
            return Ok();
        }

        [HttpPost("actualizar-estudio")]
        public async Task<IActionResult> UpdateStudy(StudyViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var study = await _lawyerStudyService.Get(model.Id.Value);
            var temporalStudy = await _temporalLawyerService.GetTemporalLawyerStudy(study.Id);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                study.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED;
                temporalStudy.Description = model.Description;
                temporalStudy.Grade = model.Grade;
                temporalStudy.Mention = model.Mention;
                temporalStudy.StartDate = ConvertHelpers.DatepickerToUtcDateTime(model.StartDate);
                temporalStudy.Ubication = model.Ubication;
                lawyer.ProfileWithChanges = true;

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    temporalStudy.EndDate = null;
                }
                else
                {
                    temporalStudy.EndDate = ConvertHelpers.DatepickerToUtcDateTime(model.EndDate);
                }
            }
            else
            {
                study.Description = model.Description;
                study.Grade = model.Grade;
                study.StartDate = ConvertHelpers.DatepickerToUtcDateTime(model.StartDate);
                study.Ubication = model.Ubication;
                study.Mention = model.Mention;

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    study.EndDate = null;
                }
                else
                {
                    study.EndDate = ConvertHelpers.DatepickerToUtcDateTime(model.EndDate);
                }
            }

            await _lawyerStudyService.Update(study);
            return Ok();
        }

        [HttpPost("eliminar-estudio")]
        public async Task<IActionResult> DeleteStudy(Guid studyId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var entity = await _lawyerStudyService.Get(studyId);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                entity.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.DELETED;
                lawyer.ProfileWithChanges = true;
                await _lawyerStudyService.Update(entity);
            }
            else
            {
                await _lawyerStudyService.Delete(entity);
            }

            return Ok();
        }

        #endregion

        #region -- Publicaciones --

        [HttpGet("get-publicaciones")]
        public async Task<IActionResult> GetPublications()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var publications = await _lawyerPublicationService.GetLawyerPublications(lawyer.Id, ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.CONFIRMED);

            var model = publications
                .OrderByDescending(x => x.PublicationDate)
                .Select(x => new PublicationViewModel
                {
                    Id = x.Id,
                    PhotoUrl = x.PhotoUrl,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    Description = x.Description,
                    Title = x.Title,
                    Topic = x.Topic,
                })
                .ToList();

            return PartialView("Partials/Views/LawyerPublicationPartialView", model);
        }

        [HttpPost("agregar-publicacion")]
        public async Task<IActionResult> AddPublication(PublicationViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var entity = new LawyerPublication
            {
                Topic = model.Topic,
                Description = model.Description,
                LawyerId = lawyer.Id,
                PublicationDate = DateTime.UtcNow,
                Status = ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.PENDING,
                Title = model.Title
            };
            if (!string.IsNullOrEmpty(model.urlPhotoCropImg))
            {
                var imgArray1 = model.urlPhotoCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    var extension = Path.GetExtension(model.Photo.FileName);
                    entity.PhotoUrl = await _cloudStorageService.UploadFile(
                        stream,
                        ConstantHelpers.CLOUD_CONTAINERS.LAWYER_PUBLICATIONS,
                        $"{Guid.NewGuid()}",
                        extension
                        );
                }
            }
            //if (model.Photo != null)
            //{
            //    var extension = Path.GetExtension(model.Photo.FileName);

            //    entity.PhotoUrl = await _cloudStorageService.UploadFile(
            //        model.Photo.OpenReadStream(),
            //        ConstantHelpers.CLOUD_CONTAINERS.LAWYER_PUBLICATIONS,
            //        $"{user.Surnames}-{Guid.NewGuid()}",
            //        extension
            //        );
            //}

            await _lawyerPublicationService.Insert(entity);
            return Ok();
        }

        [HttpPost("eliminar-publicacion")]
        public async Task<IActionResult> DeletePublication(Guid publicationId)
        {
            var entity = await _lawyerPublicationService.Get(publicationId);

            if (!string.IsNullOrEmpty(entity.PhotoUrl))
                await _cloudStorageService.TryDelete(entity.PhotoUrl, ConstantHelpers.CLOUD_CONTAINERS.LAWYER_PUBLICATIONS);

            await _lawyerPublicationService.Delete(entity);
            return Ok();
        }

        [HttpGet("get-all-publicaiones")]
        public async Task<IActionResult> GetAllPublications()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var model = await _lawyerPublicationService.GetLawyerPublications(parameters, lawyer.Id);
            return PartialView("Partials/Views/LawyerPublicationPartialView_v2", model);
        }

        #endregion

        #region -- Idiomas --
        [HttpGet("get-idiomas")]
        public async Task<IActionResult> GetLanguages()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var languages = await _lawyerLanguageService.GetLanguagesByLawyerId(lawyer.Id);

            var model = new List<LanguageViewModel>();

            if (lawyer.ProfileWithChanges)
            {
                model = languages
                    .Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.DELETED && x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.UPDATED)
                    .Select(x => new LanguageViewModel
                    {
                        Id = x.Id,
                        Name = x.Language.Name,
                        LevelName = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.LEVEL.VALUES[x.Level]
                    })
                    .ToList();

                foreach (var item in languages.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.UPDATED).ToList())
                {
                    var temp = await _temporalLawyerService.GetTemporalLawyerLanguage(item.Id);
                    model.Add(new LanguageViewModel
                    {
                        Level = temp.Level,
                        Name = temp.Language.Name
                    });
                }
            }
            else
            {
                model = languages
                   .Select(x => new LanguageViewModel
                   {
                       Id = x.Id,
                       Name = x.Language.Name,
                       LevelName = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.LEVEL.VALUES[x.Level]
                   })
                   .ToList();
            }


            return PartialView("Partials/Views/LawyerLanguagePartialView", model);
        }

        [HttpPost("agregar-idioma")]
        public async Task<IActionResult> DeleteLanguage(LanguageViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (await _lawyerLanguageService.AnyByLawyerId(lawyer.Id, model.LanguageId))
                return BadRequest("El idioma ya se encuentra registrado");

            var entity = new LawyerLanguage
            {
                LanguageId = model.LanguageId,
                LawyerId = lawyer.Id,
                Level = model.Level
            };

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                entity.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.NEW;
                lawyer.ProfileWithChanges = true;
            }

            await _lawyerLanguageService.Insert(entity);
            return Ok();
        }

        [HttpPost("eliminar-idioma")]
        public async Task<IActionResult> DeleteLanguage(Guid languageId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var lawyerLanguage = await _lawyerLanguageService.Get(languageId);

            if (lawyer.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
            {
                lawyerLanguage.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.DELETED;
                lawyer.ProfileWithChanges = true;
                await _lawyerLanguageService.Update(lawyerLanguage);
            }
            else
            {
                await _lawyerLanguageService.Delete(lawyerLanguage);
            }

            return Ok();
        }

        #endregion

        #region -- Calificaciones --
        [HttpGet("get-califaciones")]
        public async Task<IActionResult> GetQualifications()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var qualifications = await _lawyerQualificationService.GetLawyerQualificationToProfile(lawyer.Id);
            var model = qualifications
                .Select(x => new QualificationViewModel
                {
                    Client = $"{x.Client.User.Name} {x.Client.User.Surnames}",
                    ClientPicture = x.Client.User.Picture,
                    Commentary = x.Commentary,
                    Qualification = x.Qualification
                })
                .ToList();

            return PartialView("Partials/Views/LawyerQualificationPartialView", model);
        }

        [HttpGet("get-all-califaciones")]
        public async Task<IActionResult> GetAllQualifactions()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var model = await _lawyerQualificationService.GetLawyerQualifaction(parameters, lawyer.Id);
            return PartialView("Partials/Views/LawyerQualificationPartialView_v2", model);
        }
        #endregion

        [HttpPost("solicitar-primera-revision")]
        public async Task<IActionResult> RequestFirstReview()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (string.IsNullOrEmpty(lawyer.CAL))
                return BadRequest("Es necesario ingresar su número de colegiatura.");

            if (lawyer.Fee == 0M)
                return BadRequest("Es obligatorio ingresar el valor de la consulta.");

            if (string.IsNullOrEmpty(lawyer.AboutMe))
                return BadRequest("Es obligatorio ingresar 'Acerca de mi'");

            if (!await _lawyerSpecialityThemeService.AnyByLawyer(lawyer.Id))
                return BadRequest("Es obligatorio ingresar especialidades y temas.");

            if (!await _lawyerExperienceService.AnyByLawyer(lawyer.Id))
                return BadRequest("Es obligatorio ingresar por lo menos una experiencia laboral.");

            if (!await _lawyerStudyService.AnyByLawyer(lawyer.Id))
                return BadRequest("Es obligatorio ingresar por lo menos una formación académica.");

            lawyer.Status = ConstantHelpers.ENTITIES.LAWYER.STATUS.IN_EVALUATION;

            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.NEW_LAWYER_MAX_HOUR_TIME_VALIDATION_PROCESS);

            var standardTemplateModel = new StandardEmailModel
            {
                SubHeader = $"Dentro de las siguientes {Convert.ToInt32(configuration.Value)} horas tu perfil será evaluado. De pasar a la siguiente etapa, te mandaremos un correo para agendar una breve entrevista. ¡Muchos éxitos!",
                Title = "¡Gracias por tu registro!"
            };

            var emailTemplate = await _emailTemplateService.GetStandardEmailTemplate(standardTemplateModel);
            await _emailService.SendEmail("Gracias por tu registro", emailTemplate, user.Email);


            var emailTemplateToAsesor = new StandardEmailModel
            {
                Title = "¡UN ABOGADO HA REGISTRADO  SU PERFIL!",
                SubHeader = $"El abogado ha registrado su perfil. Nombre: {user.FullName}, Telefono {user.PhoneNumber}, Correo {user.Email}",
                LinkName = "Ver Perfil",
                LinkRedirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/admin/abogados/perfil/{lawyer.Id}"
            };
            var templateToAsesor = await _emailTemplateService.GetStandardEmailTemplate(emailTemplateToAsesor);
            await _emailService.SendEmail("Abogado Registro Perfil", templateToAsesor, new string[] { ConstantHelpers.EMAIL_ORGANITATION.ADVISER, ConstantHelpers.EMAIL_ORGANITATION.SUPPORTTECHNICAL });


            await _lawyerService.Update(lawyer);
            return Ok();
        }

        [HttpPost("seleccionar-entrevista")]
        public async Task<IActionResult> SelectInterview(Guid lawyerInterviewId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var lawyerInterviews = await _lawyerInterviewService.GetInterviewsByLawyer(lawyer.Id);

            if (lawyerInterviews.Any(y => y.Selected))
                return BadRequest("Ya se ha seleccionado un horario.");

            var entity = await _lawyerInterviewService.Get(lawyerInterviewId);
            entity.Selected = true;
            await _lawyerInterviewService.Update(entity);
            return Ok();
        }

        [HttpPost("solicitar-revision-observaciones")]
        public async Task<IActionResult> RequestReview()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var observation = await _lawyerObservationService.GetLastObservationByType(lawyer.Id, ConstantHelpers.ENTITIES.LAWYER_OBSERVATION.PROCESS.VALIDATION_PROFILE);
            observation.HasBeenCorrected = true;
            await _lawyerObservationService.Update(observation);
            return Ok();
        }
    }
}
