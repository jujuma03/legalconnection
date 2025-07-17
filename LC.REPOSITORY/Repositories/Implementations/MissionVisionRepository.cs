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
    public class MissionVisionRepository:Repository<MissionVision>, IMissionVisionRepository
    {
        public MissionVisionRepository(LegalConnectionContext context) : base(context) { }
        public async Task<List<MissionVision>> GetMissionVision()
        {
            var model = await _context.MissionVisions
                .Select(x => new MissionVision
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    UrlImage = x.UrlImage
                }).ToListAsync();

            return model;
        }
    }
}
