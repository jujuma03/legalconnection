using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class LawyerWithdrawalInfoRepository : Repository<LawyerWithdrawalInfo> , ILawyerWithdrawalInfoRepository
    {
        public LawyerWithdrawalInfoRepository(LegalConnectionContext context) : base(context) { }
    }
}
