using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILegalCaseLawyerRepository : IRepository<LegalCaseLawyer>
    {
        Task<ResultCustomModel> AccessLegalCaseLawyerInfo(Guid lawyerId, Guid? legalCaseId, Guid? clientId);
        Task<LegalCaseLawyer> Get(Guid legalCaseId, Guid lawyerId);
    }
}
