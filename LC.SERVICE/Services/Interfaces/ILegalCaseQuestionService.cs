using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILegalCaseQuestionService
    {
        Task<bool> AnyByDescription(string description, Guid? ignoredId = null);
        Task<bool> HasRespones(Guid id);
        Task Insert(LegalCaseQuestion entity);
        Task Update(LegalCaseQuestion entity);
        Task Delete(LegalCaseQuestion entity);
        Task<LegalCaseQuestion> Get(Guid id);
        Task<DataTablesStructs.ReturnedData<object>> GetLegalcaseQuestionsDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
        Task<IEnumerable<LegalCaseQuestion>> GetLegalCaseQuestions(Guid legalCaseId);

    }
}
