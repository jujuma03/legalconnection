using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ISpecialityThemeService
    {
        Task Insert(SpecialityTheme entity);
        Task<SpecialityTheme> Get(Guid id);
        Task Delete(SpecialityTheme id);
        Task Update(SpecialityTheme entity);
        Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null);
        Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null);
        Task<Select2Structs.ResponseParameters> GetSpecialityThemesSelect2(Select2Structs.RequestParameters parameters, string searchValue, Guid? specialityId, List<Guid> specialitiesId, bool colloquialName);
        Task<DataTablesStructs.ReturnedData<object>> GetSpecialityThemesDatatable(DataTablesStructs.SentParameters sentParameters, Guid? specialityId ,string searchValue);
        Task<List<SpecialityTheme>> GetSpecialityThemesBySpecialitiesId(List<Guid> specialitiesId);
        Task<bool> HasLawyerAssigned(Guid id);
        Task<bool> AnyByCode(string code, Guid? ignoredId = null);
    }
}
