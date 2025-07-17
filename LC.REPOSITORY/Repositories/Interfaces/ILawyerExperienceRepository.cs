using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerExperienceRepository : IRepository<LawyerExperience>
    {
        Task<IEnumerable<LawyerExperience>> GetLawyerExperiencesByLawyerId(Guid lawyerId);
        Task<string> GetTotalExperienceByLawyerId(Guid lawyerId);
        Task<bool> AnyByLawyer(Guid lawyerId);
    }
}
