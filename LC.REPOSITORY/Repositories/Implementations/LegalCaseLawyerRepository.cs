using LC.CORE.Helpers;
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
    public class LegalCaseLawyerRepository : Repository<LegalCaseLawyer> , ILegalCaseLawyerRepository
    {
        public LegalCaseLawyerRepository(LegalConnectionContext context) : base(context) { }
        public async Task<ResultCustomModel> AccessLegalCaseLawyerInfo(Guid lawyerId, Guid? legalCaseId, Guid? clientId) //o por caso o por cliente
        {
            var result = new ResultCustomModel();

            if (legalCaseId.HasValue)
            {
                var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
                var legalCaseLawyer = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId && x.LawyerId == lawyerId).FirstOrDefaultAsync();


                if (legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED)
                {
                    result.Message = "El caso se encuentra finalizado.";
                }

                if (legalCaseLawyer.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PAYMENT_MADE)
                {
                    result.Success = true;
                    return result;
                }
            }

            if (clientId.HasValue)
            {
                var legalCasesId = await _context.LegalCases.Where(x => x.ClientId == clientId && x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED).Select(x=>x.Id).ToListAsync();
                var legalCaseLawyers = await _context.LegalCaseLawyers.Where(x => legalCasesId.Contains(x.LegalCaseId) && x.LawyerId == lawyerId).ToListAsync();

                if(legalCaseLawyers.Any(y=>y.Status == ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PAYMENT_MADE))
                {
                    result.Success = true;
                    return result;
                }
            }

            result.Message = "No tiene acceso a esta información.";
            return result;
        }

        public async Task<LegalCaseLawyer> Get(Guid legalCaseId, Guid lawyerId)
            => await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId && x.LawyerId == lawyerId).FirstOrDefaultAsync();
    }
}
