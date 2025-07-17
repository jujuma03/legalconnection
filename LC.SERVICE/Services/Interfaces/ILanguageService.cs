using LC.CORE.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<Select2Structs.ResponseParameters> GetLanguagesSelect2(Select2Structs.RequestParameters parameters, string searchValue);
    }
}
