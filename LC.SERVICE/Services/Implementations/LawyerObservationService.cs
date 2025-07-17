using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerObservationService : ILawyerObservationService
    {
        private readonly ILawyerObservationRepository _lawyerObservationRepository;

        public LawyerObservationService(ILawyerObservationRepository lawyerObservationRepository)
        {
            _lawyerObservationRepository = lawyerObservationRepository;
        }

        public async Task<LawyerObservation> GetLastObservationByType(Guid lawyerId, byte process)
            => await _lawyerObservationRepository.GetLastObservationByType(lawyerId, process);

        public async Task Insert(LawyerObservation entity)
            => await _lawyerObservationRepository.Insert(entity);

        public async Task Update(LawyerObservation entity)
            => await _lawyerObservationRepository.Update(entity);
    }
}
