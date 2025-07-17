using LC.CORE.Structs;
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
    public class LawyerCardRepository : Repository<LawyerCard> , ILawyerCardRepository
    {
        public LawyerCardRepository(LegalConnectionContext context) : base(context) { }

        public async Task<PaginationStructs.ReturnedData<LawyerCard>> GetLawyerCards(PaginationStructs.SentParameters sentParameters, Guid lawyerId)
        {
            var query = _context.LawyerCards.Where(x => x.LawyerId == lawyerId)
                .AsNoTracking();

            var recordsTotal = await query.CountAsync();
            var result = await query
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new LawyerCard
                {
                    Id = x.Id,
                    CardBrand = x.CardBrand,
                    Owner = x.Owner,
                    LastCardDigits = x.LastCardDigits,
                    CreatedAt = x.CreatedAt,
                    Default = x.Default
                })
                .ToListAsync();

            return new PaginationStructs.ReturnedData<LawyerCard>
            {
                Data = result,
                PaginationData = new PaginationStructs.PaginationData
                {
                    RecordsTotal = recordsTotal,
                    Active = sentParameters.Page,
                    RecordsPerDraw = sentParameters.RecordsPerDraw
                }
            };
        }

        public async Task<int> GetLawyerCardQuantity(Guid lawyerId)
            => await _context.LawyerCards.Where(x => x.LawyerId == lawyerId).CountAsync();

        public async Task<LawyerCard> GetDefaultLawyerCard(Guid lawyerId)
            => await _context.LawyerCards.Where(x => x.LawyerId == lawyerId && x.Default).FirstOrDefaultAsync();
    }
}
