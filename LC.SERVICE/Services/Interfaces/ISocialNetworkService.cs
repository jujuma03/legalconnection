using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ISocialNetworkService
    {
        Task<List<SocialNetwork>> GetAllActive();
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, byte type, byte status);
        Task<SocialNetwork> Get(Guid id);
        Task Update(SocialNetwork social);
        Task Delete(SocialNetwork social);
        Task Insert(SocialNetwork sectionItem);
    }
}
