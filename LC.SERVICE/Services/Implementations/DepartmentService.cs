using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Get(Guid id)
            => await _departmentRepository.Get(id);

        public IQueryable<Department> GetAsQueryable()
            => _departmentRepository.GetAsQueryable();

        public async Task<Select2Structs.ResponseParameters> GetDepartmentSelect2(Select2Structs.RequestParameters parameters, string searchValue)
            => await _departmentRepository.GetDepartmentSelect2(parameters, searchValue);

        public async Task<IEnumerable<Select2Structs.Result>> GetDepartmentsSelect2ClientSide(string q,Guid? selectedId = null)
            => await _departmentRepository.GetDepartmentsSelect2ClientSide(q,selectedId);

        public async Task<IEnumerable<Select2Structs.Result>> GetUsedDepartmentsSelect2ClientSide(Guid? selectedId = null)
            => await _departmentRepository.GetUsedDepartmentsSelect2ClientSide(selectedId);
    }
}
