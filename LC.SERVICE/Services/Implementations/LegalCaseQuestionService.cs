using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LegalCaseQuestionService : ILegalCaseQuestionService
    {
        private readonly ILegalCaseQuestionRepository _legalCaseQuestionRepository;

        public LegalCaseQuestionService(ILegalCaseQuestionRepository legalCaseQuestionRepository)
        {
            _legalCaseQuestionRepository = legalCaseQuestionRepository;
        }

        public async Task<bool> AnyByDescription(string description, Guid? ignoredId = null)
            => await _legalCaseQuestionRepository.AnyByDescription(description, ignoredId);

        public async Task Delete(LegalCaseQuestion entity)
            => await _legalCaseQuestionRepository.Delete(entity);

        public async Task<LegalCaseQuestion> Get(Guid id)
            => await _legalCaseQuestionRepository.Get(id);

        public async Task<IEnumerable<LegalCaseQuestion>> GetLegalCaseQuestions(Guid legalCaseId)
            => await _legalCaseQuestionRepository.GetLegalCaseQuestions(legalCaseId);

        public async Task<DataTablesStructs.ReturnedData<object>> GetLegalcaseQuestionsDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
            => await _legalCaseQuestionRepository.GetLegalcaseQuestionsDatatable(parameters, searchValue);

        public async Task<bool> HasRespones(Guid id)
            => await _legalCaseQuestionRepository.HasResponses(id);

        public async Task Insert(LegalCaseQuestion entity)
            => await _legalCaseQuestionRepository.Insert(entity);

        public async Task Update(LegalCaseQuestion entity)
            => await _legalCaseQuestionRepository.Update(entity);
    }
}
