using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerExperienceService : ILawyerExperienceService
    {
        private readonly ILawyerExperienceRepository _lawyerExperienceRepository;

        public LawyerExperienceService(ILawyerExperienceRepository lawyerExperienceRepository)
        {
            _lawyerExperienceRepository = lawyerExperienceRepository;
        }

        public async Task<bool> AnyByLawyer(Guid lawyerId)
            => await _lawyerExperienceRepository.AnyByLawyer(lawyerId);

        public async Task Delete(LawyerExperience entity)
            => await _lawyerExperienceRepository.Delete(entity);

        public async Task<LawyerExperience> Get(Guid id)
            => await _lawyerExperienceRepository.Get(id);

        public async Task<IEnumerable<LawyerExperience>> GetLawyerExperiencesByLawyerId(Guid lawyerId)
            => await _lawyerExperienceRepository.GetLawyerExperiencesByLawyerId(lawyerId);

        public async Task<string> GetTotalExperienceByLawyerId(Guid lawyerId)
            => await _lawyerExperienceRepository.GetTotalExperienceByLawyerId(lawyerId);

        public async Task Insert(LawyerExperience entity)
            => await _lawyerExperienceRepository.Insert(entity);

        public async Task Update(LawyerExperience entity)
            => await _lawyerExperienceRepository.Update(entity);
    }
}
