using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> AnyByTypeAndEntity(byte type, Guid idToVerifiy)
            => await _blogRepository.AnyByTypeAndEntity(type, idToVerifiy);

        public async Task Delete(Blog entity)
            => await _blogRepository.Delete(entity);

        public async Task<Blog> Get(Guid id)
            => await _blogRepository.Get(id);

        public async Task<IEnumerable<Blog>> GetAll()
            => await _blogRepository.GetAll();

        public async Task<DataTablesStructs.ReturnedData<object>> GetBlogDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
            => await _blogRepository.GetBlogDatatable(parameters, searchValue);

        public async Task Insert(Blog entity)
            => await _blogRepository.Insert(entity);

        public async Task<PaginationStructs.ReturnedData<PublicationCustomModel>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters)
        {
            return await _blogRepository.GetLawyerPublications(sentParameters);
        }
    }
}
