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
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(LegalConnectionContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Select2Structs.Result>> GetDepartmentsSelect2ClientSide(string q,Guid? selectedId = null)
        {
            var query = _context.Departments.AsQueryable();

            if (selectedId!= null && selectedId!= Guid.Empty)
            {
                return await query.Select(x => new Select2Structs.Result
                {
                    Id = x.Id,
                    Text = x.Name,
                    Selected = selectedId == x.Id,
                }).ToListAsync();
            }

            return await query.Select(x => new Select2Structs.Result
            {
                Id = x.Id,
                Text = x.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<Select2Structs.Result>> GetUsedDepartmentsSelect2ClientSide(Guid? selectedId = null)
        {
            var query = _context.Departments.Where(x => x.Provinces.Any(y => y.Districts.Any(z => z.Users.Any()))).AsNoTracking();

            var result = await query
                .Select(x => new Select2Structs.Result
                {
                    Id = x.Id,
                    Text = x.Name,
                    Selected = x.Id == selectedId
                })
                .ToListAsync();

            return result;
        }

        public async Task<Select2Structs.ResponseParameters> GetDepartmentSelect2(Select2Structs.RequestParameters parameters, string searchValue)
        {
            var query = _context.Departments.AsNoTracking();

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
