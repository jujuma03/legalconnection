using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerQualificationRepository : IRepository<LawyerQualification>
    {
        Task<ResultCustomModel> SendQualification(LawyerQualification entity);
        Task<bool> AnyQualificationByFilter(Guid legalCaseId, Guid clientId, Guid lawyerId);
        Task<int> GetTotalQualification(Guid lawyerId);
        Task<IEnumerable<LawyerQualification>> GetLawyerQualificationToProfile(Guid lawyerId);
        Task<int> QualificationQuantity(Guid lawyerId);
        Task<PaginationStructs.ReturnedData<LawyerQualification>> GetLawyerQualifaction(PaginationStructs.SentParameters sentParameters,Guid lawyerId);
    }
}
