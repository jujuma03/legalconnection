using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ISocialNetworkRepository : IRepository<SocialNetwork>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, byte type, byte status);
        Task<List<SocialNetwork>> GetAllActive();
    }
}
