using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        public DistrictService(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public IQueryable<District> GetAsQueryable()
            => _districtRepository.GetAsQueryable();

        public async Task<Select2Structs.ResponseParameters> GetDistrictsSelect2(Select2Structs.RequestParameters parameters, Guid provinceId, string searchValue)
            => await _districtRepository.GetDistrictsSelect2(parameters, provinceId, searchValue);

        public async Task<IEnumerable<Select2Structs.Result>> GetDistrictsSelect2ClientSide(Guid pid)
            => await _districtRepository.GetDistrictsSelect2ClientSide(pid);
    }
}
