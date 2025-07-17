using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Custom.General;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Template;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILegalCaseRepository : IRepository<LegalCase>
    {
        Task<PaginationStructs.ReturnedData<ENTITIES.Custom.Lawyer.LegalCaseCustomModel>> GetLegalCasesFinishedToLawyer(PaginationStructs.SentParameters parameters, Guid lawyerId);
        Task<PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Client.LegalCaseCustomModel>> GetLegalCasesItemsToClient(PaginationStructs.SentParameters sentParameters, Guid clientId);
        Task<LC.ENTITIES.Custom.Client.LegalCaseCustomModel> GetLegalCaseToClient(Guid legalCaseId);
        Task<bool> ClientHasAccess(Guid clientId, Guid legalCaseId);
        Task<LC.ENTITIES.Custom.Admin.LegalCaseCustomModel> GetLegalCaseToAdmin(Guid legalCaseId);
        Task<LC.ENTITIES.Custom.Lawyer.LegalCaseCustomModel> GetLegalCaseToLawyer(Guid legalCaseId, Guid lawyerId);
        Task<bool> LawyerHasAccess(Guid lawyerId, Guid legalCaseId);
        Task<PaginationStructs.ReturnedData<LC.ENTITIES.Custom.Lawyer.LegalCaseCustomModel>> GetLegalCaseItemsToLawyer(PaginationStructs.SentParameters parameters, Guid lawyerId, byte typeSearch);
        Task<int> GetLegalCasesPaymentByClient(Guid lawyerId, Guid clientId);
        Task<ResultCustomModel> Postulate(LegalCaseApplicantLawyer entity);
        Task<List<LegalCaseCustomModel>> GetLegalCasesCustomModel(ClaimsPrincipal user, byte? status);
        Task<DataTablesStructs.ReturnedData<object>> GetLegalCasesDatatable(DataTablesStructs.SentParameters parameters, byte? status,byte? type, DateTime? dateStart = null, DateTime? dateEnd = null, string search = null);
        Task<List<LegalCaseExcelTemplate>> GetLegalCasesExport(byte? status, byte? type = null, DateTime? dateStart = null, DateTime? dateEnd = null, string search = null);
        Task<LegalCaseCustomModel> GetLegalCaseCustomModel(Guid legalCaseId);
        Task<List<LegalCaseItemCustomModel>> GetLegalCaseItemsCustomModel(Guid lawyerId, byte typeSearch);
        Task<LegalCaseToLawyerCustomModel> GetLegalCaseToLawyerCustomModel(Guid legalCaseId, Guid lawyerId);
        Task<List<LegalCaseApplicantLawyer>> GetLegalCaseApplicantLawyers(Guid legalCaseId);
        Task<List<LegalCaseLawyer>> GetLegalCaseLawyers(Guid legalCaseId);
        Task<ResultCustomModel> AcceptApplicant(LegalCaseLawyer entity);
        Task<ResultCustomModel> RemoveLawyer(Guid legalCaseId, Guid lawyerId);
        Task<ResultCustomModel> AcceptCase(LegalCaseLawyer entity);
        Task<ResultCustomModel> CloseLegalCase(Guid legalCaseId, Guid lawyerId);
        Task<ResultCustomModel> FiledLegalCase(Guid lawyerId, Guid legalCaseId);
        Task<DateTimeCustomModel> GetRemainingCustomDateTimeToSelectLawyers(Guid legalCaseId);
        Task<List<SpecialityTheme>> GetSpecialityThemeByLegalCaseId(Guid legalCaseId);
        Task UpdateLegalCaseSpecialityThemes(Guid legalCaseId, List<Guid> specialityThemesId);
        Task<int> GetLegalCaseCountByType(byte type);
        Task<ResultCustomModel> RejectDirectedLegalCase(Guid lawyerId, Guid legalCaseId);
        Task<ResultCustomModel> DeleteLegalCase(Guid clientId, Guid legalCaseId);
        Task<bool> LegalCaseApplicantsCompleted(Guid legalCaseId);
    }
}
