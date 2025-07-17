using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LegalCaseResponseService : ILegalCaseResponseService
    {
        private readonly ILegalCaseResponseRepository _legalCaseResponseRepository;

        public LegalCaseResponseService(ILegalCaseResponseRepository legalCaseResponseRepository)
        {
            _legalCaseResponseRepository = legalCaseResponseRepository;
        }

        public async Task<LegalCaseResponse> Get(Guid id)
            => await _legalCaseResponseRepository.Get(id);

        public async Task Insert(LegalCaseResponse entity)
            => await _legalCaseResponseRepository.Insert(entity);

        public async Task Update(LegalCaseResponse entity)
            => await _legalCaseResponseRepository.Update(entity);
    }
}
