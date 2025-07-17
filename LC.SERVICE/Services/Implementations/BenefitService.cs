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
    public class BenefitService : IBenefitService
    {
        private readonly IBenefitRepository _benefitRepository;

        public BenefitService(IBenefitRepository benefitRepository)
        {
            _benefitRepository = benefitRepository;
        }

        public async Task Delete(Benefit entity)
            => await _benefitRepository.Delete(entity);

        public async Task<Benefit> Get(Guid id)
            => await _benefitRepository.Get(id);

        public async Task<List<Benefit>> GetAll()
            => await _benefitRepository.GetAll();

        public async Task<DataTablesStructs.ReturnedData<object>> GetBenefitsDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue)
            => await _benefitRepository.GetBenefitsDatatable(sentParameters, searchValue);

        public async Task Insert(Benefit entity)
            => await _benefitRepository.Insert(entity);

        public async Task Update(Benefit entity)
            => await _benefitRepository.Update(entity);
    }
}
