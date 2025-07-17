using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerQualificationService
    {
        Task<ResultCustomModel> SendQualification(LawyerQualification entity);
        Task<bool> AnyQualificationByFilter(Guid legalCaseId, Guid clientId, Guid lawyerId);
        Task<int> GetTotalQualification(Guid lawyerId);
        Task<int> QualificationQuantity(Guid lawyerId);
        Task<IEnumerable<LawyerQualification>> GetLawyerQualificationToProfile(Guid lawyerId);
        Task<PaginationStructs.ReturnedData<LawyerQualification>> GetLawyerQualifaction(PaginationStructs.SentParameters sentParameters, Guid lawyerId);
    }
}
