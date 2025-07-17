using LC.CORE.Helpers;
using LC.CORE.Structs;
using LC.DATABASE.Data;
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
    public class HowItWorksStepRepository : Repository<HowItWorksStep>, IHowItWorksStepRepository
    {
        public HowItWorksStepRepository(LegalConnectionContext context) : base(context)
        {
        }

        public async Task<List<HowItWorksStep>> GetAllActive(byte type =0)
        {
            var query = _context.HowItWorksSteps.Where(x => x.Status == ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE);
            if (type!= 0)
                query = query.Where(x => x.Type == type);


            return await query.OrderBy(x=>x.Order).ToListAsync();
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null)
        {
            var query = _context.HowItWorksSteps
                .AsNoTracking();

            var recordsFiltered = await query.CountAsync();

            if (!string.IsNullOrEmpty(headline))
            {
                query = query.Where(q => q.Title.Contains(headline));
            }

            if (status != 0)
                query = query.Where(q => q.Status == status);

            query = query.AsQueryable();

            var data = await query
                .OrderBy(x=>x.Type)
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new
                {
                    Id = x.Id,
                    Headline = x.Title,
                    UrlImage = x.UrlImage,
                    IntType = x.Type,
                    Type =ConstantHelpers.ENTITIES.HOW_IT_WORKS.TYPE.VALUES[x.Type],
                    Status = ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[x.Status],
                    SequenceOrder = (x.Order != 0) ? ConstantHelpers.SEQUENCE_ORDER.VALUES[x.Order] : "SIN ORDEN",
                    SequenceOrderId = x.Order,
                })
                .ToListAsync();

            var recordsTotal = data.Count;

            return new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };
        }
        public async Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder(byte type)
        {
            var banners = await _context.HowItWorksSteps
                .Where(x=>x.Type == type)
                .ToListAsync();

            return ConstantHelpers.SEQUENCE_ORDER.VALUES
                .Where(x => !banners.Any(b => b.Order == x.Key));
        }
    }
}
