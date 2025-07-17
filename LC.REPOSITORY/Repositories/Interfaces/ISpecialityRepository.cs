using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ISpecialityRepository : IRepository<Speciality>
    {
        Task<Select2Structs.ResponseParameters> GetSpecialitiesSelect2(Select2Structs.RequestParameters parameters, string searchValue, bool colloquialName);
        Task<DataTablesStructs.ReturnedData<object>> GetSpecialitiesDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue);
        Task<IEnumerable<Speciality>> GetAll();
        Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null);
        Task<bool> AnyByCode(string code, Guid? ignoredId);
        Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null);
        Task<bool> HasSpecialityThemes(Guid id);
    }
}
