using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IMissionVisionRepository:IRepository<MissionVision>
    {
        Task<List<MissionVision>> GetMissionVision();
    }
}
