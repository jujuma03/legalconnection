using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task Insert(Plan plan)
            => await _planRepository.Insert(plan);

        public async Task<Plan> Get(string Id)
            => await _planRepository.Get(Id);

        public async Task Update(Plan plan)
            => await _planRepository.Update(plan);

        public async Task Delete(Plan plan)
            => await _planRepository.Delete(plan);

        public async Task<DataTablesStructs.ReturnedData<object>> GetPlansDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue)
            => await _planRepository.GetPlansDatatable(sentParameters, searchValue);

        public async Task<bool> AnyLawyerByPlan(string id)
            => await _planRepository.AnyLawyerByPlan(id);

        public async Task<bool> ExistFreePlan()
            => await _planRepository.ExistFreePlan();

        public async Task<List<PlanBenefit>> GetPlanBenefits(string planId)
            => await _planRepository.GetPlanBenefits(planId);

        public async Task UpdatePlanBenefits(List<PlanBenefit> entities, string planId)
            => await _planRepository.UpdatePlanBenefits(entities, planId);

        public async Task<List<Plan>> GetAll()
            => await _planRepository.GetAll();

        public async Task<Plan> GetFreePlan()
            => await _planRepository.GetFreePlan();
    }
}
