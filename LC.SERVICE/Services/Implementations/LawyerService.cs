using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerService : ILawyerService
    {
        private readonly ILawyerRepository _lawyerRepository;

        public LawyerService(ILawyerRepository lawyerRepository)
        {
            _lawyerRepository = lawyerRepository;
        }

        public async Task<Lawyer> Get(Guid id)
            => await _lawyerRepository.Get(id);

        public async Task<decimal> GetAvailableBalance(Guid lawyerId)
            => await _lawyerRepository.GetAvailableBalance(lawyerId);

        public async Task<decimal> GetInProcessBalance(Guid lawyerId)
          => await _lawyerRepository.GetInProcessBalance(lawyerId);

        public async Task<Lawyer> GetByUserId(string id)
            => await _lawyerRepository.GetByUserId(id);

        public async Task<EconomicManagementLawyerCustomModel> GetEconomicManagementLawyerCustomModel(Guid lawyerId)
        {
            return await _lawyerRepository.GetEconomicManagementLawyerCustomModel(lawyerId);
        }

        public async Task<int> GetHiredLegalCases(Guid lawyerId)
            => await _lawyerRepository.GetHiredLegalCases(lawyerId);

        public async Task<InboxCustomModel> GetInboxDetail(Guid lawyerId)
            => await _lawyerRepository.GetInboxDetail(lawyerId);

        public async Task<IEnumerable<Lawyer>> GetLawyerToDerivatedLegalCase(Guid legalCaseId)
            => await _lawyerRepository.GetLawyerToDerivatedLegalCase(legalCaseId);

        public async Task<DataTablesStructs.ReturnedData<object>> GetLawyersDatatable(DataTablesStructs.SentParameters sentparameters, byte status, string search, bool? onlyNewLawyer = null)
        {
            return await _lawyerRepository.GetLawyersDatatable(sentparameters,status,search, onlyNewLawyer);
        }

        public async Task<PaginationStructs.ReturnedData<LawyerTemp>> GetLawyersDirectoryData(PaginationStructs.SentParameters sentParameters,string userid, Guid sp, Guid d,Guid prv, Guid t, decimal min, decimal max, string stars)
        {
            return await _lawyerRepository.GetLawyersDirectoryData(sentParameters,userid,sp, d,prv,  t, min, max,stars);
        }

        public async Task<decimal> GetPossibleAvailableBalance(Guid lawyerId)
            => await _lawyerRepository.GetPossibleAvailableBalance(lawyerId);

        public async Task Insert(Lawyer entity)
            => await _lawyerRepository.Insert(entity);

        public async Task Update(Lawyer entity)
            => await _lawyerRepository.Update(entity);

        public async Task<List<LawyerTemp>> GetAllValidated()
        {
            return await _lawyerRepository.GetAllValidated();
        }
        public async Task<List<LawyerTemp>> GetAllToDirectory()
            => await _lawyerRepository.GetAllToDirectory();
        public async Task<bool> AnyByCAL(string cal, Guid ignoredId)
            => await _lawyerRepository.AnyByCAL(cal, ignoredId);

        public async Task<object> GetBalance(Guid lawyerId)
        {
            return await _lawyerRepository.GetBalance(lawyerId);
        }
    }
}
