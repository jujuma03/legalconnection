using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerSpecialityThemeService
    {
        Task<IEnumerable<LawyerSpecialityTheme>> GetSpecialitiesByLawyer(Guid lawyerId);
        Task<LawyerSpecialityTheme> Get(Guid id);
        Task InsertRange(IEnumerable<LawyerSpecialityTheme> entities);
        Task DeleteRange(IEnumerable<LawyerSpecialityTheme> entities);
        Task<ResultCustomModel> ValidateSpecialityTheme(List<Guid> specialityThemeIds);
        Task<bool> AnyByLawyer(Guid lawyerId);
    }

}
