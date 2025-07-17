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
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(LegalConnectionContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Select2Structs.Result>> GetDistrictsSelect2ClientSide(Guid pid)
        {
            return await _context.Districts.Where(x => x.ProvinceId == pid).Select(x => new Select2Structs.Result
            {
                Id = x.Id,
                Text = x.Name
            }).ToListAsync();
        }

        public async Task<Select2Structs.ResponseParameters> GetDistrictsSelect2(Select2Structs.RequestParameters parameters, Guid provinceId, string searchValue)
        {
            var query = _context.Districts.Where(x => x.ProvinceId == provinceId).AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.Name.ToLower().Trim().Contains(searchValue.ToLower().Trim()));

            var currentPage = parameters.CurrentPage != 0 ? parameters.CurrentPage - 1 : 0;

            var results = await query
            .Skip(currentPage * ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE)
            .Take(ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE)
            .Select(x => new Select2Structs.Result
            {
                Id = x.Id,
                Text = x.Name
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

    }
}
