using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task<Select2Structs.ResponseParameters> GetLanguagesSelect2(Select2Structs.RequestParameters parameters, string searchValue);
    }
}
