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
    public class FrequentQuestionService : IFrequentQuestionService
    {
        private readonly IFrequentQuestionRepository _frequentQuestionRepository;
        public FrequentQuestionService(IFrequentQuestionRepository frequentQuestionRepository)
        {
            _frequentQuestionRepository=frequentQuestionRepository;
        }

        public async Task Delete(FrequentQuestion frequent)
        {
            await _frequentQuestionRepository.Delete(frequent);
        }

        public async Task<FrequentQuestion> Get(Guid id)
        {
            return await _frequentQuestionRepository.Get(id);
        }

        public async Task<List<FrequentQuestion>> GetAllActive(byte type)
        {
            return await _frequentQuestionRepository.GetAllActive(type);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline, byte status)
        {
            return await _frequentQuestionRepository.GetAllDatatable(sentParameters, headline, status);
        }

        public async Task Insert(FrequentQuestion frequent)
        {
            await _frequentQuestionRepository.Insert(frequent);
        }

        public async Task Update(FrequentQuestion frequent)
        {
            await _frequentQuestionRepository.Update(frequent);
        }
    }
}
