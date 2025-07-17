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
    public class LawyerExperienceRepository : Repository<LawyerExperience>,ILawyerExperienceRepository
    {
        public LawyerExperienceRepository(LegalConnectionContext context) : base(context) { }

        public async Task<bool> AnyByLawyer(Guid lawyerId)
            => await _context.LawyerExperiences.AnyAsync(x => x.LawyerId == lawyerId);

        public async Task<IEnumerable<LawyerExperience>> GetLawyerExperiencesByLawyerId(Guid lawyerId)
            => await _context.LawyerExperiences.Where(x => x.LawyerId == lawyerId).ToArrayAsync();

        public async Task<string> GetTotalExperienceByLawyerId(Guid lawyerId)
        {
            var experiences = await _context.LawyerExperiences.Where(x => x.LawyerId == lawyerId).ToListAsync();

            foreach (var item in experiences)
            {
                if (!item.EndDate.HasValue)
                    item.EndDate = DateTime.UtcNow;
            }

            var totalDays = experiences.Select(x => (x.EndDate.Value - x.StartDate).TotalDays).ToList().Sum();
            var totalYears = Math.Truncate(totalDays / 365);
            var totalMonths = Math.Truncate((totalDays % 365) / 30);
            var remainingDays = Math.Truncate((totalDays % 365) % 30);
            return $"{totalYears} año {totalMonths} meses";
        } 
    }
}
