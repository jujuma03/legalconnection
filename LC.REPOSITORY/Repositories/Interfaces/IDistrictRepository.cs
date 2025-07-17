using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<IEnumerable<Select2Structs.Result>> GetDistrictsSelect2ClientSide(Guid pid);
        Task<Select2Structs.ResponseParameters> GetDistrictsSelect2(Select2Structs.RequestParameters parameters, Guid provinceId, string searchValue);
    }
}
