using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class BannerService: IBannerService
    {
        public readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository=bannerRepository;
        }

        public async Task DeleteBanner(Banner banner)
        {
            await _bannerRepository.Delete(banner);
        }

        public async Task<Banner> Get(Guid id)
        {
            return await _bannerRepository.Get(id);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllBannerDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null)
        {
            return await _bannerRepository.GetAllBannerDatatable(sentParameters, headline, status);
        }

        public async Task<List<Banner>> GetAllBannersActive()
        {
            return await _bannerRepository.GetAllBannersActive();
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder()
        {
            return await _bannerRepository.GetAvailableOrdersAndListSequenceOrder();
        }

        public async Task Insert(Banner banner)
        {
            await _bannerRepository.Insert(banner);
        }

        public async Task Update(Banner banner)
        {
            await _bannerRepository.Update(banner);
        }
    }
}
