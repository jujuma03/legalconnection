using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerInterviewService : ILawyerInterviewService
    {
        private readonly ILawyerInterviewRepository _lawyerInterviewRepository;

        public LawyerInterviewService(ILawyerInterviewRepository lawyerInterviewRepository)
        {
            _lawyerInterviewRepository = lawyerInterviewRepository;
        }

        public async Task<LawyerInterview> Get(Guid id)
            => await _lawyerInterviewRepository.Get(id);

        public async Task<IEnumerable<LawyerInterview>> GetInterviewsByLawyer(Guid lawyerId)
            => await _lawyerInterviewRepository.GetInterviewsByLawyer(lawyerId);

        public async Task InsertRange(IEnumerable<LawyerInterview> entities)
            => await _lawyerInterviewRepository.InsertRange(entities);

        public async Task Update(LawyerInterview entity)
            => await _lawyerInterviewRepository.Update(entity);
    }
}
