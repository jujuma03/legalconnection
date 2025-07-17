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
    public class LawyerObservationRepository : Repository<LawyerObservation> , ILawyerObservationRepository
    {
        public LawyerObservationRepository(LegalConnectionContext context) :base(context) { }
        public async Task<LawyerObservation> GetLastObservationByType(Guid lawyerId, byte process)
            => await _context.LawyerObservations.Where(x => x.LawyerId == lawyerId && x.Process == process).OrderByDescending(x=>x.CreatedAt).FirstOrDefaultAsync();

    }
}
