using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class MissionVisionService: IMissionVisionService
    {
        private readonly IMissionVisionRepository _missionVisionRepository;

        public MissionVisionService(IMissionVisionRepository missionVisionRepository)
        {
            _missionVisionRepository = missionVisionRepository;
        }

        public async Task Insert(MissionVision missionVision) =>
            await _missionVisionRepository.Insert(missionVision);

        public async Task Update(MissionVision missionVision) =>
            await _missionVisionRepository.Update(missionVision);


        public async Task<MissionVision> GetMissionVisionById(Guid id) =>
            await _missionVisionRepository.Get(id);

        public async Task<List<MissionVision>> GetMissionVision() =>
            await _missionVisionRepository.GetMissionVision();
    }
}
