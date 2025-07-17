using LC.CORE.Extensions;
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
    public class ExternalPublicationRepository : Repository<ExternalPublication> , IExternalPublicationRepository
    {
        public ExternalPublicationRepository(LegalConnectionContext context) : base(context) { }

        public async Task<DataTablesStructs.ReturnedData<object>> GetExternalPublicationDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
        {
            var query = _context.ExternalPublications.AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.LawyerFullName.ToLower().Trim().Contains(searchValue.ToLower().Trim()) || x.Title.ToLower().Trim().Contains(searchValue.ToLower().Trim()));

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(parameters.PagingFirstRecord)
                .Take(parameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.LawyerFullName,
                    PublicationDate = x.PublicationDate.ToLocalDateFormat()
                })
                .ToListAsync();

            var recordsFiltered = data.Count;

            var result = new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = parameters.DrawCounter,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
            };

            return result;
        }
    }
}
