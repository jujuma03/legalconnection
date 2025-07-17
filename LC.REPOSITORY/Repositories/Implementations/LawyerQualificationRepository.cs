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
    public class LawyerQualificationRepository : Repository<LawyerQualification>, ILawyerQualificationRepository
    {
        public LawyerQualificationRepository(LegalConnectionContext context) : base(context) { }

        public async Task<ResultCustomModel> SendQualification(LawyerQualification entity)
        {
            var result = new ResultCustomModel();

            if (await _context.LawyerQualifications.AnyAsync(x => x.LegalCaseId == entity.LegalCaseId && x.ClientId == entity.ClientId && x.LawyerId == entity.LawyerId))
            {
                result.Success = false;
                result.Message = "Ya se ha registrado uan calificación para este caso.";
            }

            await _context.LawyerQualifications.AddAsync(entity);
            await _context.SaveChangesAsync();
            result.Success = true;
            return result;
        }

        public async Task<bool> AnyQualificationByFilter(Guid legalCaseId, Guid clientId, Guid lawyerId)
            => await _context.LawyerQualifications.AnyAsync(x => x.LegalCaseId == legalCaseId && x.ClientId == clientId && x.LawyerId == lawyerId);

        public async Task<int> GetTotalQualification(Guid lawyerId)
        {
            var qualifications = await _context.LawyerQualifications.Where(x => x.LawyerId == lawyerId).Select(x => x.Qualification).ToListAsync();

            if (!qualifications.Any())
                return 0;

            var result = (int)Math.Round((decimal)(qualifications.Sum() / qualifications.Count()), 0, MidpointRounding.AwayFromZero);
            return result;
        }

        public async Task<IEnumerable<LawyerQualification>> GetLawyerQualificationToProfile(Guid lawyerId)
        {
            var result = await _context.LawyerQualifications
                .Where(x=>x.LawyerId == lawyerId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(3)
                .Select(x => new LawyerQualification
                {
                    Commentary = x.Commentary,
                    Qualification = x.Qualification,
                    Client = new Client
                    {
                        User = new ApplicationUser
                        {
                            Name = x.Client.User.Name,
                            Surnames = x.Client.User.Surnames,
                            Picture = x.Client.User.Picture
                        }
                    }
                })
                .ToListAsync();

            return result;
        }

        public async Task<int> QualificationQuantity(Guid lawyerId)
            => await _context.LawyerQualifications.Where(x => x.LawyerId == lawyerId).CountAsync();

        public async Task<PaginationStructs.ReturnedData<LawyerQualification>> GetLawyerQualifaction(PaginationStructs.SentParameters sentParameters,Guid id)
        {
            var query = _context.LawyerQualifications
                .Where(x=> x.LawyerId == id)
                .AsNoTracking();

            var recordsTotal = await query.CountAsync();
            var result = await query
                .Skip(sentParameters.RecordsToTake)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x=> new LawyerQualification 
                {
                    Commentary = x.Commentary,
                    Qualification = x.Qualification,
                    Client = new Client
                    {
                        User = new ApplicationUser
                        {
                            Name = x.Client.User.Name,
                            Surnames = x.Client.User.Surnames,
                            Picture = x.Client.User.Picture
                        }
                    }
                })
                .ToListAsync();

            return new PaginationStructs.ReturnedData<LawyerQualification>
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
    }
}
