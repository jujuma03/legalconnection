using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerService
    {
        Task<Lawyer> GetByUserId(string id);
        Task<bool> AnyByCAL(string cal, Guid ignoredId);
        Task Update(Lawyer entity);
        Task Insert(Lawyer entity);
        Task<Lawyer> Get(Guid id);
        Task<DataTablesStructs.ReturnedData<object>> GetLawyersDatatable(DataTablesStructs.SentParameters sentparameters, byte status, string search, bool? onlyNewLawyer = null);
        Task<PaginationStructs.ReturnedData<LawyerTemp>> GetLawyersDirectoryData(PaginationStructs.SentParameters sentParameters,string userid, Guid sp, Guid d,Guid prv, Guid t, decimal min, decimal max, string stars);
        Task<EconomicManagementLawyerCustomModel> GetEconomicManagementLawyerCustomModel(Guid lawyerId);
        Task<decimal> GetAvailableBalance(Guid lawyerId);
        Task<decimal> GetInProcessBalance(Guid lawyerId);
        Task<int> GetHiredLegalCases(Guid lawyerId);
        Task<decimal> GetPossibleAvailableBalance(Guid lawyerId);
        Task<object> GetBalance(Guid lawyerId);
        Task<InboxCustomModel> GetInboxDetail(Guid lawyerId);
        Task<IEnumerable<Lawyer>> GetLawyerToDerivatedLegalCase(Guid legalCaseId);
        Task<List<LawyerTemp>> GetAllValidated();
        Task<List<LawyerTemp>> GetAllToDirectory();
    }
}
