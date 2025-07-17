using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<bool> AnyByTypeAndEntity(byte type, Guid idToVerifiy);
        Task<DataTablesStructs.ReturnedData<object>> GetBlogDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
        Task<IEnumerable<Blog>> GetAll();
        Task<PaginationStructs.ReturnedData<PublicationCustomModel>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters);
    }
}
