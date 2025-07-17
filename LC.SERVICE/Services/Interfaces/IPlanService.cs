using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IPlanService
    {
        Task Insert(Plan plan);
        Task<Plan> Get(string Id);
        Task Update(Plan plan);
        Task<bool> AnyLawyerByPlan(string id);
        Task Delete(Plan plan);
        Task<bool> ExistFreePlan();
        Task<Plan> GetFreePlan();
        Task<List<PlanBenefit>> GetPlanBenefits(string planId);
        Task<List<Plan>> GetAll();
        Task UpdatePlanBenefits(List<PlanBenefit> entities, string planId);
        Task<DataTablesStructs.ReturnedData<object>> GetPlansDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue);
    }
}
