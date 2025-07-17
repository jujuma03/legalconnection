using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILegalCaseQuestionRepository : IRepository<LegalCaseQuestion>
    {
        Task<bool> AnyByDescription(string description, Guid? ignoredId = null);
        Task<bool> HasResponses(Guid id);
        Task<DataTablesStructs.ReturnedData<object>> GetLegalcaseQuestionsDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
        Task<IEnumerable<LegalCaseQuestion>> GetLegalCaseQuestions(Guid legalCaseId);
    }
}
