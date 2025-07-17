using LC.CORE.Extensions;
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
    public class LawyerWithdrawalRequestRepository : Repository<LawyerWithdrawalRequest>, ILawyerWithdrawalRequestRepository
    {
        public LawyerWithdrawalRequestRepository(LegalConnectionContext context) :base(context) { }

        public async Task<DataTablesStructs.ReturnedData<object>> GetWithdrawalRequestDatatable(DataTablesStructs.SentParameters sentParameters, byte? status, Guid? lawyerId = null)
        {
            var query = _context.WithdrawalRequests.AsNoTracking();

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x=>x.CreatedAt)
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new
                {
                    x.Id,
                    lawyerFullName = $"{x.Lawyer.User.Surnames}, {x.Lawyer.User.Name}",
                    registerDate = x.CreatedAt.ToLocalDateTimeFormat(),
                    x.Amount,
                    status = ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.VALUES[x.Status]
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
        public async Task InsertLawyerWithdrawals(LawyerWithdrawal entity)
        {
            await _context.LawyerWithdrawals.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<PaginationStructs.ReturnedData<WithdrawalRequestCustomModel>> GetWithdrawalRequest(PaginationStructs.SentParameters sentParameters, byte? status, Guid? lawyerId = null)
        {
            var query = _context.LawyerWithdrawals
                .Where(x=>x.LawyerId == lawyerId).AsNoTracking();

            //if (status.HasValue)
            //    query = query.Where(x => x.Status == status);

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new WithdrawalRequestCustomModel
                {
                    Id = x.Id,
                    UrlDepositReceipt = x.WithdrawalRequest.UrlDepositReceipt,
                    RegisterDate = x.CreatedAt.ToLocalDateTimeFormat(),
                    Amount = x.Amount,
                    BankAccount = x.WithdrawalRequest.BankAccount,
                    InterBankAccount = x.WithdrawalRequest.InterbankAccount,
                    Status = ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.VALUES[x.WithdrawalRequest.Status]
                })
                .ToListAsync();

            var recordsFiltered = data.Count;

            var result = new PaginationStructs.ReturnedData<WithdrawalRequestCustomModel>
            {
                Data = data,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = sentParameters.Page,
                    RecordsPerDraw = sentParameters.RecordsPerDraw
                }
            };
            return result;
        }
    }
}
