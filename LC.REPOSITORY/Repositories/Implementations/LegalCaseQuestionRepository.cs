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
    public class LegalCaseQuestionRepository : Repository<LegalCaseQuestion>, ILegalCaseQuestionRepository
    {
        public LegalCaseQuestionRepository(LegalConnectionContext context) : base(context) { }

        public async Task<bool> AnyByDescription(string description, Guid? ignoredId = null)
            => await _context.LegalCaseQuestions.AnyAsync(x => x.Description.Trim().ToLower() == description.Trim().ToLower() && x.Id != ignoredId);

        public async Task<bool> HasResponses(Guid id)
            => await _context.LegalCaseResponses.AnyAsync(x=>x.LegalCaseQuestionId == id);

        public async Task<DataTablesStructs.ReturnedData<object>> GetLegalcaseQuestionsDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
        {
            var query = _context.LegalCaseQuestions.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.Description.Trim().ToLower().Contains(searchValue.Trim().ToLower()));

            var recordsTotal = await query.CountAsync();

            var data = await query
                .Skip(parameters.PagingFirstRecord)
                .Take(parameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    description = x.Description.Length > 50 ? $"{x.Description.Substring(0,50)}..." : x.Description,
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
        public async Task<IEnumerable<LegalCaseQuestion>> GetLegalCaseQuestions(Guid legalCaseId)
        {
            var legalCaseQuestions = await _context.LegalCaseQuestions
                .Select(x => new LegalCaseQuestion
                {
                    Id = x.Id,
                    Description = x.Description,
                    LegalCaseResponses = x.LegalCaseResponses.Where(x => x.LegalCaseId == legalCaseId)
                    .Select(y => new LegalCaseResponse
                    {
                        Id = y.Id,
                        LegalCaseId = y.LegalCaseId,
                        CreatedAt = y.CreatedAt,
                        Description = y.Description,
                    })
                    .ToList()
                }).ToListAsync();

            return legalCaseQuestions;
        }
    
    }
}
