using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerWithdrawalInfoService
    {
        Task Insert(LawyerWithdrawalInfo entity);
        Task Update(LawyerWithdrawalInfo entity);
        Task<LawyerWithdrawalInfo> Get(Guid id);
    }
}
