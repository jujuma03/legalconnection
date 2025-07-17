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
    public class LawyerInterviewRepository : Repository<LawyerInterview> , ILawyerInterviewRepository
    {
        public LawyerInterviewRepository(LegalConnectionContext context) :base(context) { }

        public async Task<IEnumerable<LawyerInterview>> GetInterviewsByLawyer(Guid lawyerId)
            => await _context.LawyerInterviews.Where(x => x.LawyerId == lawyerId).ToArrayAsync();
    }
}
