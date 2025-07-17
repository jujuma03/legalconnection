using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerSpecialityThemeService : ILawyerSpecialityThemeService
    {
        private readonly ILawyerSpecialityThemeRepository _lawyerSpecialityRepository;

        public LawyerSpecialityThemeService(ILawyerSpecialityThemeRepository lawyerSpecialityRepository)
        {
            _lawyerSpecialityRepository = lawyerSpecialityRepository;
        }

        public async Task<bool> AnyByLawyer(Guid lawyerId)
            => await _lawyerSpecialityRepository.AnyByLawyer(lawyerId);

        public async Task DeleteRange(IEnumerable<LawyerSpecialityTheme> entities)
            => await _lawyerSpecialityRepository.DeleteRange(entities);

        public async Task<LawyerSpecialityTheme> Get(Guid id)
            => await _lawyerSpecialityRepository.Get(id);

        public async Task<IEnumerable<LawyerSpecialityTheme>> GetSpecialitiesByLawyer(Guid lawyerId)
            => await _lawyerSpecialityRepository.GetSpecialitiesByLawyer(lawyerId);

        public async Task InsertRange(IEnumerable<LawyerSpecialityTheme> entities)
            => await _lawyerSpecialityRepository.InsertRange(entities);

        public async Task<ResultCustomModel> ValidateSpecialityTheme(List<Guid> specialityThemeIds)
            => await _lawyerSpecialityRepository.ValidateSpecialityTheme(specialityThemeIds);
    }
}
