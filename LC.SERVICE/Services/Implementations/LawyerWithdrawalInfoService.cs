using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerWithdrawalInfoService : ILawyerWithdrawalInfoService
    {
        private readonly ILawyerWithdrawalInfoRepository _lawyerWithdrawalInfoRepository;

        public LawyerWithdrawalInfoService(ILawyerWithdrawalInfoRepository lawyerWithdrawalInfoRepository)
        {
            _lawyerWithdrawalInfoRepository = lawyerWithdrawalInfoRepository;
        }

        public async Task<LawyerWithdrawalInfo> Get(Guid id)
            => await _lawyerWithdrawalInfoRepository.Get(id);

        public async Task Insert(LawyerWithdrawalInfo entity)
            => await _lawyerWithdrawalInfoRepository.Insert(entity);

        public async Task Update(LawyerWithdrawalInfo entity)
            => await _lawyerWithdrawalInfoRepository.Update(entity);       
    }
}
