using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ISpecialityService
    {
        Task<Select2Structs.ResponseParameters> GetSpecialitiesSelect2(Select2Structs.RequestParameters parameters, string searchValue, bool colloquialName);
        Task<DataTablesStructs.ReturnedData<object>> GetSpecialitiesDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue);
        Task Insert(Speciality entity);
        Task Delete(Speciality entity);
        Task Update(Speciality entity);
        Task<Speciality> Get(Guid id);
        Task<IEnumerable<Speciality>> GetAll();
        Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null);
        Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null);
        Task<bool> AnyByCode(string code, Guid? ignoredId = null);
        Task<bool> HasSpecialityThemes(Guid id);
    }
}
