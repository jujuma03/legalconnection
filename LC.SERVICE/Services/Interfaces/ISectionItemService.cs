using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ISectionItemService
    {
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline, byte v);
        Task Insert(SectionItem sectionItem);
        Task<SectionItem> Get(Guid id);
        Task Update(SectionItem sectionitem);
        Task Delete(SectionItem sectionItem);
        Task<List<SectionItem>> GetActiveBySection(byte benefits);
    }
}
