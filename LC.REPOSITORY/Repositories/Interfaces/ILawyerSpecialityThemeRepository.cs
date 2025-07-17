using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerSpecialityThemeRepository : IRepository<LawyerSpecialityTheme>
    {
        Task<IEnumerable<LawyerSpecialityTheme>> GetSpecialitiesByLawyer(Guid lawyerId);
        Task<ResultCustomModel> ValidateSpecialityTheme(List<Guid> specialityThemeIds);
        Task<bool> AnyByLawyer(Guid lawyerId);
    }
}
