using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerStudyService
    {
        Task<IEnumerable<LawyerStudy>> GetLawyerStudiesByLawyerId(Guid lawyerId);
        Task Insert(LawyerStudy entity);
        Task Update(LawyerStudy entity);
        Task<LawyerStudy> Get(Guid id);
        Task Delete(LawyerStudy entity);
        Task<string> GetFeaturedStudy(Guid lawyerId);

        Task<bool> AnyByLawyer(Guid lawyerId);
    }
}
