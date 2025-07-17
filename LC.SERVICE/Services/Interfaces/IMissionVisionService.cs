using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IMissionVisionService
    {
        Task Insert(MissionVision missionVision);
        Task Update(MissionVision missionVision);
        Task<MissionVision> GetMissionVisionById(Guid id);
        Task<List<MissionVision>> GetMissionVision();
    }
}
