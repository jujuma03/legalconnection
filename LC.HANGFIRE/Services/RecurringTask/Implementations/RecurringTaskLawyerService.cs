using LC.CORE.Helpers;
using LC.DATABASE.Data;
using LC.HANGFIRE.Services.RecurringTask.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.HANGFIRE.Services.RecurringTask.Implementations
{
    public class RecurringTaskLawyerService : IRecurringTaskLawyerService
    {
        private readonly LegalConnectionContext _context;

        public RecurringTaskLawyerService(LegalConnectionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Restablece la cantidad de consultas gratuitas del abogado mensualmente en base a la cantidad establecida en la configuración 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateFreeConsulting()
        {
            var freeConsultingConfiguration = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.FREE_CONSULTING).FirstOrDefaultAsync();
            var lawyers = await _context.Lawyers.Where(x => x.Status == ConstantHelpers.ENTITIES.LAWYER.STATUS.VALIDATED).ToListAsync();

            var freeConsulting = Convert.ToInt32(freeConsultingConfiguration.Value);

            foreach (var lawyer in lawyers)
                lawyer.FreeLegalCases = Convert.ToInt32(freeConsulting);

            await _context.SaveChangesAsync();
        }
    }
}
