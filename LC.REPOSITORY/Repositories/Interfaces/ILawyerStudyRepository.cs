using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerStudyRepository : IRepository<LawyerStudy>
    {
        Task<IEnumerable<LawyerStudy>> GetLawyerStudiesByLawyerId(Guid lawyerId);
        Task<string> GetFeaturedStudy(Guid lawyerId);
        Task<bool> AnyByLawyer(Guid lawyerId);
    }
}
