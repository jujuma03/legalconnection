using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Custom.General;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.REPOSITORY.Repositories.Template;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LegalCaseService : ILegalCaseService
    {
        private readonly ILegalCaseRepository _legalCaseRepository;

        public LegalCaseService(ILegalCaseRepository legalCaseRepository)
        {
            _legalCaseRepository = legalCaseRepository;
        }

        public async Task<ResultCustomModel> AcceptApplicant(LegalCaseLawyer entity)
            => await _legalCaseRepository.AcceptApplicant(entity);

        public async Task<ResultCustomModel> AcceptCase(LegalCaseLawyer entity)
            => await _legalCaseRepository.AcceptCase(entity);

        public async Task<bool> ClientHasAccess(Guid clientId, Guid legalCaseId)
            => await _legalCaseRepository.ClientHasAccess(clientId, legalCaseId);

        public async Task<ResultCustomModel> CloseLegalCase(Guid legalCaseId, Guid lawyerId)
            => await _legalCaseRepository.CloseLegalCase(legalCaseId, lawyerId);
        public async Task Delete(LegalCase entity)
            => await _legalCaseRepository.Delete(entity);

        public async Task<ResultCustomModel> DeleteLegalCase(Guid clientId, Guid legalCaseId)
            => await _legalCaseRepository.DeleteLegalCase(clientId, legalCaseId);

        public async Task<ResultCustomModel> FiledLegalCase(Guid lawyerId, Guid legalCaseId)
            => await _legalCaseRepository.FiledLegalCase(lawyerId, legalCaseId);

        public async Task<LegalCase> Get(Guid id)
            => await _legalCaseRepository.Get(id);

        public async Task<List<LegalCaseApplicantLawyer>> GetLegalCaseApplicantLawyers(Guid legalCaseId)
            => await _legalCaseRepository.GetLegalCaseApplicantLawyers(legalCaseId);

        public async Task<int> GetLegalCaseCountByType(byte type)
            => await _legalCaseRepository.GetLegalCaseCountByType(type);

        public async Task<ENTITIES.Custom.LegalCaseCustomModel> GetLegalCaseCustomModel(Guid legalCaseId)
            => await _legalCaseRepository.GetLegalCaseCustomModel(legalCaseId);

        public async Task<List<LegalCaseItemCustomModel>> GetLegalCaseItemsCustomModel(Guid lawyerId, byte typeSearch)
            => await _legalCaseRepository.GetLegalCaseItemsCustomModel(lawyerId, typeSearch);

        public async Task<PaginationStructs.ReturnedData<ENTITIES.Custom.Lawyer.LegalCaseCustomModel>> GetLegalCaseItemsToLawyer(PaginationStructs.SentParameters parameters, Guid lawyerId, byte typeSearch)
            => await _legalCaseRepository.GetLegalCaseItemsToLawyer(parameters, lawyerId, typeSearch);

        public async Task<List<LegalCaseLawyer>> GetLegalCaseLawyers(Guid legalCaseId)
            => await _legalCaseRepository.GetLegalCaseLawyers(legalCaseId);
        public async Task<List<ENTITIES.Custom.LegalCaseCustomModel>> GetLegalCasesCustomModel(ClaimsPrincipal user, byte? status)
            => await _legalCaseRepository.GetLegalCasesCustomModel(user, status);

        public async Task<DataTablesStructs.ReturnedData<object>> GetLegalCasesDatatable(DataTablesStructs.SentParameters parameters, byte? status,byte? type, DateTime? dateStart = null, DateTime? dateEnd = null, string search = null)
            => await _legalCaseRepository.GetLegalCasesDatatable(parameters, status,type,dateStart, dateEnd,search);
        public async Task<List<LegalCaseExcelTemplate>> GetLegalCasesExport(byte? status, byte? type = null, DateTime? dateStart = null, DateTime? dateEnd = null, string search = null)
            => await _legalCaseRepository.GetLegalCasesExport(status, type, dateStart, dateEnd, search);
        public async Task<PaginationStructs.ReturnedData<ENTITIES.Custom.Lawyer.LegalCaseCustomModel>> GetLegalCasesFinishedToLawyer(PaginationStructs.SentParameters parameters, Guid lawyerId)
            => await _legalCaseRepository.GetLegalCasesFinishedToLawyer(parameters, lawyerId);

        public async Task<PaginationStructs.ReturnedData<ENTITIES.Custom.Client.LegalCaseCustomModel>> GetLegalCasesItemsToClient(PaginationStructs.SentParameters sentParameters, Guid clientId)
            => await _legalCaseRepository.GetLegalCasesItemsToClient(sentParameters, clientId);

        public async Task<int> GetLegalCasesPaymentByClient(Guid lawyerId, Guid clientId)
            => await _legalCaseRepository.GetLegalCasesPaymentByClient(lawyerId, clientId);

        public async Task<ENTITIES.Custom.Admin.LegalCaseCustomModel> GetLegalCaseToAdmin(Guid legalCaseId)
            => await _legalCaseRepository.GetLegalCaseToAdmin(legalCaseId);

        public async Task<ENTITIES.Custom.Client.LegalCaseCustomModel> GetLegalCaseToClient(Guid legalCaseId)
            => await _legalCaseRepository.GetLegalCaseToClient(legalCaseId);

        public async Task<ENTITIES.Custom.Lawyer.LegalCaseCustomModel> GetLegalCaseToLawyer(Guid legalCaseId, Guid lawyerId)
            => await _legalCaseRepository.GetLegalCaseToLawyer(legalCaseId, lawyerId);

        public async Task<LegalCaseToLawyerCustomModel> GetLegalCaseToLawyerCustomModel(Guid legalCaseId, Guid lawyerId)
            => await _legalCaseRepository.GetLegalCaseToLawyerCustomModel(legalCaseId, lawyerId);

        public async Task<DateTimeCustomModel> GetRemainingCustomDateTimeToSelectLawyers(Guid legalCaseId)
            => await _legalCaseRepository.GetRemainingCustomDateTimeToSelectLawyers(legalCaseId);

        public async Task<List<SpecialityTheme>> GetSpecialityThemeByLegalCaseId(Guid legalCaseId)
            => await _legalCaseRepository.GetSpecialityThemeByLegalCaseId(legalCaseId);

        public async Task Insert(LegalCase entity)
            => await _legalCaseRepository.Insert(entity);

        public async Task<bool> LawyerHasAccess(Guid lawyerId, Guid legalCaseId)
            => await _legalCaseRepository.LawyerHasAccess(lawyerId, legalCaseId);

        public async Task<bool> LegalCaseApplicantsCompleted(Guid legalCaseId)
            => await _legalCaseRepository.LegalCaseApplicantsCompleted(legalCaseId);

        public async Task<ResultCustomModel> Postulate(LegalCaseApplicantLawyer entity)
            => await _legalCaseRepository.Postulate(entity);

        public async Task<ResultCustomModel> RejectDirectedLegalCase(Guid lawyerId, Guid legalCaseId)
            => await _legalCaseRepository.RejectDirectedLegalCase(lawyerId, legalCaseId);

        public async Task<ResultCustomModel> RemoveLawyer(Guid legalCaseId, Guid lawyerId)
            => await _legalCaseRepository.RemoveLawyer(legalCaseId, lawyerId);

        public async Task Update(LegalCase entity)
            => await _legalCaseRepository.Update(entity);

        public async Task UpdateLegalCaseSpecialityThemes(Guid legalCaseId, List<Guid> specialityThemesId)
            => await _legalCaseRepository.UpdateLegalCaseSpecialityThemes(legalCaseId, specialityThemesId);
    }
}
