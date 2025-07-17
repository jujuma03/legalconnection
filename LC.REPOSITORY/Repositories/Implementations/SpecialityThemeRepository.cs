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
    public class SpecialityThemeRepository : Repository<SpecialityTheme>, ISpecialityThemeRepository
    {
        public SpecialityThemeRepository(LegalConnectionContext context) : base(context) { }
        public async Task<Select2Structs.ResponseParameters> GetSpecialityThemesSelect2(Select2Structs.RequestParameters parameters, string searchValue, Guid? specialityId, List<Guid> specialitiesId, bool colloquialName)
        {
            var query = GetSpeialityThemesByParameters(searchValue, specialityId, specialitiesId);

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

        private IQueryable<SpecialityTheme> GetSpeialityThemesByParameters(string searchValue, Guid? specialityId, List<Guid> specialitiesId)
        {
            var query = _context.SpecialityThemes.AsQueryable();

            if (specialityId.HasValue)
                query = query.Where(x => x.SpecialityId == specialityId);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.ColloquialName.ToLower().Trim().Contains(searchValue.ToLower().Trim()));

            if (specialitiesId != null)
                query = query.Where(x => specialitiesId.Contains(x.SpecialityId));
            return query;
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetSpecialityThemesDatatable(DataTablesStructs.SentParameters sentParameters, Guid? specialityId, string searchValue)
        {
            var query = _context.SpecialityThemes.AsQueryable();

            if (specialityId.HasValue)
                query = query.Where(x => x.SpecialityId == specialityId);

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

        public async Task<bool> AnyByColloquialName(string name, Guid? ignoredId = null)
            => await _context.SpecialityThemes.AnyAsync(x => x.ColloquialName.Trim().ToLower().Contains(name.Trim().ToLower()) && x.Id != ignoredId);

        public async Task<bool> AnyByOfficialName(string name, Guid? ignoredId = null)
           => await _context.SpecialityThemes.AnyAsync(x => x.OfficialName.Trim().ToLower().Contains(name.Trim().ToLower()) && x.Id != ignoredId);

        public async Task<List<SpecialityTheme>> GetSpecialityThemesBySpecialitiesId(List<Guid> specialitiesId)
        {
            var query = GetSpeialityThemesByParameters(null, null, specialitiesId);
            var result = await query.Select(x => new SpecialityTheme
            {
                Id = x.Id,
                Code=x.Code,
                OfficialName = x.OfficialName,
                ColloquialName=x.ColloquialName,
                SpecialityId = x.SpecialityId,
                Speciality = new Speciality
                {
                    OfficialName = x.Speciality.OfficialName,
                    ColloquialName = x.Speciality.ColloquialName
                }
            }).ToListAsync();
            return result;
        }

        public async Task<bool> HasLawyerAssigned(Guid id)
            => await _context.SpecialityThemes.Where(x => x.Id == id).AnyAsync(x => x.LawyerSpecialityThemes.Any());

        public async Task<bool> AnyByCode(string code, Guid? ignoredId)
        {
            return await _context.SpecialityThemes.AnyAsync(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower()) && x.Id != ignoredId);
        }
    }
}
