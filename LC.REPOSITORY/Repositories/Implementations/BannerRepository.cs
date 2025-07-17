using LC.CORE.Extensions;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        public BannerRepository(LegalConnectionContext context) : base(context)
        {
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllBannerDatatable(DataTablesStructs.SentParameters sentParameters, string headline = null, byte? status = null)
        {
            var query = _context.Banners
                .AsNoTracking();

            var recordsFiltered = await query.CountAsync();

            if (!string.IsNullOrEmpty(headline))
            {
                query = query.Where(q => q.Headline.Contains(headline));
            }

            if (status != 0)
                query = query.Where(q => q.Status == status);

            query = query.AsQueryable();

            var data = await query
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new
                {
                    Id = x.Id,
                    Headline = x.Headline,
                    UrlImage = x.UrlImage,
                    PublicationDate = x.PublicationDate.ToLocalDateFormat(),
                    Status = ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[x.Status],
                    SequenceOrder = (x.SequenceOrder.HasValue && x.SequenceOrder.Value != 0) ? ConstantHelpers.SEQUENCE_ORDER.VALUES[x.SequenceOrder.Value] : "SIN ORDEN",
                    SequenceOrderId = x.SequenceOrder.HasValue ? x.SequenceOrder.Value : 0
                }).ToListAsync();

            var recordsTotal = data.Count;

            return new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetAvailableOrdersAndListSequenceOrder()
        {
            var banners = await _context.Banners.ToListAsync();

            return ConstantHelpers.SEQUENCE_ORDER.VALUES
                .Where(x => !banners.Any(b => b.SequenceOrder == x.Key));
        }
        public async Task<List<Banner>> GetAllBannersActive()
          => await _context.Banners.Where(x => x.Status == 1).ToListAsync();
    }
}
