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
    public class SectionItemService : ISectionItemService
    {
        private readonly ISectionItemRepository _sectionItemRepository;
        public SectionItemService(ISectionItemRepository sectionItemRepository)
        {
            _sectionItemRepository = sectionItemRepository;
        }

        public async Task Delete(SectionItem sectionItem)
        {
            await _sectionItemRepository.Delete(sectionItem);
        }

        public async Task<SectionItem> Get(Guid id)
        {
            return await _sectionItemRepository.Get(id);
        }

        public async Task<List<SectionItem>> GetActiveBySection(byte benefits)
        {
            return await _sectionItemRepository.GetActiveBySection(benefits);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline, byte status)
        {
            return await _sectionItemRepository.GetAllDatatable(sentParameters, headline, status);
        }

        public async Task Insert(SectionItem sectionItem)
        {
            await _sectionItemRepository.Insert(sectionItem);
        }

        public async Task Update(SectionItem sectionitem)
        {
            await _sectionItemRepository.Update(sectionitem);
        }
    }
}
