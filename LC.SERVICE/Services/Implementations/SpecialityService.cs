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
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;

        public SpecialityService(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }

        public async Task<bool> AnyByCode(string code, Guid? ignoredId = null)
        {
            return await _specialityRepository.AnyByCode(code, ignoredId);
        }

        public async Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null)
            => await _specialityRepository.AnyByColloquialName(name, ignoredId);

        public async Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null)
            => await _specialityRepository.AnyByOfficialName(name, ignoredId);

        public async Task Delete(Speciality entity)
            => await _specialityRepository.Delete(entity);

        public async Task<Speciality> Get(Guid id)
            => await _specialityRepository.Get(id);

        public async Task<IEnumerable<Speciality>> GetAll()
            => await _specialityRepository.GetAll();

        public async Task<DataTablesStructs.ReturnedData<object>> GetSpecialitiesDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue)
            => await _specialityRepository.GetSpecialitiesDatatable(sentParameters, searchValue);

        public async Task<Select2Structs.ResponseParameters> GetSpecialitiesSelect2(Select2Structs.RequestParameters parameters, string searchValue, bool colloquialName)
            => await _specialityRepository.GetSpecialitiesSelect2(parameters, searchValue, colloquialName);

        public async Task<bool> HasSpecialityThemes(Guid id)
            => await _specialityRepository.HasSpecialityThemes(id);

        public async Task Insert(Speciality entity)
            => await _specialityRepository.Insert(entity);

        public async Task Update(Speciality entity)
            => await _specialityRepository.Update(entity);
    }
}
