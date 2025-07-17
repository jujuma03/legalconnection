using LC.CORE.Helpers;
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
    public class LawyerStudyRepository : Repository<LawyerStudy> , ILawyerStudyRepository
    {
        public LawyerStudyRepository(LegalConnectionContext context) : base(context) { }

        public async Task<IEnumerable<LawyerStudy>> GetLawyerStudiesByLawyerId(Guid lawyerId)
            => await _context.LawyerStudies.Where(x => x.LawyerId == lawyerId).ToArrayAsync();

        public async Task<string> GetFeaturedStudy(Guid lawyerId)
        {
            var studies = await _context.LawyerStudies.Where(x => x.LawyerId == lawyerId).ToListAsync();
            var featuredStudy = studies.OrderByDescending(x => x.Grade).FirstOrDefault();
            if (featuredStudy is null)
                return null;

            return $"{ConstantHelpers.ENTITIES.LAWYER_STUDY.GRADE.VALUES[featuredStudy.Grade]} en {featuredStudy.Mention}";
        }

        public async Task<bool> AnyByLawyer(Guid lawyerId)
            => await _context.LawyerStudies.AnyAsync(x => x.LawyerId == lawyerId);
    }
}
