using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetPlansDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue);
        Task<bool> AnyLawyerByPlan(string id);
        Task<bool> ExistFreePlan();
        Task<List<PlanBenefit>> GetPlanBenefits(string planId);
        Task UpdatePlanBenefits(List<PlanBenefit> entities, string planId);
        Task<List<Plan>> GetAll();
        Task<Plan> GetFreePlan();
    }
}
