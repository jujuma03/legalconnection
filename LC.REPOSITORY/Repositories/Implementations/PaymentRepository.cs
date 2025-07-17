using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.Structs;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {

        public PaymentRepository(LegalConnectionContext context) : base(context){ }

        public async Task<ResultCustomModel> ProcessPayment(Payment payment)
        {
            var result = new ResultCustomModel();
            var lawyer = await _context.Lawyers.Where(x => x.Id == payment.LawyerId).FirstOrDefaultAsync();
            var user = await _context.Users.Where(x => x.Id == lawyer.UserId).FirstOrDefaultAsync();
            var legalCase = await _context.LegalCases.Where(x => x.Id == payment.LegalCaseId).FirstOrDefaultAsync();
            var legalCaseLawyer = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == payment.LegalCaseId && x.LawyerId == payment.LawyerId).FirstOrDefaultAsync();
            var number = await _context.Payments.CountAsync();

            payment.Serie = "B059";
            payment.Number = number + 1;
            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS;
            legalCaseLawyer.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PAYMENT_MADE;

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
           
            result.Success = true;
            result.Message = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.VALUES[legalCase.Status];
            return result;
        }
        public async Task<DataTablesStructs.ReturnedData<object>> GetClientsWithPayment(DataTablesStructs.SentParameters parameters, string searchValue)
        {
            var query = _context.Payments
                 .AsNoTracking();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.LegalCase.Client.User.Name.ToUpper().Contains(searchValue.ToUpper()) ||
                                         x.LegalCase.Client.User.UserName.ToUpper().Contains(searchValue.ToUpper()) ||
                                         x.LegalCase.Client.User.Surnames.ToUpper().Contains(searchValue.ToUpper())
                                         );

            var recordsTotal = await query.CountAsync();

            var data = await query
                    .Select(x => new
                    {
                        x.Id,
                        x.LegalCase.Client.User.Name,
                        x.LegalCase.Client.User.Surnames,
                        x.LegalCase.Client.User.Document,
                        x.LegalCase.Client.User.Email,
                        TotalAmount= $"S/. {x.TotalAmount.ToString("0.00")}",
                        date = x.CreatedAt.ToLocalDateTimeFormat(),
                        x.CreatedAt,
                    })
                    .OrderBy(x => x.CreatedAt)
                    .Skip(parameters.PagingFirstRecord)
                    .Take(parameters.RecordsPerDraw)
                    .ToListAsync();

            var recordsFiltered = data.Count;

            var result = new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = parameters.DrawCounter,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
            };
            return result;
        }

    }
}
