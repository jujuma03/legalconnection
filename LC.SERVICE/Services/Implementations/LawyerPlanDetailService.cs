using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerPlanDetailService : ILawyerPlanDetailService
    {
        private readonly ILawyerPlanDetailRepository _lawyerPlanDetailRepository;

        public LawyerPlanDetailService(ILawyerPlanDetailRepository lawyerPlanDetailRepository)
        {
            _lawyerPlanDetailRepository = lawyerPlanDetailRepository;
        }

        public async Task<LawyerPlanDetail> Get(Guid id)
            => await _lawyerPlanDetailRepository.Get(id);

        public async Task Insert(LawyerPlanDetail entity)
            => await _lawyerPlanDetailRepository.Insert(entity);

        public async Task Update(LawyerPlanDetail entity)
            => await _lawyerPlanDetailRepository.Update(entity);
    }
}
