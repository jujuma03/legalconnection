using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LegalCaseLawyerService : ILegalCaseLawyerService
    {
        private readonly ILegalCaseLawyerRepository _legalCaseLawyerRepository;

        public LegalCaseLawyerService(ILegalCaseLawyerRepository legalCaseLawyerRepository)
        {
            _legalCaseLawyerRepository = legalCaseLawyerRepository;
        }

        public async Task<ResultCustomModel> AccessLegalCaseLawyerInfo(Guid lawyerId, Guid? legalCaseId, Guid? clientId)
            => await _legalCaseLawyerRepository.AccessLegalCaseLawyerInfo(lawyerId, legalCaseId, clientId);

        public async Task<LegalCaseLawyer> Get(Guid legalCaseId, Guid lawyerId)
            => await _legalCaseLawyerRepository.Get(legalCaseId, lawyerId);
    }
}
