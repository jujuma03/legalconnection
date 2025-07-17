using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IExternalPublicationService
    {
        Task Insert(ExternalPublication entity);
        Task Update(ExternalPublication entity);
        Task Delete(ExternalPublication entity);
        Task<ExternalPublication> Get(Guid id);
        Task<DataTablesStructs.ReturnedData<object>> GetExternalPublicationDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
    }
}
