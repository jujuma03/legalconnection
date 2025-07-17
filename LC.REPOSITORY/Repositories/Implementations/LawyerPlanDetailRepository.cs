using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class LawyerPlanDetailRepository : Repository<LawyerPlanDetail> , ILawyerPlanDetailRepository
    {
        public LawyerPlanDetailRepository(LegalConnectionContext context) :base(context) { }
    }
}
