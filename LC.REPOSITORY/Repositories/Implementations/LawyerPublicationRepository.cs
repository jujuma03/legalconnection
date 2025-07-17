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
    public class LawyerPublicationRepository : Repository<LawyerPublication>, ILawyerPublicationRepository
    {
        public LawyerPublicationRepository(LegalConnectionContext context) : base(context) { }

        public override async Task<LawyerPublication> Get(Guid id)
        {
            return await _context.LawyerPublications
                .Include(x => x.Lawyer.User.District.Province.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<LawyerPublication>> GetLawyerPublications(Guid? lawyerId, byte? status)
        {
            var query = _context.LawyerPublications
                .AsQueryable();
            if (lawyerId.HasValue)
                query = query.Where(x => x.LawyerId == lawyerId);

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            return await query.ToArrayAsync();
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetPublicationsDatatable(DataTablesStructs.SentParameters sentParameters, byte status, string search)
        {
            var query = _context.LawyerPublications.Include(x => x.Lawyer.User).AsQueryable();

            if (status != 0) query = query.Where(x => x.Status == status);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Lawyer.User.Name.ToUpper().Contains(search.ToUpper()) || x.Lawyer.User.Surnames.ToUpper().Contains(search.ToUpper()));

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.PublicationDate)
                .Select(x => new
                {
                    x.Id,
                    x.Lawyer.User.Name,
                    x.Lawyer.User.Surnames,
                    x.Title,
                    x.Status,
                    statusName = ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.VALUES[x.Status],
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat()
                })
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .ToListAsync();

            var recordsFiltered = data.Count;

            var result = new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
            };
            return result;
        }

        public async Task<PaginationStructs.ReturnedData<LawyerPublication>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters, Guid lawyerid)
        {
            var query = _context.LawyerPublications
                .Where(x => x.LawyerId == lawyerid)
                .AsNoTracking();

            var recordsTotal = await query.CountAsync();
            var result = await query
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new LawyerPublication
                {
                    AnswerDate = x.AnswerDate,
                    LawyerId=x.LawyerId,
                    Id =x.Id,
                    Description = x.Description,
                    PhotoUrl = x.PhotoUrl,
                    PublicationDate = x.PublicationDate,
                    Status = x.Status,
                    Title=x.Title,
                    Topic = x.Topic
                })
                .ToListAsync();

            return new PaginationStructs.ReturnedData<LawyerPublication>
            {
                Data = result,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = sentParameters.Page,
                    RecordsPerDraw = sentParameters.RecordsPerDraw
                }
            };
        }
    }
}
