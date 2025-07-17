using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerQualificationService : ILawyerQualificationService
    {
        private readonly ILawyerQualificationRepository _lawyerQualificationRepository;

        public LawyerQualificationService(ILawyerQualificationRepository lawyerQualificationRepository)
        {
            _lawyerQualificationRepository = lawyerQualificationRepository;
        }

        public async Task<bool> AnyQualificationByFilter(Guid legalCaseId, Guid clientId, Guid lawyerId)
            => await _lawyerQualificationRepository.AnyQualificationByFilter(legalCaseId, clientId, lawyerId);

        public async Task<PaginationStructs.ReturnedData<LawyerQualification>> GetLawyerQualifaction(PaginationStructs.SentParameters sentParameters,Guid lawyerId)
            => await _lawyerQualificationRepository.GetLawyerQualifaction(sentParameters,lawyerId);

        public async Task<IEnumerable<LawyerQualification>> GetLawyerQualificationToProfile(Guid lawyerId)
            => await _lawyerQualificationRepository.GetLawyerQualificationToProfile(lawyerId);

        public async Task<int> GetTotalQualification(Guid lawyerId)
            => await _lawyerQualificationRepository.GetTotalQualification(lawyerId);

        public async Task<int> QualificationQuantity(Guid lawyerId)
            => await _lawyerQualificationRepository.QualificationQuantity(lawyerId);

        public async Task<ResultCustomModel> SendQualification(LawyerQualification entity)
            => await _lawyerQualificationRepository.SendQualification(entity);
    }
}
