using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IProvinceService 
    {
        Task<IEnumerable<Select2Structs.Result>> GetProvinceSelect2ClientSide(Guid departmentId,Guid? selectedId=null);
        Task<Select2Structs.ResponseParameters> GetProvinceSelect2(Select2Structs.RequestParameters parameters, Guid departmentId, string searchValue);
        Task<Province> Get(Guid id);
    }
}
