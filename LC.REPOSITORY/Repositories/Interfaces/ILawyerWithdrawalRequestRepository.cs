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
    public interface ILawyerWithdrawalRequestRepository : IRepository<LawyerWithdrawalRequest>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetWithdrawalRequestDatatable(DataTablesStructs.SentParameters sentParameters, byte? status, Guid? lawyerId = null);
        Task<PaginationStructs.ReturnedData<WithdrawalRequestCustomModel>> GetWithdrawalRequest(PaginationStructs.SentParameters sentParameters, byte? status, Guid? lawyerId = null);
        Task InsertLawyerWithdrawals(LawyerWithdrawal entity);
    }
}
