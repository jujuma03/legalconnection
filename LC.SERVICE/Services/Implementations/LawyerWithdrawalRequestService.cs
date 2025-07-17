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
    public class LawyerWithdrawalRequestService : ILawyerWithdrawalRequestService
    {
        private readonly ILawyerWithdrawalRequestRepository _withdrawalRequestRepository;

        public LawyerWithdrawalRequestService(ILawyerWithdrawalRequestRepository withdrawalRequestRepository)
        {
            _withdrawalRequestRepository = withdrawalRequestRepository;
        }

        public async Task<LawyerWithdrawalRequest> Get(Guid id)
            => await _withdrawalRequestRepository.Get(id);

        public async Task<PaginationStructs.ReturnedData<WithdrawalRequestCustomModel>> GetWithdrawalRequest(PaginationStructs.SentParameters parameters, byte? status, Guid? lawyerId = null)
        {
            return await _withdrawalRequestRepository.GetWithdrawalRequest(parameters, status, lawyerId);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetWithdrawalRequestDatatable(DataTablesStructs.SentParameters sentParameters, byte? status, Guid? lawyerId = null)
            => await _withdrawalRequestRepository.GetWithdrawalRequestDatatable(sentParameters, status, lawyerId);

        public async Task Insert(LawyerWithdrawalRequest entity)
            => await _withdrawalRequestRepository.Insert(entity);

        public async Task InsertLawyerWithdrawals(LawyerWithdrawal entity)
        {
            await _withdrawalRequestRepository.InsertLawyerWithdrawals(entity);
        }

        public async Task Update(LawyerWithdrawalRequest entity)
            => await _withdrawalRequestRepository.Update(entity);
    }
}
