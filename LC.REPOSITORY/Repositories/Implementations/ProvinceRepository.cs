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
    public class ProvinceRepository : Repository<Province> , IProvinceRepository 
    {
        public ProvinceRepository(LegalConnectionContext context) : base(context) { }

        public async Task<IEnumerable<Select2Structs.Result>> GetProvinceSelect2ClientSide(Guid departmentId, Guid? selectedId = null)
        {
            var query = _context.Provinces.Where(x => x.DepartmentId == departmentId);

            var result = new List<Select2Structs.Result>();
            if(selectedId != null &&selectedId!= Guid.Empty)
            {
                result =await query
                 .Select(x => new Select2Structs.Result
                 {
                     Id = x.Id,
                     Text = x.Name,
                     Selected = selectedId == x.Id
                 })
                 .ToListAsync();
                return result;
            }
            result =await query
                 .Select(x => new Select2Structs.Result
                 {
                     Id = x.Id,
                     Text = x.Name,
                 })
                 .ToListAsync();
            return result;
        }

        public async Task<Select2Structs.ResponseParameters> GetProvinceSelect2(Select2Structs.RequestParameters parameters, Guid departmentId, string searchValue)
        {
            var query = _context.Provinces.Where(x => x.DepartmentId == departmentId).AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.Name.ToLower().Trim().Contains(searchValue.ToLower().Trim()));

            var currentPage = parameters.CurrentPage != 0 ? parameters.CurrentPage - 1 : 0;

            var results = await query
            .Skip(currentPage * ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE)
            .Take(ConstantHelpers.SELECT2.DEFAULT.PAGE_SIZE)
            .Select(x=> new Select2Structs.Result 
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
