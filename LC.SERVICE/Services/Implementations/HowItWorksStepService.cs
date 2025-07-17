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
    public class HowItWorksStepService : IHowItWorksStepService
    {
        private readonly IHowItWorksStepRepository _howItWorksStepRepository;
        public HowItWorksStepService(IHowItWorksStepRepository howItWorksStepRepository)
        {
            _howItWorksStepRepository = howItWorksStepRepository;
        }

        public async Task Delete(HowItWorksStep model)
        {
            await _howItWorksStepRepository.Delete(model);
        }

        public async Task<HowItWorksStep> Get(Guid id)
        {
            return await _howItWorksStepRepository.Get(id);
        }

        public async Task<List<HowItWorksStep>> GetAllActive(byte type = 0)
        {
            return await _howItWorksStepRepository.GetAllActive(type);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null)
        {
            return await _howItWorksStepRepository.GetAllDatatable(sentParameters, headline, status);
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder(byte type)
        {
            return await _howItWorksStepRepository.GetAvailableOrdersAndListSequenceOrder(type);
        }

        public async Task Insert(HowItWorksStep model)
        {
            await _howItWorksStepRepository.Insert(model);
        }

        public async Task Update(HowItWorksStep entity)
        {
            await _howItWorksStepRepository.Update(entity);
        }
    }
}
