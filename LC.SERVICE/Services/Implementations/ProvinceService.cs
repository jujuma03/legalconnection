using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public async Task<Province> Get(Guid id)
            => await _provinceRepository.Get(id);

        public async Task<Select2Structs.ResponseParameters> GetProvinceSelect2(Select2Structs.RequestParameters parameters, Guid departmentId, string searchValue)
            => await _provinceRepository.GetProvinceSelect2(parameters, departmentId, searchValue);

        public async Task<IEnumerable<Select2Structs.Result>> GetProvinceSelect2ClientSide(Guid departmentId,Guid? selectedId =null)
            => await _provinceRepository.GetProvinceSelect2ClientSide(departmentId, selectedId);
    }
}
