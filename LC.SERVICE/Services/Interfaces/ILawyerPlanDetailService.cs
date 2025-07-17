using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerPlanDetailService
    {
        Task<LawyerPlanDetail> Get(Guid id);
        Task Update(LawyerPlanDetail entity);
        Task Insert(LawyerPlanDetail entity);
    }
}
