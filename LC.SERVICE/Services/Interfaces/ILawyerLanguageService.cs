using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerLanguageService
    {
        Task<IEnumerable<LawyerLanguage>> GetLanguagesByLawyerId(Guid lawyerId);
        Task Delete(LawyerLanguage entity);
        Task<LawyerLanguage> Get(Guid id);
        Task Insert(LawyerLanguage entity);
        Task Update(LawyerLanguage entity);
        Task<bool> AnyByLawyerId(Guid lawyerId, Guid languageId);
    }
}
