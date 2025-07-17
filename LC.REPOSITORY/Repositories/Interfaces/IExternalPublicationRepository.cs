using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IExternalPublicationRepository : IRepository<ExternalPublication>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetExternalPublicationDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
    }
}
