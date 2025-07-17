using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerLanguageRepository : IRepository<LawyerLanguage>
    {
        Task<IEnumerable<LawyerLanguage>> GetLanguagesByLawyerId(Guid lawyerId);
        Task<bool> AnyByLawyerId(Guid lawyerId, Guid languageId);
    }
}
