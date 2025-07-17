using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILegalCaseLawyerService
    {
        Task<ResultCustomModel> AccessLegalCaseLawyerInfo(Guid lawyerId, Guid? legalCaseId, Guid? clientId);
        Task<LegalCaseLawyer> Get(Guid legalCaseId, Guid lawyerId);
    }
}
