using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LegalCaseObservationService : ILegalCaseObservationService
    {
        private readonly ILegalCaseObservationRepository _legalCaseObservationRepository;

        public LegalCaseObservationService(ILegalCaseObservationRepository legalCaseObservationRepository)
        {
            _legalCaseObservationRepository = legalCaseObservationRepository;
        }

        public async Task<LegalCaseObservation> GetLastPendingObservationByType(Guid legalCaseId, byte process)
            => await _legalCaseObservationRepository.GetLastPendingObservationByType(legalCaseId, process);

        public async Task Insert(LegalCaseObservation entity)
            => await _legalCaseObservationRepository.Insert(entity);

        public async Task Update(LegalCaseObservation entity)
            => await _legalCaseObservationRepository.Update(entity);
    }
}
