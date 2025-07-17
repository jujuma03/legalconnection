using LC.CORE.Structs;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<Select2Structs.ResponseParameters> GetLanguagesSelect2(Select2Structs.RequestParameters parameters, string searchValue)
            => await _languageRepository.GetLanguagesSelect2(parameters, searchValue);
    }
}
