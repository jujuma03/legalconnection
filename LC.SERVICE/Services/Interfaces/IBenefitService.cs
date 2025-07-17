using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IBenefitService
    {
        Task<Benefit> Get(Guid id);
        Task<List<Benefit>> GetAll();
        Task Update(Benefit entity);
        Task Insert(Benefit entity);
        Task Delete(Benefit entity);
        Task<DataTablesStructs.ReturnedData<object>> GetBenefitsDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue);
    }
}
