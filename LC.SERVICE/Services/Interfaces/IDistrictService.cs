using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IDistrictService
    {
        IQueryable<District> GetAsQueryable();
        Task<IEnumerable<Select2Structs.Result>> GetDistrictsSelect2ClientSide(Guid pid);
        Task<Select2Structs.ResponseParameters> GetDistrictsSelect2(Select2Structs.RequestParameters parameters, Guid provinceId, string searchValue);
    }
}
