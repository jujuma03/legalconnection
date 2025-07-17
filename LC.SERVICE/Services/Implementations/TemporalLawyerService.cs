using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class TemporalLawyerService : ITemporalLawyerService
    {
        private readonly ITemporalLawyerRepository _temporalLawyerRepository;

        public TemporalLawyerService(ITemporalLawyerRepository temporalLawyerRepository)
        {
            _temporalLawyerRepository = temporalLawyerRepository;
        }

        public async Task AcceptProfileChanges(Guid lawyerId)
            => await _temporalLawyerRepository.AcceptProfileChanges(lawyerId);

        public async Task<TemporalLawyer> GetTemporalLawyer(Guid lawyerId)
            => await _temporalLawyerRepository.GetTemporalLawyer(lawyerId);

        public async Task<TemporalLawyerExperience> GetTemporalLawyerExperience(Guid lawyerExperienceId)
            => await _temporalLawyerRepository.GetTemporalLawyerExperience(lawyerExperienceId);

        public async Task<TemporalLawyerLanguage> GetTemporalLawyerLanguage(Guid lawyerLanguageId)
            => await _temporalLawyerRepository.GetTemporalLawyerLanguage(lawyerLanguageId);

        public async Task<TemporalLawyerStudy> GetTemporalLawyerStudy(Guid lawyerStudyId)
            => await _temporalLawyerRepository.GetTemporalLawyerStudy(lawyerStudyId);

        public async Task RejectProfileChanges(Guid lawyerId)
            => await _temporalLawyerRepository.RejectProfileChanges(lawyerId);

        public async Task SaveTemporalLawyer(TemporalLawyer entity)
            => await _temporalLawyerRepository.SaveTemporalLawyer(entity);
    }
}
