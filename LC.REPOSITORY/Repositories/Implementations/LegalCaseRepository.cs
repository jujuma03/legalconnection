using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.Structs;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Custom.General;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.REPOSITORY.Repositories.Template;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class LegalCaseRepository : Repository<LegalCase>, ILegalCaseRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;

        public LegalCaseRepository(
            LegalConnectionContext context,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService
            ) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        #region General

        public async Task<bool> LegalCaseApplicantsCompleted(Guid legalCaseId)
        {
            var configuration = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_VACANCIES).FirstOrDefaultAsync();
            var maxVacancies = Convert.ToInt32(configuration.Value);

            var applicants = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId).CountAsync();
            if (applicants >= maxVacancies)
                return true;

            return false;
        }
        #endregion

        #region Client

        public async Task<PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Client.LegalCaseCustomModel>> GetLegalCasesItemsToClient(PaginationStructs.SentParameters sentParameters, Guid clientId)
        {
            var query = _context.LegalCases.OrderByDescending(x => x.CreatedAt)
                .Where(x => x.ClientId == clientId)
                .AsNoTracking();

            var recordsTotal = await query.CountAsync();

            var result = await query
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new ENTITIES.Custom.Client.LegalCaseCustomModel
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt.ToLocalDateTimeFormat(),
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    Speciality = x.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.OfficialName).FirstOrDefault(),
                    Status = x.Status,
                    Type = x.Type,
                    SpecialityThemes = x.LegalCaseSpecialityThemes
                        .Select(y => new SpecialityThemesCustomModel
                        {
                            Id = y.SpecialityThemeId,
                            Name = y.SpecialityTheme.OfficialName
                        }).ToList(),
                    Lawyers = x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED ?
                        x.LegalCaseApplicantLawyers
                             .Select(y => new ENTITIES.Custom.General.LawyerCustomModel
                             {
                                 Name = y.Lawyer.User.Name,
                                 Surname = y.Lawyer.User.Surnames,
                                 PhotoUrl = y.Lawyer.User.Picture
                             }).ToList()
                            : x.LegalCaseLawyers
                                .Select(y => new ENTITIES.Custom.General.LawyerCustomModel
                                {
                                    Name = y.Lawyer.User.Name,
                                    Surname = y.Lawyer.User.Surnames,
                                    PhotoUrl = y.Lawyer.User.Picture
                                }).ToList()
                })
                .ToListAsync();

            return new PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Client.LegalCaseCustomModel>
            {
                Data = result,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = sentParameters.Page,
                    RecordsPerDraw = sentParameters.RecordsPerDraw
                }
            };
        }
        public async Task<bool> ClientHasAccess(Guid clientId, Guid legalCaseId)
         => await _context.LegalCases.AnyAsync(y => y.ClientId == clientId && y.Id == legalCaseId);
        public async Task<LC.ENTITIES.Custom.Client.LegalCaseCustomModel> GetLegalCaseToClient(Guid legalCaseId)
        {
            var query = _context.LegalCases.Where(x => x.Id == legalCaseId).AsNoTracking();

            var configurationPostulate = await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE);
            var configurationAcceptAndPay = await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER);

            var configurationMaxlengt = await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var maxlength = Convert.ToInt32(configurationMaxlengt.Value);

            var maxHoursPostulate = Convert.ToInt32(configurationPostulate.Value);
            var maxHoursAcceptAndPayLawyer = Convert.ToInt32(configurationAcceptAndPay.Value);

            var legalCase = await query
                .Select(x => new LC.ENTITIES.Custom.Client.LegalCaseCustomModel
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt.ToLocalDateTimeFormat(),

                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    Speciality = x.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.OfficialName).FirstOrDefault(),
                    Status = x.Status,
                    Type = x.Type,
                    SpecialityThemes = x.LegalCaseSpecialityThemes
                        .Select(y => new SpecialityThemesCustomModel
                        {
                            Id = y.SpecialityThemeId,
                            Name = y.SpecialityTheme.OfficialName
                        }).ToList(),
                    FileUrl = x.UrlFile,
                    AnyLawyerApplicant = x.LegalCaseApplicantLawyers.Any(),
                    ValidationDateUtc = x.ValidationDate,
                    DerivatedDateUtc = x.DerivedDate,
                    MaxHoursToClientAcceptAndPayLawyer = maxHoursAcceptAndPayLawyer,
                    DescriptionMaxLength = maxlength,
                })
                .FirstOrDefaultAsync();

            if (legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.REJECTED)
            {
                var entityObservation = await _context.LegalCaseObservations.Where(x => x.LegalCaseId == legalCase.Id && x.Process == ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.VALIDATION).FirstOrDefaultAsync();
                legalCase.Observation = entityObservation.Observation;
            }

            if (legalCase.DerivatedDateUtc.HasValue)
            {
                legalCase.ApplicationDeadlineUtc = legalCase.DerivatedDateUtc.Value.AddHours(maxHoursPostulate);
                legalCase.SelectionDeadlineUtc = legalCase.ApplicationDeadlineUtc.Value.AddHours(maxHoursAcceptAndPayLawyer);
            }

            return legalCase;

        }

        public async Task<ResultCustomModel> DeleteLegalCase(Guid clientId, Guid legalCaseId)
        {
            var result = new ResultCustomModel();
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            var client = await _context.Clients.Where(x => x.Id == clientId).FirstOrDefaultAsync();
            var user = await _context.Users.Where(x => x.Id == client.UserId).FirstOrDefaultAsync();
            var role = await _context.Roles.Where(x => x.Name == ConstantHelpers.ROLES.CLIENT).FirstOrDefaultAsync();

            if (legalCase.ClientId != clientId)
            {
                result.Success = false;
                result.Message = "El usuario no tiene los permisos necesarios para eliminar este caso.";
            }

            var legalCaseLawyers = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId).ToListAsync();
            var anyLawyerCompleted = legalCaseLawyers.Any(y => y.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PAYMENT_MADE || y.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED);

            if (anyLawyerCompleted)
            {
                result.Success = false;
                result.Message = "El caso tiene abogados con pagos asociados.";
            }

            _context.LegalCaseLawyers.RemoveRange(legalCaseLawyers);

            var applicantLegalCase = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId).ToListAsync();

            if (legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL)
            {
                applicantLegalCase.ForEach(x => x.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.PENDING);
            }
            else
            {
                applicantLegalCase.ForEach(x => x.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.DIRECTED);

                var lawyerUsersEmail = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId).Select(x => x.Lawyer.User.Email).ToListAsync();

                var modelEmail = new StandardEmailModel
                {
                    Title = "Caso Cerrado",
                    SubHeader = $"El cliente {user.Name} {user.Surnames}, quien reportó el siguiente caso: {legalCase.Code}, ha eliminado la solicitud de contratación. Por lo que, ya no se podrá visualizar en la bandeja de casos.",
                    LinkRedirect = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}",
                    LinkName = $"Ir a {ConstantHelpers.PROJECT}"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                await _emailService.SendEmail("Caso Cerrado", template, lawyerUsersEmail.ToArray());
            }

            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CLOSED;
            var observation = new LegalCaseObservation
            {
                CreatorRoleId = role.Id,
                CreatorUserId = user.Id,
                LegalCaseId = legalCase.Id,
                Observation = "Cerrado por cliente",
                Process = ConstantHelpers.ENTITIES.LEGAL_CASE_OBSERVATION.PROCESS.CLOSED
            };

            await _context.AddAsync(observation);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = "Caso Eliminado con Éxito";
            return result;
        }

        public async Task<int> GetLegalCaseCountByType(byte type)
        {
            var quantity = await _context.LegalCases.Where(x => x.Type == type).CountAsync() + 1;
            return quantity;
        }

        #endregion

        #region Admin

        public async Task<LC.ENTITIES.Custom.Admin.LegalCaseCustomModel> GetLegalCaseToAdmin(Guid legalCaseId)
        {
            var query = _context.LegalCases.Where(x => x.Id == legalCaseId).AsNoTracking();

            var legalCase = await query
                .Select(x => new LC.ENTITIES.Custom.Admin.LegalCaseCustomModel
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt.ToLocalDateTimeFormat(),
                    ValidatedAt = x.ValidationDate.ToLocalDateTimeFormat(),
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    SpecialityId = x.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.Id).FirstOrDefault(),
                    Speciality = x.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.OfficialName).FirstOrDefault(),
                    Status = x.Status,
                    Type = x.Type,
                    FileUrl = x.UrlFile,
                    Client = new ENTITIES.Custom.General.ClientCustomModel
                    {
                        Department = x.Client.User.District.Province.Department.Name,
                        Name = x.Client.User.Name,
                        Surnames = x.Client.User.Surnames,
                        PhoneNumber = x.Client.User.PhoneNumber,
                        PhotoUrl = x.Client.User.Picture,
                        Province = x.Client.User.District.Province.Name,
                        Email = x.Client.User.Email,
                        Document = x.Client.User.Document
                    },
                    SpecialityThemes = x.LegalCaseSpecialityThemes
                        .Select(y => new SpecialityThemesCustomModel
                        {
                            Id = y.SpecialityThemeId,
                            Name = y.SpecialityTheme.OfficialName
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return legalCase;
        }

        #endregion

        #region Lawyer

        public async Task<bool> LawyerHasAccess(Guid lawyerId, Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();

            if (legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS ||
                legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATED ||
                legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.REJECTED ||
                legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CORRECTED_OBSERVATIONS ||
                legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CLOSED)
            {
                return false;
            }

            return true;
        }
        public async Task<ENTITIES.Custom.Lawyer.LegalCaseCustomModel> GetLegalCaseToLawyer(Guid legalCaseId, Guid lawyerId)
        {
            var query = _context.LegalCases
                .Where(x => x.Id == legalCaseId).AsNoTracking();
            var lawyer = await _context.Lawyers.Where(x => x.Id == lawyerId)
                .Select(x =>
                new
                {
                    x.FreeUser,
                    IsFreeFee = x.FreeFirst,
                    clients = x.LegalCaseLawyers.Where(y=>y.LegalCaseId!= legalCaseId)
                                                .Select(y=>y.LegalCase.ClientId),
                })
                .FirstOrDefaultAsync();
            //IsFreeFee = x.FreeFirst && x.LegalCaseLawyers.Where(y => y.LegalCase.Client.UserId == userId).Count() == 0,

            var maxVacancies = Convert.ToInt32((await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_VACANCIES)).Value);
            var result = await query
                .Select(x => new ENTITIES.Custom.Lawyer.LegalCaseCustomModel
                {
                    Id = x.Id,
                    Status = x.Status,
                    Type = x.Type,
                    IsFiledCase = x.LegalCaseFiledLawyers.Any(),
                    Department = x.Province.Department.Name,
                    DerivatedAt = x.DerivedDate.ToLocalDateTimeFormat(),
                    DerivatedAtUtc = x.DerivedDate.Value,
                    Description = x.Description,
                    Province = x.Province.Name,
                    PostulationDate = x.LegalCaseApplicantLawyers.Where(y => y.LawyerId == lawyerId).Select(y => y.ApplicationDate.ToLocalDateTimeFormat()).FirstOrDefault(),
                    IsFreeUser = lawyer.FreeUser,
                    IsFreeFee = !lawyer.clients.Contains(x.ClientId) && lawyer.IsFreeFee,
                    TotalVacancies = maxVacancies,
                    TotalApplicants = x.LegalCaseApplicantLawyers.Count(),
                    Speciality = x.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.OfficialName).FirstOrDefault(),
                    SpecialityThemes = x.LegalCaseSpecialityThemes
                        .Select(y => new SpecialityThemesCustomModel
                        {
                            Id = y.SpecialityThemeId,
                            Name = y.SpecialityTheme.OfficialName
                        }).ToList(),
                    Questions = x.LegalCaseResponses.Select(x => new QuestionCustomModel
                    {
                        Question = x.LegalCaseQuestion.Description,
                        Response = x.Description
                    }).ToList(),
                    Client = new ENTITIES.Custom.General.ClientCustomModel
                    {
                        Name = x.Client.User.Name,
                        Surnames = x.Client.User.Surnames,
                        Department = x.Client.User.District.Province.Department.Name,
                        Province = x.Client.User.District.Province.Name,
                        Document = x.Client.User.Document,
                        Email = x.Client.User.Email,
                        PhoneNumber = x.Client.User.PhoneNumber,
                        PhotoUrl = x.Client.User.Picture
                    },
                })
                .FirstOrDefaultAsync();

            var lawyerApplicants = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId).ToListAsync();
            var lawyers = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId).ToListAsync();

            if (result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER && !lawyerApplicants.Any(y => y.LawyerId == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.SEARCH_LEGALCASES;
            }
            else if (result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER && lawyerApplicants.Any(y => y.LawyerId == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.INCOMING_REQUESTS;
            }
            else if (result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS && lawyers.Any(z => z.LawyerId == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.LEGAL_CASE_IN_COURSE;
            }
            else if ((result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT || result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER || result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER) && lawyerApplicants.Any(y => y.LawyerId == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.POSTULATED_CASES;
            }

            return result;
        }
        public async Task<PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Lawyer.LegalCaseCustomModel>> GetLegalCaseItemsToLawyer(PaginationStructs.SentParameters parameters, Guid lawyerId, byte typeSearch)
        {
            var maxHours = Convert.ToInt32((await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE)).Value);
            var maxVacancies = Convert.ToInt32((await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_VACANCIES)).Value);
            var provinceId = await _context.Lawyers.Where(x => x.Id == lawyerId).Select(x => x.User.District.ProvinceId).FirstOrDefaultAsync();
            var specialityThemes = await _context.LawyerSpecialityThemes.Where(x => x.LawyerId == lawyerId).Select(x => x.SpecialityThemeId).ToListAsync();

            var query = _context.LegalCases
                .Where(x =>
                x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS &&
                x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATED &&
                x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.REJECTED &&
                x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CORRECTED_OBSERVATIONS &&
                x.LegalCaseSpecialityThemes.Any(y => specialityThemes.Contains(y.SpecialityThemeId)))
                .AsNoTracking();


            switch (typeSearch)
            {
                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.SEARCH_LEGALCASES:
                    query = query.Where(x => x.ProvinceId == provinceId && x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER && x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL && !x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId) && x.DerivedDate.Value.AddHours(maxHours) > DateTime.UtcNow);
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.INCOMING_REQUESTS:
                    query = query.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER && x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId));
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.LEGAL_CASE_IN_COURSE:
                    query = query.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS && x.LegalCaseLawyers.Any(y => y.LawyerId == lawyerId));
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.POSTULATED_CASES:
                    query = query.Where(x => (x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER) && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId));
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.ARCHIVED:
                    query = _context.LegalCaseFiledLawyers.Where(x => x.LawyerId == lawyerId && x.EndDateTimeAt >= DateTime.UtcNow).Select(x => x.LegalCase);
                    break;
            }

            var recordsTotal = await query.CountAsync();

            var result = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(parameters.RecordsToTake)
                .Take(parameters.RecordsPerDraw)
                .Select(x => new ENTITIES.Custom.Lawyer.LegalCaseCustomModel
                {
                    Id = x.Id,
                    Client = new ENTITIES.Custom.General.ClientCustomModel
                    {
                        Name = x.Client.User.Name,
                        Surnames = x.Client.User.Surnames
                    },
                    IsFiledCase = x.LegalCaseFiledLawyers.Any(),
                    DerivatedAt = x.DerivedDate.ToLocalDateTimeFormat(),
                    DerivatedAtUtc = x.DerivedDate.Value,
                    Speciality = x.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.OfficialName).FirstOrDefault(),
                    SpecialityThemes = x.LegalCaseSpecialityThemes
                    .Select(y => new SpecialityThemesCustomModel
                    {
                        Id = y.SpecialityThemeId,
                        Name = y.SpecialityTheme.OfficialName
                    }).ToList(),
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    Status = x.Status,
                    TotalVacancies = maxVacancies,
                    TotalApplicants = x.LegalCaseApplicantLawyers.Count(),
                    SearchType = typeSearch,
                    Type = x.Type
                })
                .ToListAsync();

            return new PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Lawyer.LegalCaseCustomModel>
            {
                Data = result,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = parameters.Page,
                    RecordsPerDraw = parameters.RecordsPerDraw
                }
            };

        }
        public async Task<List<LegalCaseLawyer>> GetLegalCaseLawyers(Guid legalCaseId)
        {
            var result = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId)
               .Select(x => new LegalCaseLawyer
               {
                   CreatedAt = x.CreatedAt,
                   LegalCaseId = x.LegalCaseId,
                   ResponseTime = x.ResponseTime,
                   LawyerId = x.LawyerId,
                   Status = x.Status,
                   Fee = x.Fee,
                   Lawyer = new Lawyer
                   {
                       FreeFirst = x.Lawyer.FreeFirst,
                       HiredCases = x.Lawyer.LegalCaseLawyers.Count(),
                       User = new ApplicationUser
                       {
                           Id = x.Lawyer.UserId,
                           Surnames = x.Lawyer.User.Surnames,
                           Email = x.Lawyer.User.Email,
                           Name = x.Lawyer.User.Name,
                           LastConnection = x.Lawyer.User.LastConnection,
                           CreatedAt = x.Lawyer.User.CreatedAt,
                           Picture = x.Lawyer.User.Picture
                       },
                       Id = x.Lawyer.Id,
                       Fee = x.Lawyer.Fee,
                       AboutMe = x.Lawyer.AboutMe,
                       LawyerSpecialityThemes = x.Lawyer.LawyerSpecialityThemes.Select(x => new LawyerSpecialityTheme
                       {
                           LawyerId = x.LawyerId,
                           SpecialityThemeId = x.SpecialityThemeId,
                           SpecialityTheme = new SpecialityTheme
                           {
                               ColloquialName = x.SpecialityTheme.ColloquialName,
                               OfficialName = x.SpecialityTheme.OfficialName,
                               Speciality = new Speciality
                               {
                                   ColloquialName = x.SpecialityTheme.Speciality.ColloquialName,
                                   OfficialName = x.SpecialityTheme.Speciality.OfficialName
                               }
                           }
                       }).ToList()
                   }
               })
               .ToListAsync();

            return result;
        }
        public async Task<int> GetLegalCasesPaymentByClient(Guid lawyerId, Guid clientId)
            => await _context.LegalCases.CountAsync(x => x.ClientId == clientId && x.Payments.Any(y => y.LawyerId == lawyerId));
        public async Task<ResultCustomModel> CloseLegalCase(Guid legalCaseId, Guid lawyerId)
        {
            var model = new ResultCustomModel();
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();

            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED;
            var client = await _context.Clients.Where(x => x.Id == legalCase.ClientId).FirstOrDefaultAsync();
            var userClient = await _context.Users.Where(x => x.Id == client.UserId).FirstOrDefaultAsync();

            var lawyerLegalCase = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId && x.LawyerId == lawyerId).FirstOrDefaultAsync();
            lawyerLegalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED;

            var modelEmail = new StandardEmailModel
            {
                Title = "Caso Concluido",
                SubHeader = "Por favor, califica al abogado que te atendió a través del siguiente enlace.",
                LinkRedirect = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/calificaciones/{legalCase.Id}/{client.Id}/{lawyerId}",
                LinkName = "Calificar Abogado"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
            await _emailService.SendEmail("Calificar Abogado", template, userClient.Email);

            await _context.SaveChangesAsync();
            model.Success = true;
            return model;
        }
        public async Task<PaginationStructs.ReturnedData<ENTITIES.Custom.Lawyer.LegalCaseCustomModel>> GetLegalCasesFinishedToLawyer(PaginationStructs.SentParameters parameters, Guid lawyerId)
        {
            var lawyer = await _context.Lawyers.Where(x => x.Id == lawyerId).FirstOrDefaultAsync();
            var query = _context.LegalCaseLawyers.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED).OrderByDescending(x => x.CreatedAt).AsNoTracking();
            var recordsTotal = await query.CountAsync();

            var result = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(parameters.RecordsToTake)
                .Take(parameters.RecordsPerDraw)
                .Select(x => new ENTITIES.Custom.Lawyer.LegalCaseCustomModel
                {
                    Id = x.LegalCase.Id,
                    Client = new ENTITIES.Custom.General.ClientCustomModel
                    {
                        Name = x.LegalCase.Client.User.Name,
                        Surnames = x.LegalCase.Client.User.Surnames
                    },
                    DerivatedAt = x.LegalCase.DerivedDate.ToLocalDateTimeFormat(),
                    DerivatedAtUtc = x.LegalCase.DerivedDate.Value,
                    Speciality = x.LegalCase.LegalCaseSpecialityThemes.Select(y => y.SpecialityTheme.Speciality.OfficialName).FirstOrDefault(),
                    SpecialityThemes = x.LegalCase.LegalCaseSpecialityThemes
                    .Select(y => new SpecialityThemesCustomModel
                    {
                        Id = y.SpecialityThemeId,
                        Name = y.SpecialityTheme.OfficialName
                    }).ToList(),
                    Department = x.LegalCase.Province.Department.Name,
                    Province = x.LegalCase.Province.Name,
                    Description = x.LegalCase.Description,
                    Status = x.Status,
                    FinishDate = x.LegalCase.FinishDate.ToLocalDateTimeFormat()
                })
                .ToListAsync();

            foreach (var item in result)
            {
                var payment = await _context.Payments.Where(x => x.LawyerId == lawyerId && x.LegalCaseId == item.Id).Select(x => x.LawyerAmount).FirstOrDefaultAsync();
                item.Payment = payment;
            }

            return new PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Lawyer.LegalCaseCustomModel>
            {
                Data = result,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = parameters.Page,
                    RecordsPerDraw = parameters.RecordsPerDraw
                }
            };

        }
        public async Task<ResultCustomModel> RejectDirectedLegalCase(Guid lawyerId, Guid legalCaseId)
        {
            var result = new ResultCustomModel();

            var legalCaseApplicant = await _context.LegalCaseApplicantLawyers.Where(x => x.LawyerId == lawyerId && x.LegalCaseId == legalCaseId).FirstOrDefaultAsync();
            if (legalCaseApplicant is null)
            {
                result.Message = "Usted no tiene acceso para este proceso.";
                result.Success = false;

                return result;
            }

            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();

            var userEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();

            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.REJECTED_BY_LAWYER;
            legalCaseApplicant.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.REJECTED_DIRECTED;

            var modelEmail = new StandardEmailModel
            {
                Title = "Caso Rechazado",
                SubHeader = "Lamentablemente el abogado seleccionado por contacto directo ha rechazado el caso. Pronto uno de nuestros asesores se contactará.",
                LinkRedirect = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}",
                LinkName = "Ir a Legal Connection"
            };

            var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
            await _emailService.SendEmail("Calificar Abogado", template, userEmail);
            await _context.SaveChangesAsync();
            result.Success = true;

            return result;
        }
        public async Task<ResultCustomModel> Postulate(LegalCaseApplicantLawyer entity)
        {
            var result = new ResultCustomModel();
            var configuration = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_VACANCIES).FirstOrDefaultAsync();
            if (configuration is null)
            {
                configuration = new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_VACANCIES,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_VACANCIES]
                };
            }

            var maxVacancies = Convert.ToInt32(configuration.Value);

            var legalCase = await _context.LegalCases.Where(x => x.Id == entity.LegalCaseId).FirstOrDefaultAsync();
            var user = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User).FirstOrDefaultAsync();

            if (legalCase.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER)
            {
                result.Success = false;
                result.Message = "No se puede postular a este caso";
                return result;
            }

            var applicants = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == entity.LegalCaseId).ToListAsync();

            if (applicants.Any(y => y.LawyerId == entity.LawyerId))
            {
                result.Success = false;
                result.Message = "Usted ya se encuentra registrado como postulante en este caso";
                return result;
            }

            if (applicants.Count() >= maxVacancies)
            {
                result.Success = false;
                result.Message = "Ya se ha alcanzado el máximo de postulaciones para este caso";
                return result;
            }

            if (applicants.Count() + 1 == maxVacancies)
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER;
            }

            await _context.LegalCaseApplicantLawyers.AddAsync(entity);
            await _context.SaveChangesAsync();
            result.Success = true;
            result.Message = "Postulación realizada con éxito";
            return result;
        }

        #endregion

        public async Task<List<LegalCaseCustomModel>> GetLegalCasesCustomModel(ClaimsPrincipal user, byte? status)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = _context.LegalCases.OrderByDescending(x => x.CreatedAt).AsQueryable();

            if (user.IsInRole(ConstantHelpers.ROLES.CLIENT))
            {
                var client = await _context.Clients.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                query = query.Where(x => x.ClientId == client.Id);
            }
            else if (user.IsInRole(ConstantHelpers.ROLES.LAWYER))
            {
                var lawyer = await _context.Lawyers.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED)
                {
                    query = query.Where(x => x.Status == status && x.LegalCaseLawyers.Any(y => y.LawyerId == lawyer.Id));
                }
            }

            var result = await query
                .Select(x => new LegalCaseCustomModel
                {
                    Id = x.Id,
                    ClientFullName = $"{x.Client.User.Name} {x.Client.User.Surnames}",
                    CreatedAt = x.CreatedAt.ToLocalDateTimeFormat(),
                    MainLawyer = x.LegalCaseLawyers.Select(x => $"{x.Lawyer.User.Name} {x.Lawyer.User.Surnames}").FirstOrDefault(),
                    PhotoMainLawyer = x.LegalCaseLawyers.Select(x => x.Lawyer.User.Picture).FirstOrDefault(),
                    ValidatedAt = x.ValidationDate.ToLocalDateTimeFormat(),
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    FinishDate = x.FinishDate.ToLocalDateTimeFormat(),
                    //Speciality = x.SpecialityTheme.Speciality.ColloquialName,
                    Status = x.Status,
                    TotalApplicants = x.LegalCaseApplicantLawyers.Count()
                })
                .ToListAsync();

            if (user.IsInRole(ConstantHelpers.ROLES.LAWYER))
            {
                if (status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED)
                {
                    foreach (var item in result)
                    {
                        var payments = await _context.Payments.Where(x => x.LegalCaseId == item.Id).ToListAsync();
                        item.Payments = payments.Select(x => new LegalCasePaymentCustomModel
                        {
                            LawyerId = x.LawyerId,
                            Amount = x.TotalAmount
                        }).ToList();
                    }
                }
            }

            return result;
        }
        public async Task<LegalCaseCustomModel> GetLegalCaseCustomModel(Guid legalCaseId)
        {
            var query = _context.LegalCases.Where(x => x.Id == legalCaseId).AsQueryable();
            var configuration = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER).FirstOrDefaultAsync();

            if (configuration is null)
            {
                configuration = new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER]
                };
            }

            var result = await query
                .Select(x => new LegalCaseCustomModel
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt.ToLocalDateTimeFormat(),
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    //SpecialityTheme = x.SpecialityTheme.OfficialName,
                    //MainLawyer = $"{x.MainLawyer.User.Name} {x.MainLawyer.User.Surnames}",
                    //PhotoMainLawyer = x.MainLawyer.User.Picture,
                    //Speciality = x.SpecialityTheme.Speciality.OfficialName,
                    ValidatedAt = x.ValidationDate.ToLocalDateTimeFormat(),
                    Status = x.Status,
                    TotalApplicants = x.LegalCaseApplicantLawyers.Count(),
                    PublicatedDays = (int)(DateTime.UtcNow.Date - x.CreatedAt.Date).TotalDays,
                    Client = new ENTITIES.Custom.ClientCustomModel
                    {
                        Department = x.Client.User.District.Province.Department.Name,
                        Province = x.Client.User.District.Province.Name,
                        Email = x.Client.User.Email,
                        Fullname = $"{x.Client.User.Name} {x.Client.User.Surnames}",
                        PhoneNumber = x.Client.User.PhoneNumber,
                        Id = x.Client.Id,
                        PhotoUrl = x.Client.User.Picture
                    },
                    MaxHoursToClientAcceptLawyer = Convert.ToInt32(configuration.Value)
                })
                .FirstOrDefaultAsync();

            var firstApplicationDate = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId)
                .OrderByDescending(x => x.ApplicationDate).Select(x => x.ApplicationDate).FirstOrDefaultAsync();

            if (firstApplicationDate != DateTime.MinValue)
            {
                var maxHourTime = Convert.ToInt32(configuration.Value);
                var finishDateTime = firstApplicationDate.AddHours(maxHourTime);
                if (DateTime.UtcNow > finishDateTime)
                    result.TimeFinishedToAcceptApplications = true;
            }

            return result;
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetLegalCasesDatatable(DataTablesStructs.SentParameters parameters, byte? status, byte? type, DateTime? dateStart = null, DateTime? dateEnd = null, string search = null)
        {
            Expression<Func<LegalCase, dynamic>> orderByPredicate = null;

            switch (parameters.OrderColumn)
            {
                case "0":
                    orderByPredicate = (x) => x.Code;
                    break;
                case "1":
                    orderByPredicate = (x) => x.Client.User.FullName;
                    break;
                case "3":
                    orderByPredicate = (x) => x.CreatedAt;
                    break;
                case "4":
                    orderByPredicate = (x) => x.ValidationDate;
                    break;
                case "5":
                    orderByPredicate = (x) => string.Join(", ", x.LegalCaseSpecialityThemes.Select(y => $"{y.SpecialityTheme.Speciality.OfficialName}"));
                    break;
                case "6":
                    orderByPredicate = (x) => x.Status;
                    break;
                default:
                    orderByPredicate = (x) => x.CreatedAt;
                    break;
            }

            var query = _context.LegalCases
                .AsQueryable();


            if (type.HasValue)
                query = query.Where(x => x.Type == type);

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            if (dateStart.HasValue && dateEnd.HasValue)
            {
                dateStart = DateTime.SpecifyKind(dateStart.Value, DateTimeKind.Unspecified).ToUtcDateTime();
                dateEnd = DateTime.SpecifyKind(dateEnd.Value, DateTimeKind.Unspecified).AddDays(0).AddTicks(-1).ToUtcDateTime();

                query = query.Where(x => dateStart <= x.CreatedAt && x.CreatedAt <= dateEnd).AsNoTracking();
            }

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Code.ToLower().Contains(search) ||
                                        x.Client.User.FullName.ToLower().Contains(search));

            var recordsTotal = await query.CountAsync();
            query = query.AsQueryable();

            var data = await query
                .OrderByCondition(parameters.OrderDirection, orderByPredicate)
                .Skip(parameters.PagingFirstRecord)
                .Take(parameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    x.Code,
                    speciality = string.Join(", ", x.LegalCaseSpecialityThemes.Select(y => $"{y.SpecialityTheme.Speciality.OfficialName}")),
                    lawyer = x.LegalCaseApplicantLawyers.Where(y=> y.LegalCaseId == x.Id).Select(y => $"{y.Lawyer.User.Name} {y.Lawyer.User.Surnames}").FirstOrDefault(),
                    clientName = x.Client.User.Name,
                    clientSurnames = x.Client.User.Surnames,
                    clienteFullname = x.Client.User.FullName,
                    province = x.Province.Name,
                    department = x.Province.Department.Name,
                    paymentdate = x.Payments.Select(y => y.CreatedAt.ToLocalDateTimeFormat()).FirstOrDefault() ?? "",
                    approvedAt = x.ValidationDate.ToLocalDateTimeFormat(),
                    finishat = x.FinishDate.ToLocalDateTimeFormat() ?? "",
                    createdAt = x.CreatedAt.ToLocalDateTimeFormat(),
                    statusName = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALUES[x.Status],
                    statusColor = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.COLORS[x.Status],
                    status = x.Status,
                    type = x.Type,
                })
                .ToListAsync();

            var recordsFiltered = data.Count;

            var result = new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = parameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal,
            };

            return result;

        }
        public async Task<List<LegalCaseExcelTemplate>> GetLegalCasesExport(byte? status, byte? type = null, DateTime? dateStart = null, DateTime? dateEnd = null, string search = null)
        {
            var query = _context.LegalCases
                .AsQueryable();

            if (type.HasValue)
                query = query.Where(x => x.Type == type);

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            if (dateStart.HasValue && dateEnd.HasValue)
            {
                dateStart = DateTime.SpecifyKind(dateStart.Value, DateTimeKind.Unspecified).ToUtcDateTime();
                dateEnd = DateTime.SpecifyKind(dateEnd.Value, DateTimeKind.Unspecified).AddDays(0).AddTicks(-1).ToUtcDateTime();

                query = query.Where(x => dateStart <= x.CreatedAt && x.CreatedAt <= dateEnd).AsNoTracking();
            }

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Code.ToLower().Contains(search) ||
                                        x.Client.User.FullName.ToLower().Contains(search));

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new LegalCaseExcelTemplate
                {
                    Id = x.Id,
                    Code = x.Code,
                    ClientFullName = x.Client.User.FullName,
                    Dni = x.Client.User.Document,
                    PhoneNumber = x.Client.User.PhoneNumber,
                    Email = x.Client.User.Email,
                    CreatedAt = x.CreatedAt.ToLocalDateTimeFormat(),
                    Speciality = string.Join(", ", x.LegalCaseSpecialityThemes.Select(y => $"{y.SpecialityTheme.Speciality.OfficialName}")),
                    SpecialityThemes = string.Join(", ", x.LegalCaseSpecialityThemes.Select(y => $"{y.SpecialityTheme.OfficialName}")),
                    Lawyer = string.Join(", ", x.LegalCaseLawyers.Select(y => $"{y.Lawyer.User.Name} {y.Lawyer.User.Surnames}")) ?? "",
                    Status = x.Status,
                    StatusName = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALUES[x.Status],
                })
                .ToListAsync();

            return data;
        }
        public async Task<List<LegalCaseItemCustomModel>> GetLegalCaseItemsCustomModel(Guid lawyerId, byte typeSearch)
        {
            var configuration = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE).FirstOrDefaultAsync();

            if (configuration is null)
            {
                configuration = new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE],
                };
            }

            var maxHours = Convert.ToInt32(configuration.Value);
            var provinceId = await _context.Lawyers.Where(x => x.Id == lawyerId).Select(x => x.User.District.ProvinceId).FirstOrDefaultAsync();

            var specialityThemes = await _context.LawyerSpecialityThemes.Where(x => x.LawyerId == lawyerId).Select(x => x.SpecialityThemeId).ToListAsync();
            var query = _context.LegalCases
                .Where(x =>
                x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS &&
                x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATED) /*&&
*/                //specialityThemes.Contains(x.SpecialityThemeId))
                .AsNoTracking();

            switch (typeSearch)
            {
                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.SEARCH_LEGALCASES:
                    query = query.Where(x => x.ProvinceId == provinceId && x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER && x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL && !x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId) && x.DerivedDate.Value.AddHours(maxHours) > DateTime.UtcNow);
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.INCOMING_REQUESTS:
                    query = query.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER && x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId));
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.LEGAL_CASE_IN_COURSE:
                    query = query.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS && x.LegalCaseLawyers.Any(y => y.LawyerId == lawyerId));
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.POSTULATED_CASES:
                    query = query.Where(x => (x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER) && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId));
                    break;

                case ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.ARCHIVED:
                    query = _context.LegalCaseFiledLawyers.Where(x => x.LawyerId == lawyerId && x.EndDateTimeAt >= DateTime.UtcNow).Select(x => x.LegalCase);
                    break;
                default:
                    return null;
            }

            var max_vacancies_value = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_VACANCIES).Select(x => x.Value).FirstOrDefaultAsync();
            var max_vacancies = 0;

            if (string.IsNullOrEmpty(max_vacancies_value))
            {
                max_vacancies = Convert.ToInt32(ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_VACANCIES]);
            }
            else
            {
                max_vacancies = Convert.ToInt32(max_vacancies_value);
            }

            var result = await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new LegalCaseItemCustomModel
                {
                    ClientFullName = $"{x.Client.User.Name} {x.Client.User.Surnames}",
                    ValidatedAt = x.ValidationDate.ToLocalDateTimeFormat(),
                    Id = x.Id,
                    //Speciality = x.SpecialityTheme.Speciality.ColloquialName,
                    //SpecialityTheme = x.SpecialityTheme.ColloquialName,
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    Status = x.Status,
                    TotalVacancies = max_vacancies,
                    TotalApplicants = x.LegalCaseApplicantLawyers.Count(),
                    TypeSearch = typeSearch,
                    IsDirected = x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED
                })
                .ToListAsync();

            return result;

        }
        public async Task<LegalCaseToLawyerCustomModel> GetLegalCaseToLawyerCustomModel(Guid legalCaseId, Guid lawyerId)
        {
            var query = _context.LegalCases
                .Where(x => x.Id == legalCaseId && x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS)
                .AsQueryable();

            var max_vacancies_value = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_VACANCIES).Select(x => x.Value).FirstOrDefaultAsync();
            var max_vacancies = 0;

            if (string.IsNullOrEmpty(max_vacancies_value))
            {
                max_vacancies = Convert.ToInt32(ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_VACANCIES]);
            }
            else
            {
                max_vacancies = Convert.ToInt32(max_vacancies_value);
            }

            var result = await query
                .Select(x => new LegalCaseToLawyerCustomModel
                {
                    Id = x.Id,
                    ValidatedAt = x.ValidationDate.ToLocalDateFormat(),
                    Department = x.Province.Department.Name,
                    Province = x.Province.Name,
                    Description = x.Description,
                    //SpecialityTheme = x.SpecialityTheme.ColloquialName,
                    //Speciality = x.SpecialityTheme.Speciality.ColloquialName,
                    Status = x.Status,
                    TotalVacancies = max_vacancies,
                    TotalApplicants = x.LegalCaseApplicantLawyers.Count(),
                    PublicatedDays = (int)(DateTime.UtcNow.Date - x.ValidationDate.Value.Date).TotalDays,
                    Client = new ENTITIES.Custom.ClientCustomModel
                    {
                        Fullname = $"{x.Client.User.Name} {x.Client.User.Surnames}",
                        Department = x.Client.User.District.Province.Department.Name,
                        Province = x.Client.User.District.Province.Name,
                        Email = x.Client.User.Email,
                        Id = x.Id,
                        PhoneNumber = x.Client.User.PhoneNumber
                    },
                    LawyerApplicants = x.LegalCaseApplicantLawyers.Select(y => new ENTITIES.Custom.LawyerCustomModel
                    {
                        FullName = $"{y.Lawyer.User.Name} {y.Lawyer.User.Surnames}",
                        PostulationDate = y.ApplicationDate.ToLocalDateTimeFormat(),
                        Id = y.LawyerId,
                        You = y.LawyerId == lawyerId
                    }).ToList(),
                    Lawyers = x.LegalCaseLawyers.Select(y => new ENTITIES.Custom.LawyerCustomModel
                    {
                        FullName = $"{y.Lawyer.User.Name} {y.Lawyer.User.Surnames}",
                        PostulationDate = y.CreatedAt.ToLocalDateTimeFormat(),
                        Id = y.LawyerId,
                        You = y.LawyerId == lawyerId
                    }).ToList(),
                    Questions = x.LegalCaseResponses.Select(y => new LegalCaseQuestionCustomModel
                    {
                        Question = y.LegalCaseQuestion.Description,
                        Response = y.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER && !result.LawyerApplicants.Any(y => y.Id == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.SEARCH_LEGALCASES;
            }
            else if (result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER && result.LawyerApplicants.Any(y => y.Id == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.INCOMING_REQUESTS;
            }
            else if (result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS && result.Lawyers.Any(z => z.Id == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.LEGAL_CASE_IN_COURSE;
            }
            else if ((result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT || result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER || result.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER) && result.LawyerApplicants.Any(y => y.Id == lawyerId))
            {
                result.SearchType = ConstantHelpers.ENTITIES.LEGAL_CASE.SEARCH_TYPE.POSTULATED_CASES;
            }

            return result;
        }
        public async Task<List<LegalCaseApplicantLawyer>> GetLegalCaseApplicantLawyers(Guid legalCaseId)
        {
            var result = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId &&
            (x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.PENDING || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.DIRECTED)
            )
                .Select(x => new LegalCaseApplicantLawyer
                {
                    ApplicationDate = x.ApplicationDate,
                    LegalCaseId = x.LegalCaseId,
                    ResponseTime = x.ResponseTime,
                    LawyerId = x.LawyerId,
                    Lawyer = new Lawyer
                    {
                        HiredCases = x.Lawyer.LegalCaseLawyers.Count(),
                        User = new ApplicationUser
                        {
                            Id = x.Lawyer.UserId,
                            Surnames = x.Lawyer.User.Surnames,
                            Email = x.Lawyer.User.Email,
                            Name = x.Lawyer.User.Name,
                            LastConnection = x.Lawyer.User.LastConnection,
                            CreatedAt = x.Lawyer.User.CreatedAt,
                            Picture = x.Lawyer.User.Picture
                        },
                        Id = x.Lawyer.Id,
                        Fee = x.Lawyer.Fee,
                        AboutMe = x.Lawyer.AboutMe,
                        FreeFirst = x.Lawyer.FreeFirst,
                        LawyerSpecialityThemes = x.Lawyer.LawyerSpecialityThemes.Select(x => new LawyerSpecialityTheme
                        {
                            LawyerId = x.LawyerId,
                            SpecialityThemeId = x.SpecialityThemeId,
                            SpecialityTheme = new SpecialityTheme
                            {
                                ColloquialName = x.SpecialityTheme.ColloquialName,
                                OfficialName = x.SpecialityTheme.OfficialName,
                                Speciality = new Speciality
                                {
                                    ColloquialName = x.SpecialityTheme.Speciality.ColloquialName,
                                    OfficialName = x.SpecialityTheme.Speciality.OfficialName
                                }
                            }
                        }).ToList()
                    }
                })
                .ToListAsync();

            return result;
        }
        public async Task<ResultCustomModel> AcceptApplicant(LegalCaseLawyer entity)
        {
            var result = new ResultCustomModel();
            var legalCase = await _context.LegalCases.Where(x => x.Id == entity.LegalCaseId).FirstOrDefaultAsync();
            var isApplicant = await _context.LegalCaseApplicantLawyers.AnyAsync(x => x.LawyerId == entity.LawyerId && x.LegalCaseId == entity.LegalCaseId);
            if (!isApplicant)
            {
                result.Success = false;
                result.Message = "No se encontró la postulacion del abogado";
                return result;
            }

            var applicant = await _context.LegalCaseApplicantLawyers.Where(x => x.LawyerId == entity.LawyerId && x.LegalCaseId == entity.LegalCaseId).FirstOrDefaultAsync();

            var lawyers = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == entity.LegalCaseId).ToListAsync();

            if (lawyers.Any(y => y.LawyerId == entity.LawyerId))
            {
                result.Success = false;
                result.Message = "El abogado ya ha sido aceptado para este caso.";
                return result;
            }

            if (lawyers.Any() && !lawyers.All(y => y.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED))
            {
                result.Success = false;
                result.Message = "El caso ya tiene un abogado principal asignado.";
                return result;
            }

            var maxHourTimeToAcceptAndPayLawyer = Convert.ToInt32((await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER)).Value);

            var maxDateTimeToAccept = legalCase.SelectionLawyerStartDate.Value.AddHours(maxHourTimeToAcceptAndPayLawyer);

            if (DateTime.UtcNow > maxDateTimeToAccept)
            {
                result.Success = false;
                result.Message = "El tiempo para aceptar postulaciones a finalizado.";
                return result;
            }

            var lawyerFee = await _context.Lawyers.Where(x => x.Id == entity.LawyerId).Select(x => x.Fee).FirstOrDefaultAsync();

            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT;
            entity.ResponseTime = applicant.ResponseTime;
            entity.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PENDING;
            entity.Fee = lawyerFee;
            applicant.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.ACCEPTED;
            await _context.LegalCaseLawyers.AddAsync(entity);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALUES[legalCase.Status];
            return result;
        }
        public async Task<ResultCustomModel> RemoveLawyer(Guid legalCaseId, Guid lawyerId)
        {
            var result = new ResultCustomModel();
            var applicant = await _context.LegalCaseApplicantLawyers.Where(x => x.LawyerId == lawyerId && x.LegalCaseId == legalCaseId).FirstOrDefaultAsync();
            var legalCaseLawyer = await _context.LegalCaseLawyers.Where(x => x.LawyerId == lawyerId && x.LegalCaseId == legalCaseId).FirstOrDefaultAsync();
            applicant.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.PENDING;
            _context.LegalCaseLawyers.Remove(legalCaseLawyer);
            await _context.SaveChangesAsync();

            result.Message = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALUES[ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT];

            if (!await _context.LegalCaseLawyers.AnyAsync(y => y.LegalCaseId == legalCaseId))
            {
                var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER;
                result.Message = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALUES[ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER];
                await _context.SaveChangesAsync();
            }

            result.Success = true;
            return result;
        }
        public async Task<ResultCustomModel> AcceptCase(LegalCaseLawyer entity)
        {
            var lawyerFee = await _context.Lawyers.Where(x => x.Id == entity.LawyerId).Select(x => x.Fee).FirstOrDefaultAsync();
            entity.Fee = lawyerFee;
            await _context.LegalCaseLawyers.AddAsync(entity);
            var legalCase = await _context.LegalCases.Where(x => x.Id == entity.LegalCaseId).FirstOrDefaultAsync();
            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT;

            var applicant = await _context.LegalCaseApplicantLawyers.Where(x => x.LawyerId == entity.LawyerId && x.LegalCaseId == entity.LegalCaseId).FirstOrDefaultAsync();
            applicant.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.ACCPETED_DIRECTED;

            await _context.SaveChangesAsync();
            var model = new ResultCustomModel
            {
                Message = "",
                Success = true
            };

            return model;
        }

        public async Task<ResultCustomModel> FiledLegalCase(Guid lawyerId, Guid legalCaseId)
        {
            var result = new ResultCustomModel();

            try
            {

                var maxTime = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_FILED_LEGAL_CASE).FirstOrDefaultAsync();

                if (maxTime == null)
                {
                    maxTime = new Configuration
                    {
                        Key = ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_FILED_LEGAL_CASE,
                        Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_FILED_LEGAL_CASE]
                    };

                }

                var maxTimeInt = Convert.ToInt32(maxTime.Value);

                var entity = new LegalCaseFiledLawyer
                {
                    CreatedAt = DateTime.UtcNow,
                    LawyerId = lawyerId,
                    LegalCaseId = legalCaseId,
                    EndDateTimeAt = DateTime.UtcNow.AddHours(maxTimeInt)
                };

                await _context.LegalCaseFiledLawyers.AddAsync(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error al guardar el caso legal.";
            }

            return result;
        }
        public async Task<DateTimeCustomModel> GetRemainingCustomDateTimeToSelectLawyers(Guid legalCaseId)
        {
            var configurationAcceptAndPay = await GetConfigurationByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER);
            var maxHours = Convert.ToInt32(configurationAcceptAndPay.Value);

            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();

            if (!legalCase.DerivedDate.HasValue)
                return null;

            var finishDateTime = legalCase.SelectionLawyerStartDate.ToDefaultTimeZone().Value.AddHours(maxHours);

            var model = new DateTimeCustomModel
            {
                Year = finishDateTime.Year,
                Hour = finishDateTime.Hour,
                Day = finishDateTime.Day,
                Month = finishDateTime.Month,
                Minutes = finishDateTime.Minute,
                Seconds = finishDateTime.Second
            };

            return model;
        }
        public async Task<List<SpecialityTheme>> GetSpecialityThemeByLegalCaseId(Guid legalCaseId)
        {
            var result = await _context.LegalCaseSpecialityThemes.Where(x => x.LegalCaseId == legalCaseId)
                .Select(x => new SpecialityTheme
                {
                    Id = x.SpecialityThemeId,
                    ColloquialName = x.SpecialityTheme.ColloquialName,
                    OfficialName = x.SpecialityTheme.OfficialName,
                    Speciality = new Speciality
                    {
                        Id = x.SpecialityTheme.Speciality.Id,
                        ColloquialName = x.SpecialityTheme.Speciality.ColloquialName,
                        OfficialName = x.SpecialityTheme.Speciality.OfficialName
                    }
                })
                .ToListAsync();

            return result;
        }

        public async Task UpdateLegalCaseSpecialityThemes(Guid legalCaseId, List<Guid> specialityThemesId)
        {
            var legalCaseSpecialityThemes = await _context.LegalCaseSpecialityThemes.Where(x => x.LegalCaseId == legalCaseId).ToListAsync();
            _context.LegalCaseSpecialityThemes.RemoveRange(legalCaseSpecialityThemes);
            var entities = specialityThemesId
                .Select(x => new LegalCaseSpecialityTheme
                {
                    LegalCaseId = legalCaseId,
                    SpecialityThemeId = x
                })
                .ToList();

            await _context.LegalCaseSpecialityThemes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        private async Task<Configuration> GetConfigurationByKey(string key)
        {
            var configuration = await _context.Configurations.Where(x => x.Key == key).FirstOrDefaultAsync();
            if (configuration == null)
            {
                configuration = new Configuration
                {
                    Key = key,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[key]
                };

                await _context.AddAsync(configuration);
                await _context.SaveChangesAsync();
            }

            return configuration;
        }
    }
}
