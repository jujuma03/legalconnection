using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IFrequentQuestionService
    {
        Task<List<FrequentQuestion>> GetAllActive(byte type);
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline, byte status);
        Task Insert(FrequentQuestion frequent);
        Task<FrequentQuestion> Get(Guid id);
        Task Update(FrequentQuestion frequent);
        Task Delete(FrequentQuestion frequent);
    }
}
