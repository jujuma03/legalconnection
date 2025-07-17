using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerWithdrawalRequestService
    {
        Task Insert(LawyerWithdrawalRequest entity);
        Task<LawyerWithdrawalRequest> Get(Guid id);
        Task Update(LawyerWithdrawalRequest entity);
        Task<DataTablesStructs.ReturnedData<object>> GetWithdrawalRequestDatatable(DataTablesStructs.SentParameters sentParameters, byte? status, Guid? lawyerId = null);
        Task<PaginationStructs.ReturnedData<WithdrawalRequestCustomModel>> GetWithdrawalRequest(PaginationStructs.SentParameters parameters, byte? status, Guid? lawyerId = null);
        Task InsertLawyerWithdrawals(LawyerWithdrawal entity);
    }
}
