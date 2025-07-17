using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerStudyService : ILawyerStudyService
    {
        private readonly ILawyerStudyRepository _lawyerStudyRepository;

        public LawyerStudyService(ILawyerStudyRepository lawyerStudyRepository)
        {
            _lawyerStudyRepository = lawyerStudyRepository;
        }

        public async Task<bool> AnyByLawyer(Guid lawyerId)
            => await _lawyerStudyRepository.AnyByLawyer(lawyerId);

        public async Task Delete(LawyerStudy entity)
            => await _lawyerStudyRepository.Delete(entity);

        public async Task<LawyerStudy> Get(Guid id)
            => await _lawyerStudyRepository.Get(id);

        public async Task<string> GetFeaturedStudy(Guid lawyerId)
            => await _lawyerStudyRepository.GetFeaturedStudy(lawyerId);

        public async Task<IEnumerable<LawyerStudy>> GetLawyerStudiesByLawyerId(Guid lawyerId)
            => await _lawyerStudyRepository.GetLawyerStudiesByLawyerId(lawyerId);

        public async Task Insert(LawyerStudy entity)
            => await _lawyerStudyRepository.Insert(entity);


        public async Task Update(LawyerStudy entity)
            => await _lawyerStudyRepository.Update(entity);
    }
}
