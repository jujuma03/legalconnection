using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Structs;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class LawyerRepository : Repository<Lawyer>, ILawyerRepository
    {
        public LawyerRepository(LegalConnectionContext context) : base(context) { }

        public async Task<Lawyer> GetByUserId(string userId)
            => await _context.Lawyers.Where(x => x.UserId == userId).FirstOrDefaultAsync();

        public async Task<bool> AnyByCAL(string cal, Guid ignoredId)
            => await _context.Lawyers.AnyAsync(x => x.Id != ignoredId && x.CAL.ToLower().Trim() == cal.ToLower().Trim());
        public async Task<List<LawyerTemp>> GetAllValidated()
        {
            var queryClient = await _context.Lawyers
                .Where(x => x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new LawyerTemp
                {
                    Id = x.Id,
                    Name = x.User.Name,
                    Surnames = x.User.Surnames,
                    Department = x.User.District.Province.Department.Name,
                    District = x.User.District.Name,
                    AboutLawyer = x.AboutMe,
                    Price = x.Fee,
                    LastConnection = DateTime.Now.ToLocalDateFormat(),
                    Cases = x.LegalCaseApplicantLawyers.Count(),
                    RegisteredAt = x.User.CreatedAt.ToLocalDateFormat(),
                    UrlImage = x.User.Picture ?? @"..\images\general\profile.jpg"
                })
                .ToListAsync();
            var specialties = await _context.LawyerSpecialityThemes
               .Include(x => x.SpecialityTheme.Speciality)
               .ToListAsync();
            var data = queryClient?
               .Select(x => new LawyerTemp
               {
                   Id = x.Id,
                   Name = x.Name,
                   Surnames = x.Surnames,
                   Specialties = specialties != null ? specialties.Where(y => y.LawyerId == x.Id)
                        .GroupBy(y => y.SpecialityTheme.Speciality)
                        .Select(y => new LawyerSpecialtiesThemesTemp
                        {
                            Name = y.Key.ColloquialName,
                        })
                        .ToList()
                    : null,
                   Themes = specialties != null ? specialties.Where(y => y.LawyerId == x.Id)
                   .Select(y => new LawyerSpecialtiesThemesTemp
                   {
                       Name = y.SpecialityTheme.ColloquialName,
                   }).ToList()
                   : null,
                   Department = x.Department,
                   District = x.District,
                   Price = x.Price,
                   LastConnection = x.LastConnection,
                   Cases = x.Cases,
                   RegisteredAt = x.RegisteredAt,
                   UrlImage = x.UrlImage
               })
               .ToList();
            return data;
        }
        public async Task<List<LawyerTemp>> GetAllToDirectory()
        {
            var queryClient = await _context.Lawyers
                .Where(x => x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED && x.PublicProfile)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new LawyerTemp
                {
                    Id = x.Id,
                    Name = x.User.Name,
                    Surnames = x.User.Surnames,
                    Department = x.User.District.Province.Department.Name,
                    District = x.User.District.Name,
                    AboutLawyer = x.AboutMe,
                    Price = x.Fee,
                    LastConnection = DateTime.Now.ToLocalDateFormat(),
                    Cases = x.LegalCaseLawyers.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED).Count(),
                    RegisteredAt = x.User.CreatedAt.ToLocalDateFormat(),
                    UrlImage = x.User.Picture ?? @"..\images\general\profile.jpg"
                })
                .ToListAsync();
            var specialties = await _context.LawyerSpecialityThemes
               .Include(x => x.SpecialityTheme.Speciality)
               .ToListAsync();
            var data = queryClient?
               .Select(x => new LawyerTemp
               {
                   Id = x.Id,
                   Name = x.Name,
                   Surnames = x.Surnames,
                   Specialties = specialties != null ? specialties.Where(y => y.LawyerId == x.Id)
                        .GroupBy(y => y.SpecialityTheme.Speciality)
                        .Select(y => new LawyerSpecialtiesThemesTemp
                        {
                            Name = y.Key.ColloquialName,
                        })
                        .ToList()
                    : null,
                   Themes = specialties != null ? specialties.Where(y => y.LawyerId == x.Id)
                   .Select(y => new LawyerSpecialtiesThemesTemp
                   {
                       Name = y.SpecialityTheme.ColloquialName,
                   }).ToList()
                   : null,
                   Department = x.Department,
                   District = x.District,
                   Price = x.Price,
                   LastConnection = x.LastConnection,
                   Cases = x.Cases,
                   RegisteredAt = x.RegisteredAt,
                   UrlImage = x.UrlImage
               })
               .ToList();
            return data;
        }
        public async Task<DataTablesStructs.ReturnedData<object>> GetLawyersDatatable(DataTablesStructs.SentParameters sentParameters, byte status, string search, bool? onlyNewLawyer = null)
        {
            var query = _context.Lawyers
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking();

            if (onlyNewLawyer.HasValue)
            {
                if (onlyNewLawyer.Value)
                {
                    query = query.Where(x => x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.INTERVIEW_VALIDATED || x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.IN_EVALUATION || x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.PROFILE_VALIDATED);
                }
            }
            else
            {
                if (status != 0) query = query.Where(x => x.Status == status);
            }

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.User.FullName.ToUpper().Contains(search.ToUpper()));

            var recordsTotal = await query.CountAsync();

            var data = await query
                    .Select(x => new LC.ENTITIES.Custom.Admin.LawyerCustomModel
                    {
                        Id = x.Id,
                        Name = x.User.Name,
                        Surnames = x.User.Surnames,
                        FullName = x.User.FullName,
                        Code = x.Code,
                        IsPublic = x.PublicProfile ? "Público" : "Privado",
                        Ubigeo = x.User.District != null ? $"{x.User.District.Province.Department.Name} - {x.User.District.Province.Name} - {x.User.District.Name}" : "",
                        //specialties = string.Join(", ", x.LawyerSpecialityThemes.Select(y => $"{y.SpecialityTheme.Speciality.OfficialName}")) ?? "",
                        SpecialitiesList = x.LawyerSpecialityThemes.Select(y=>y.SpecialityTheme.Speciality.OfficialName).Distinct().ToList(),
                        RegisterDate = x.CreatedAt.ToLocalDateTimeFormat(),
                        ValidationDate = x.ValidationDate.ToLocalDateTimeFormat(),
                        LegalCasesFinalized = x.LegalCaseLawyers.Where(x => x.LegalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED).Count(),
                        LegalCasesReceived = x.LegalCaseLawyers.Count(),
                        LegalCasesRejected = x.LegalCaseLawyers.Where(x => x.LegalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.REJECTED_BY_LAWYER).Count(),
                        LegalCasesApplied = x.LegalCaseApplicantLawyers.Count(),
                        Status = x.Status != 0 ? ConstantHelpers.ENTITIES.LAWYER.STATUS.VALUES[x.Status] : "-",
                        PaymentDate = x.LawyerPlanDetail.TempStartDate.ToLocalDateFormat() ?? "",
                        Plan = x.LawyerPlanDetail.Plan.Name ?? ""
                    })
                    .Skip(sentParameters.PagingFirstRecord)
                    .Take(sentParameters.RecordsPerDraw)
                    .ToListAsync();

            foreach (var item in data)
            {
                item.Specialties = string.Join(", ",item.SpecialitiesList.Distinct().ToList());
            }

            var recordsFiltered = data.Count;

            var result = new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
            };
            return result;
        }
        public async Task<PaginationStructs.ReturnedData<LawyerTemp>> GetLawyersDirectoryData(PaginationStructs.SentParameters sentParameters, string userId, Guid sp, Guid d, Guid prv, Guid t, decimal min, decimal max, string stars)
        {
            var query = _context.Lawyers
                .Where(x => x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED && x.PublicProfile)
                .Include(x => x.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(sentParameters.SearchValue))
                query = query.Where(x => x.User.Name.ToUpper().Contains(sentParameters.SearchValue.ToUpper()) || x.User.Surnames.ToUpper().Contains(sentParameters.SearchValue.ToUpper()));

            if (sp != Guid.Empty)
            {
                var lawyers = await _context.LawyerSpecialityThemes.Where(x => x.SpecialityTheme.SpecialityId == sp).Select(x => x.LawyerId).ToListAsync();
                query = query.Where(x => lawyers.Contains(x.Id));
            }
            if (d != Guid.Empty)
            {
                query = query.Where(x => x.User.District.Province.DepartmentId == d);
            }
            if (prv != Guid.Empty)
            {
                query = query.Where(x => x.User.District.ProvinceId == prv);
            }
            if (t != Guid.Empty)
            {
                //query = query.Where(x => );
            }
            if (min > -1)
            {
                query = query.Where(x => x.Fee >= min);
            }
            if (max > -1)
            {
                query = query.Where(x => x.Fee <= max);
            }
            if (!string.IsNullOrEmpty(stars))
            {
                var splited = stars.Split(",").ToList();
                var items = await query.Select(x => new { qual = x.LawyerQualifications.Average(y => y.Qualification), id = x.Id }).ToListAsync();
                var ids = items.Where(x => splited.Contains(Math.Round(x.qual).ToString())).Select(x => x.id).ToList();
                query = query.Where(x => ids.Contains(x.Id));
            }

            var recordsTotal = await query.CountAsync();

            var specialties = await _context.LawyerSpecialityThemes
                .Include(x => x.SpecialityTheme.Speciality)
                .ToListAsync();

            var queryClient = await query
                .Select(x => new LawyerTemp
                {
                    Id = x.Id,
                    Name = x.User.Name,
                    Surnames = x.User.Surnames,
                    Department = x.User.District.Province.Department.Name,
                    District = x.User.District.Name,
                    Price = x.Fee,
                    LastConnection = x.User.LastConnection.ToLocalDateFormat(),
                    Cases = x.LegalCaseLawyers.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED).Count(),
                    RegisteredAt = x.User.CreatedAt.ToLocalDateFormat(),
                    UrlImage = x.User.Picture,
                    Qualification = x.LawyerQualifications.Average(y => y.Qualification),
                    AboutLawyer = x.AboutMe,
                    IsFreeFee = x.FreeFirst && x.LegalCaseLawyers.Where(y => y.LegalCase.Client.UserId == userId).Count() == 0,
                    HasPlans = x.LawyerPlanDetail != null
                })
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .ToListAsync();

            var data = queryClient?
                .Select(x => new LawyerTemp
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surnames = x.Surnames,
                    Specialties = specialties != null ? specialties.Where(y => y.LawyerId == x.Id)
                        .GroupBy(y => y.SpecialityTheme.Speciality)
                        .Select(y => new LawyerSpecialtiesThemesTemp
                        {
                            Name = y.Key.ColloquialName,
                        })
                        .ToList()
                    : null,
                    Themes = specialties != null ? specialties.Where(y => y.LawyerId == x.Id)
                        .Select(y => new LawyerSpecialtiesThemesTemp
                        {
                            Name = y.SpecialityTheme.ColloquialName,
                        }).ToList()
                    : null,
                    Qualification = x.Qualification,
                    Department = x.Department,
                    District = x.District,
                    Price = x.Price,
                    LastConnection = x.LastConnection,
                    Cases = x.Cases,
                    RegisteredAt = x.RegisteredAt,
                    UrlImage = x.UrlImage,
                    AboutLawyer = x.AboutLawyer,
                    HasPlans = x.HasPlans,
                    IsFreeFee=x.IsFreeFee
                })
                .ToList();

            return new PaginationStructs.ReturnedData<LawyerTemp>
            {
                Data = data,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = sentParameters.Page,
                    RecordsPerDraw = sentParameters.RecordsPerDraw
                }
            };
        }
        public async Task<EconomicManagementLawyerCustomModel> GetEconomicManagementLawyerCustomModel(Guid lawyerId)
        {
            var legalCases = await _context.LegalCases
                .Where(x => x.LegalCaseLawyers.Any(y => y.LawyerId == lawyerId) || x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId))
                .Include(x => x.LegalCaseApplicantLawyers)
                .Include(x => x.LegalCaseLawyers)
                .ToListAsync();

            var model = new EconomicManagementLawyerCustomModel
            {
                Applications = legalCases.Count(),
                Finalized = legalCases.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED && x.LegalCaseLawyers.Any(y => y.LawyerId == lawyerId)).Count(),
                InCourse = legalCases.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS && x.LegalCaseLawyers.Any(y => y.LawyerId == lawyerId)).Count(),
                LawyerId = lawyerId,
                PendingDirected = legalCases.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId)).Count()
            };

            return model;
        }
        public async Task<decimal> GetAvailableBalance(Guid lawyerId)
        {
            var payments = await _context.Payments.Where(x => x.LawyerId == lawyerId && x.LegalCase.LegalCaseLawyers.All(y => y.LawyerId == lawyerId && y.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED)).Select(x => x.LawyerAmount).SumAsync();
            var withdrawalRequestDone = await _context.LawyerWithdrawals.Where(x => x.LawyerId == lawyerId).Select(x => x.Amount).SumAsync();
            var withdrawalRequestPending = await _context.WithdrawalRequests.Where(x => x.LawyerId == lawyerId && x.Status == ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.IN_PROCESS).Select(x => x.Amount).SumAsync();
            var withdrawalRequestDenied = await _context.WithdrawalRequests.Where(x => x.LawyerId == lawyerId && x.Status == ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.DENIED).Select(x => x.Amount).SumAsync();
            var result = payments - withdrawalRequestDone - withdrawalRequestPending + withdrawalRequestDenied;
            return result;
        }

        public async Task<decimal> GetInProcessBalance(Guid lawyerId)
        {
            var withdrawalRequestPending = await _context.WithdrawalRequests.Where(x => x.LawyerId == lawyerId && x.Status == ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.IN_PROCESS).Select(x => x.Amount).SumAsync();
            return withdrawalRequestPending;
        }
        public async Task<decimal> GetPossibleAvailableBalance(Guid lawyerId)
        {
            var possibleCasesPayment = await _context.LegalCaseLawyers.Where(x => x.LawyerId == lawyerId && x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED).Select(x => x.Fee).SumAsync();
            //var possibleCasesPayment = await _context.LegalCaseLawyers.Where(x => x.LawyerId == lawyerId && x.LegalCase.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED).Select(x=>x.Fee).SumAsync();
            return (1M - ConstantHelpers.PROFIT_PERCENTAGE_PER_LEGALCASE / 100) * possibleCasesPayment;
        }
        public async Task<object> GetBalance(Guid lawyerId)
        {
            var withdrawalRequestDone = await _context.LawyerWithdrawals.Where(x => x.LawyerId == lawyerId && x.WithdrawalRequest.Status == ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.DEPOSIT_DONE).Select(x => x.Amount).SumAsync();
            var cases = await _context.LegalCaseLawyers.Where(x => x.LawyerId == lawyerId).ToListAsync();

            var ipc = cases.Where(x => x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED).ToList();
            var fc = cases.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED).ToList();

            return new
            {
                inProgessCases = ipc.Count(),
                finishedCases = fc.Count(),
                inprogressBalanace = (1M - ConstantHelpers.PROFIT_PERCENTAGE_PER_LEGALCASE / 100) * ipc.Select(x=>x.Fee).Sum(),
                finishedBalance = (1M - ConstantHelpers.PROFIT_PERCENTAGE_PER_LEGALCASE / 100) * fc.Select(x => x.Fee).Sum(),
                total = ((1M - ConstantHelpers.PROFIT_PERCENTAGE_PER_LEGALCASE / 100) * cases.Sum(x=>x.Fee)) - withdrawalRequestDone
            };
        }
        public async Task<int> GetHiredLegalCases(Guid lawyerId)
        {
            var lawyerCases = await _context.LegalCaseLawyers
                .Where(x => x.LawyerId == lawyerId && (x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PAYMENT_MADE))
                .CountAsync();

            return lawyerCases;
        }
        public async Task<IEnumerable<Lawyer>> GetLawyerToDerivatedLegalCase(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            if (legalCase.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL)
            {
                var themes = await _context.LegalCaseSpecialityThemes.Where(x => x.LegalCaseId == legalCaseId).Select(y => y.SpecialityThemeId).ToListAsync();
                var lawyers = await _context.Lawyers.Include(x => x.User).Where(x => x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED && x.User.District.ProvinceId == legalCase.ProvinceId && x.LawyerSpecialityThemes.Any(y => themes.Contains(y.SpecialityThemeId))).ToArrayAsync();
                return lawyers;
            }
            else
            {
                var lawyers = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCase.Id).Include(x => x.Lawyer.User).Select(x => x.Lawyer).ToListAsync();
                return lawyers;
            }
        }
        public async Task<InboxCustomModel> GetInboxDetail(Guid lawyerId)
        {
            var model = new InboxCustomModel();

            var configuration = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE).FirstOrDefaultAsync();
            if (configuration is null)
            {
                configuration = new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE],
                };
            }
            var provinceId = await _context.Lawyers.Where(x => x.Id == lawyerId).Select(x => x.User.District.ProvinceId).FirstOrDefaultAsync();

            var maxHours = Convert.ToInt32(configuration.Value);
            var specialityThemes = await _context.LawyerSpecialityThemes.Where(x => x.LawyerId == lawyerId).Select(x => x.SpecialityThemeId).ToListAsync();
            var query = _context.LegalCases
                //.Where(x =>
                //x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATION_PROCCESS &&
                //x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALIDATED &&
                //specialityThemes.Contains(x.SpecialityThemeId))
                .AsNoTracking();

            model.Search = await query.Where(x => x.ProvinceId == provinceId && x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER && x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.NORMAL && !x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId) && x.DerivedDate.Value.AddHours(maxHours) > DateTime.UtcNow).CountAsync();
            model.IncomingRequest = await query.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.AWAITING_CONFIRMATION_FROM_LAWYER && x.Type == ConstantHelpers.ENTITIES.LEGAL_CASE.TYPE.DIRECTED && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId)).CountAsync();
            model.InCourse = await query.Where(x => x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS && x.LegalCaseLawyers.Any(y => y.LawyerId == lawyerId)).CountAsync();
            model.Postulated = await query.Where(x => (x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER || x.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.LOOKING_LAWYER) && x.LegalCaseApplicantLawyers.Any(y => y.LawyerId == lawyerId)).CountAsync();
            model.Archived = await _context.LegalCaseFiledLawyers.Where(x => x.LawyerId == lawyerId && x.EndDateTimeAt >= DateTime.UtcNow).Select(x => x.LegalCase).CountAsync();
            return model;
        }
    }
}
