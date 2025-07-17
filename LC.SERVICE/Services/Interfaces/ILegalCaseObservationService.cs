using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILegalCaseObservationService
    {
        Task<LegalCaseObservation> GetLastPendingObservationByType(Guid legalCaseId, byte process);
        Task Insert(LegalCaseObservation entity);
        Task Update(LegalCaseObservation entity);
    }
}
