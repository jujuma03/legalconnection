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
    public class SpecialityThemeService : ISpecialityThemeService
    {
        private readonly ISpecialityThemeRepository _specialityThemeRepository;

        public SpecialityThemeService(ISpecialityThemeRepository specialityThemeRepository)
        {
            _specialityThemeRepository = specialityThemeRepository;
        }

        public async Task<bool> AnyByCode(string code, Guid? ignoredId = null)
        {
            return await _specialityThemeRepository.AnyByCode(code, ignoredId);
        }

        public async Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null)
            => await _specialityThemeRepository.AnyByColloquialName(name, ignoredId);

        public async Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null)
            => await _specialityThemeRepository.AnyByOfficialName(name, ignoredId);

        public async Task Delete(SpecialityTheme entity)
            => await _specialityThemeRepository.Delete(entity);

        public async Task<SpecialityTheme> Get(Guid id)
            => await _specialityThemeRepository.Get(id);

        public async Task<List<SpecialityTheme>> GetSpecialityThemesBySpecialitiesId(List<Guid> specialitiesId)
        {
            return await _specialityThemeRepository.GetSpecialityThemesBySpecialitiesId(specialitiesId);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetSpecialityThemesDatatable(DataTablesStructs.SentParameters sentParameters, Guid? specialityId ,string searchValue)
            => await _specialityThemeRepository.GetSpecialityThemesDatatable(sentParameters, specialityId,searchValue);

        public async Task<Select2Structs.ResponseParameters> GetSpecialityThemesSelect2(Select2Structs.RequestParameters parameters, string searchValue, Guid? specialityId, List<Guid> specialitiesId, bool colloquialName)
            => await _specialityThemeRepository.GetSpecialityThemesSelect2(parameters, searchValue, specialityId, specialitiesId, colloquialName);

        public async Task<bool> HasLawyerAssigned(Guid id)
            => await _specialityThemeRepository.HasLawyerAssigned(id);
    
        public async Task Insert(SpecialityTheme entity)
            => await _specialityThemeRepository.Insert(entity);

        public async Task Update(SpecialityTheme entity)
            => await _specialityThemeRepository.Update(entity);
    }
}
