using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerLanguageService : ILawyerLanguageService
    {
        private readonly ILawyerLanguageRepository _lawyerLanguageRepository;

        public LawyerLanguageService(ILawyerLanguageRepository lawyerLanguageRepository)
        {
            _lawyerLanguageRepository = lawyerLanguageRepository;
        }

        public async Task<bool> AnyByLawyerId(Guid lawyerId, Guid languageId)
            => await _lawyerLanguageRepository.AnyByLawyerId(lawyerId, languageId);

        public async Task Delete(LawyerLanguage entity)
            => await _lawyerLanguageRepository.Delete(entity);

        public async Task<LawyerLanguage> Get(Guid id)
            => await _lawyerLanguageRepository.Get(id);

        public async Task<IEnumerable<LawyerLanguage>> GetLanguagesByLawyerId(Guid lawyerId)
            => await _lawyerLanguageRepository.GetLanguagesByLawyerId(lawyerId);

        public async Task Insert(LawyerLanguage entity)
            => await _lawyerLanguageRepository.Insert(entity);

        public async Task Update(LawyerLanguage entity)
            => await _lawyerLanguageRepository.Update(entity);
    }
}
