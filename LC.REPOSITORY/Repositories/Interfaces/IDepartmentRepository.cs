using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Select2Structs.Result>> GetDepartmentsSelect2ClientSide(string q,Guid? selectedId = null);
        Task<Select2Structs.ResponseParameters> GetDepartmentSelect2(Select2Structs.RequestParameters parameters, string searchValue);
        Task<IEnumerable<Select2Structs.Result>> GetUsedDepartmentsSelect2ClientSide(Guid? selectedId = null);
    }
}
