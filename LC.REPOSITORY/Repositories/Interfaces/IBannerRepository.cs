using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IBannerRepository:IRepository<Banner>
    {
        Task<DataTablesStructs.ReturnedData<object>> GetAllBannerDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null);
        Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder();
        Task<List<Banner>> GetAllBannersActive();
    }
}
