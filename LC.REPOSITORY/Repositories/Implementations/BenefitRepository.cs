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
    public class BenefitRepository :  Repository<Benefit> , IBenefitRepository
    {
        public BenefitRepository(LegalConnectionContext context) : base(context) { }
        public async Task<DataTablesStructs.ReturnedData<object>> GetBenefitsDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue)
        {
            var query = _context.Benefits.AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.Description.ToLower().Contains(searchValue.Trim().ToLower()));

            var recordsTotal = await query.CountAsync();

            var data = await query
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    x.Description
                })
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

        public async Task<List<Benefit>> GetAll()
            => await _context.Benefits.ToListAsync();
    }
}
