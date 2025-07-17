using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILegalCaseResponseService
    {
        Task Insert(LegalCaseResponse entity);
        Task Update(LegalCaseResponse entity);
        Task<LegalCaseResponse> Get(Guid id);
    }
}
