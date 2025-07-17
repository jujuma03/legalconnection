using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerRepository : IRepository<Lawyer>
    {
        Task<Lawyer> GetByUserId(string id);
        Task<DataTablesStructs.ReturnedData<object>> GetLawyersDatatable(DataTablesStructs.SentParameters sentparameters, byte status, string search, bool? onlyNewLawyer = null);
        Task<PaginationStructs.ReturnedData<LawyerTemp>> GetLawyersDirectoryData(PaginationStructs.SentParameters sentParameters,string userid, Guid sp, Guid d,Guid prv, Guid t, decimal min, decimal max, string stars);
        Task<EconomicManagementLawyerCustomModel> GetEconomicManagementLawyerCustomModel(Guid lawyerId);
        Task<decimal> GetAvailableBalance(Guid lawyerId);
        Task<decimal> GetInProcessBalance(Guid lawyerId);
        Task<int> GetHiredLegalCases(Guid lawyerId);
        Task<decimal> GetPossibleAvailableBalance(Guid lawyerId);
        Task<InboxCustomModel> GetInboxDetail(Guid lawyerId);
        Task<IEnumerable<Lawyer>> GetLawyerToDerivatedLegalCase(Guid legalCaseId);
        Task<List<LawyerTemp>> GetAllValidated();
        Task<List<LawyerTemp>> GetAllToDirectory();
        Task<bool> AnyByCAL(string cal, Guid ignoredId);
        Task<object> GetBalance(Guid lawyerId);
    }
}
