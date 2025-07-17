using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IHowItWorksStepRepository:IRepository<HowItWorksStep>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null);
        Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder(byte type);
        Task<List<HowItWorksStep>> GetAllActive(byte type = 0);
    }
}
