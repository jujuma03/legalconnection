using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IHowItWorksStepService
    {
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null);
        Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder(byte type);
        Task Insert(HowItWorksStep model);
        Task<HowItWorksStep> Get(Guid id);
        Task Update(HowItWorksStep entity);
        Task<List<HowItWorksStep>> GetAllActive(byte type = 0);
        Task Delete(HowItWorksStep banner);
    }
}
