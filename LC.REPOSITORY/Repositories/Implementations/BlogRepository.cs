using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Structs;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class BlogRepository : Repository<Blog> , IBlogRepository
    {
        public BlogRepository(LegalConnectionContext context) : base(context) { }

        public async Task<bool> AnyByTypeAndEntity(byte type, Guid idToVerifiy)
        {
            if(type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION)
            {
                return await _context.Blogs.AnyAsync(x => x.Type == type && x.ExternalPublicationId == idToVerifiy);
            } 
            else
            {
                return await _context.Blogs.AnyAsync(x => x.Type == type && x.LawyerPublicationId == idToVerifiy);
            }
        }

        public async Task<IEnumerable<Blog>> GetAll()
            => await _context.Blogs.Include(x=>x.LawyerPublication).Include(x=>x.ExternalPublication).ToListAsync();

        public async Task<DataTablesStructs.ReturnedData<object>> GetBlogDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
        {
            var query = _context.Blogs.AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => 
                x.LawyerPublication.Title.ToLower().Contains(searchValue) ||
                x.LawyerPublication.Topic.ToLower().Contains(searchValue) || 
                x.ExternalPublication.Title.ToLower().Contains(searchValue) ||
                x.ExternalPublication.Topic.ToLower().Contains(searchValue)
                );


            var recordsFiltered = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(parameters.PagingFirstRecord)
                .Take(parameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    title = x.Type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION ? x.ExternalPublication.Title : x.LawyerPublication.Title,
                    topic = x.Type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION ? x.ExternalPublication.Topic : x.LawyerPublication.Topic,
                    lawyer = x.Type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION ? x.ExternalPublication.LawyerFullName : $"{x.LawyerPublication.Lawyer.User.Name} {x.LawyerPublication.Lawyer.User.Surnames}",
                    publicationDate = x.Type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION ? x.ExternalPublication.PublicationDate.ToLocalDateFormat() : x.LawyerPublication.PublicationDate.ToLocalDateFormat()
                })
                .ToListAsync();

            var recordsTotal = data.Count;

            return new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = parameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

        }
        public async Task<PaginationStructs.ReturnedData<PublicationCustomModel>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters)
        {
            var query = _context.Blogs
                .Where(x => (x.LawyerPublication!= null&&x.LawyerPublication.Status == ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.CONFIRMED)||x.ExternalPublication!=null)
                .AsQueryable();
            var recordsTotal = await query.CountAsync();

            var data = await query
                .Select(x => new PublicationCustomModel
                {
                    Id=x.Id,
                    Type= x.LawyerPublication== null ? ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION : ConstantHelpers.ENTITIES.BLOG.TYPES.LAWYERPUBLICATION,
                    ExternalPublication=x.ExternalPublication!= null ? new ExternalPublicationCustom
                    {
                        Description=x.ExternalPublication.Description,
                        PhotoUrl=x.ExternalPublication.PhotoUrl,
                        PublicationDate=x.ExternalPublication.PublicationDate.ToLocalDateFormat(),
                        Title=x.ExternalPublication.Title
                    } : null,
                    LawyerPublication=x.LawyerPublication!= null ? new LawyerPublicationCustom
                    {
                        Description=x.LawyerPublication.Description,
                        PhotoUrl=x.LawyerPublication.PhotoUrl,
                        PublicationDate=x.LawyerPublication.PublicationDate.ToLocalDateFormat(),
                        Title=x.LawyerPublication.Title
                    } : null,
                })
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .ToListAsync();

            return new PaginationStructs.ReturnedData<PublicationCustomModel>
            {
                Data=data,
                PaginationData=new PaginationStructs.PaginationData
                {
                    RecordsTotal=recordsTotal,
                    Active = sentParameters.Page,
                    RecordsPerDraw= sentParameters.RecordsPerDraw
                }
            };
        }
    }
}
