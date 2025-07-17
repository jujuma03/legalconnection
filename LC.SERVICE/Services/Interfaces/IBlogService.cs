using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IBlogService
    {
        Task Insert(Blog entity);
        Task<Blog> Get(Guid id);
        Task Delete(Blog entity);
        Task<bool> AnyByTypeAndEntity(byte type, Guid idToVerifiy);
        Task<DataTablesStructs.ReturnedData<object>> GetBlogDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
        Task<IEnumerable<Blog>> GetAll();
        Task<PaginationStructs.ReturnedData<PublicationCustomModel>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters);
    }
}
