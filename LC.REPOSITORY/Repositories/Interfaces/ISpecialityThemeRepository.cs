using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ISpecialityThemeRepository: IRepository<SpecialityTheme>
    {
        Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null);
        Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null);
        Task<Select2Structs.ResponseParameters> GetSpecialityThemesSelect2(Select2Structs.RequestParameters parameters, string searchValue, Guid? specialityId, List<Guid> specialitiesId, bool colloquialName);
        Task<DataTablesStructs.ReturnedData<object>> GetSpecialityThemesDatatable(DataTablesStructs.SentParameters sentParameters, Guid? specialityId ,string searchValue);
        Task<bool> AnyByCode(string code, Guid? ignoredId);
        Task<List<SpecialityTheme>> GetSpecialityThemesBySpecialitiesId(List<Guid> specialitiesId);
        Task<bool> HasLawyerAssigned(Guid id);
    }
}
