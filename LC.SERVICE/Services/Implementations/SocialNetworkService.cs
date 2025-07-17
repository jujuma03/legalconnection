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
    public class SocialNetworkService : ISocialNetworkService
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        public SocialNetworkService(ISocialNetworkRepository socialNetworkRepository)
        {
            _socialNetworkRepository = socialNetworkRepository;
        }
        public async Task Delete(SocialNetwork social)
        {
            await _socialNetworkRepository.Delete(social);
        }

        public async  Task<SocialNetwork> Get(Guid id)
        {
            return await _socialNetworkRepository.Get(id);
        }

        public async Task<List<SocialNetwork>> GetAllActive()
        {
            return await _socialNetworkRepository.GetAllActive();
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, byte type, byte status)
        {
            return await _socialNetworkRepository.GetAllDatatable(sentParameters, type, status);
        }

        public async Task Insert(SocialNetwork sectionItem)
        {
            await _socialNetworkRepository.Insert(sectionItem);
        }

        public async Task Update(SocialNetwork social)
        {
            await _socialNetworkRepository.Update(social);
        }
    }
}
