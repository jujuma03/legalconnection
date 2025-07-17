using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerExperienceService
    {
        Task Insert(LawyerExperience entity);
        Task Update(LawyerExperience entity);
        Task<LawyerExperience> Get(Guid id);
        Task Delete(LawyerExperience entity);
        Task<IEnumerable<LawyerExperience>> GetLawyerExperiencesByLawyerId(Guid lawyerId);
        Task<string> GetTotalExperienceByLawyerId(Guid lawyerId);
        Task<bool> AnyByLawyer(Guid lawyerId);

    }
}
