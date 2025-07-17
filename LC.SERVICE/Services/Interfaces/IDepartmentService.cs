using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Select2Structs.Result>> GetDepartmentsSelect2ClientSide(string q,Guid? selectedId=null);
        Task<Select2Structs.ResponseParameters> GetDepartmentSelect2(Select2Structs.RequestParameters parameters, string searchValue);
        Task<IEnumerable<Select2Structs.Result>> GetUsedDepartmentsSelect2ClientSide(Guid? selectedId = null);
        IQueryable<Department> GetAsQueryable();
        Task<Department> Get(Guid id);
    }
}
