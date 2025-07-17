using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IBenefitRepository : IRepository<Benefit>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetBenefitsDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue);
        Task<List<Benefit>> GetAll();
    }
}
