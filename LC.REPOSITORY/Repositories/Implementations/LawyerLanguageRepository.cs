using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class LawyerLanguageRepository : Repository<LawyerLanguage> , ILawyerLanguageRepository
    {
        public LawyerLanguageRepository(LegalConnectionContext context) : base(context) { }

        public async Task<bool> AnyByLawyerId(Guid lawyerId, Guid languageId)
            => await _context.LawyerLanguages.AnyAsync(x => x.LawyerId == lawyerId && x.LanguageId == languageId);

        public async Task<IEnumerable<LawyerLanguage>> GetLanguagesByLawyerId(Guid lawyerId)
            => await _context.LawyerLanguages.Where(x => x.LawyerId == lawyerId).Include(x => x.Language).ToArrayAsync();
    }
}
