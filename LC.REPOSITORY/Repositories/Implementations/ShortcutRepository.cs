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
    public class ShortcutRepository : Repository<Shortcut>, IShortcutRepository
    {
        public ShortcutRepository(LegalConnectionContext context) : base(context)
        {
        }

        public async Task<List<Shortcut>> GetAllActive(byte type)
        {
            var query = _context.Shortcuts.Where(x=>x.Status == ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE)
               .AsNoTracking();

            if(type != 0)
            {
                query = query.Where(x => x.Type==type);
            }
            return await query.ToListAsync();
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, byte type, byte status)
        {
            var query = _context.Shortcuts
                .AsNoTracking();

            var recordsFiltered = await query.CountAsync();

            if (type != 0)
            {
                query = query.Where(q => q.Type == type);
            }

            if (status != 0)
                query = query.Where(q => q.Status == status);

            query = query.AsQueryable();

            var data = await query
                .Select(x => new
                {
                    Id = x.Id,
                    x.Title,
                    Status = ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[x.Status],
                    type = ConstantHelpers.ENTITIES.SHORTCUT.TYPES.VALUES[x.Type],
                    url = x.UrlDirection,
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
