using LC.CORE.Helpers;
using LC.CORE.Structs;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
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
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        public PlanRepository(LegalConnectionContext context) : base(context) { }

        public async Task<DataTablesStructs.ReturnedData<object>> GetPlansDatatable(DataTablesStructs.SentParameters sentParameters, string searchValue)
        {
            var query = _context.Plans.AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.Name.ToLower().Contains(searchValue.Trim().ToLower()));

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x=>x.Amount)
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    interval = ConstantHelpers.ENTITIES.PLAN.INTERVAL.VALUES[x.Interval],
                    x.Amount
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

        public async Task<bool> AnyLawyerByPlan(string id)
            => await _context.Lawyers.AnyAsync(x => x.LawyerPlanDetail.PlanId == id);

        public async Task<bool> ExistFreePlan()
            => await _context.Plans.AnyAsync(x => x.Amount == 0M);

        public async Task<List<PlanBenefit>> GetPlanBenefits(string planId)
            => await _context.PlanBenefits.Where(x => x.PlanId == planId).ToListAsync();

        public async Task UpdatePlanBenefits(List<PlanBenefit> entities, string planId)
        {
            var currentPlanBenefits = await _context.PlanBenefits.Where(x => x.PlanId == planId).ToListAsync();

            var toDelete = new List<PlanBenefit>();
            var toAdd = new List<PlanBenefit>();

            foreach (var item in currentPlanBenefits)
            {
                if (!entities.Any(y => y.BenefitId == item.BenefitId))
                    toDelete.Add(item);
            }

            foreach (var item in entities)
            {
                if (!currentPlanBenefits.Any(y => y.BenefitId == item.BenefitId))
                    toAdd.Add(item);
            }

            _context.PlanBenefits.RemoveRange(toDelete);
            await _context.PlanBenefits.AddRangeAsync(toAdd);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Plan>> GetAll()
            => await _context.Plans.Include(x=>x.PlanBenefits).ToListAsync();

        public async Task<Plan> GetFreePlan()
            => await _context.Plans.Where(x => x.Amount == 0M).FirstOrDefaultAsync();
    }
}
