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
    public class SpecialityRepository : Repository<Speciality>, ISpecialityRepository
    {
        public SpecialityRepository(LegalConnectionContext context) : base(context) { }

        public async Task<Select2Structs.ResponseParameters> GetSpecialitiesSelect2(Select2Structs.RequestParameters parameters, string searchValue, bool colloquialName)
        {
            var query = _context.Specialities.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.ColloquialName.ToLower().Trim().Contains(searchValue.ToLower().Trim()));

            var currentPage = parameters.CurrentPage != 0 ? parameters.CurrentPage - 1 : 0;

            var results = await query
            .Skip(currentPage * ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE)
            .Take(ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE)
            .Select(x => new Select2Structs.Result
            {
                Id = x.Id,
                Text = colloquialName ? x.ColloquialName : x.OfficialName
            })
            .ToListAsync();

            return new Select2Structs.ResponseParameters
            {
                Pagination = new Select2Structs.Pagination
                {
                    More = results.Count >= ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE
                },
                Results = results
            };
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetSpecialitiesDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue)
        {
            var query = _context.Specialities.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToLower().Trim();
                query = query.Where(x => x.ColloquialName.Trim().ToLower().Contains(searchValue) || x.OfficialName.Trim().ToLower().Contains(searchValue));
            }

            var recordsTotal = await query.CountAsync();

            var data = await query
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.ColloquialName,
                    x.OfficialName,
                    x.Code,
                    x.Id
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

        public async Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null)
            => await _context.Specialities.AnyAsync(x => x.OfficialName.Trim().ToLower() == name.Trim().ToLower() && x.Id != ignoredId);

        public async Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null)
    => await _context.Specialities.AnyAsync(x => x.ColloquialName.Trim().ToLower() == name.Trim().ToLower() && x.Id != ignoredId);

        public async Task<IEnumerable<Speciality>> GetAll()
            => await _context.Specialities.ToArrayAsync();
        public async Task<bool> HasSpecialityThemes(Guid id)
            => await _context.Specialities.Where(x => x.Id == id).AnyAsync(y => y.SpecialityThemes.Any());

        public async Task<bool> AnyByCode(string code, Guid? ignoredId)
        {
            return await _context.Specialities.AnyAsync(x => x.Code.Trim().ToLower() == code.Trim().ToLower() && x.Id != ignoredId);
        }
    }
}
