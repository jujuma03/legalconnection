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
    public class LegalCaseObservationRepository : Repository<LegalCaseObservation>, ILegalCaseObservationRepository
    {
        public LegalCaseObservationRepository(LegalConnectionContext context) :base(context) { }

        public async Task<LegalCaseObservation> GetLastPendingObservationByType(Guid legalCaseId, byte process)
            => await _context.LegalCaseObservations.Where(x => x.LegalCaseId == legalCaseId && x.Process == process).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

    }
}
