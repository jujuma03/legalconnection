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
    public class FrequentQuestionRepository : Repository<FrequentQuestion>, IFrequentQuestionRepository
    {
        public FrequentQuestionRepository(LegalConnectionContext context) : base(context)
        {
        }

        public async Task<List<FrequentQuestion>> GetAllActive(byte type)
        {
            return  await _context.FrequentQuestions.Where(x => x.Type == type && x.Status == ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE).ToListAsync();
        }
        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, string headline, byte status)
        {
            var query = _context.FrequentQuestions
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
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    x.Description,
                    Status = ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[x.Status],
                    type = ConstantHelpers.ENTITIES.FREQUENT_QUESTION.TYPES.VALUES[x.Type],
                    icon = x.Icon
                })
                .ToListAsync();

            data =data
                .OrderBy(z => z.type)
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .ToList();

            var recordsTotal = data.Count;

            return new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };
        }
    }
}
