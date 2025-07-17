using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IBannerService
    {
        Task<DataTablesStructs.ReturnedData<object>> GetAllBannerDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null);
        Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder();
        Task Insert(Banner banner);
        Task<List<Banner>> GetAllBannersActive();
        Task<Banner> Get(Guid id);
        Task DeleteBanner(Banner banner);
        Task Update(Banner banner);
    }
}
