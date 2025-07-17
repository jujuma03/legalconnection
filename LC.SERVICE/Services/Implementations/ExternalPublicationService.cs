using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class ExternalPublicationService : IExternalPublicationService
    {
        private readonly IExternalPublicationRepository _externalPublicationRepository;

        public ExternalPublicationService(IExternalPublicationRepository externalPublicationRepository)
        {
            _externalPublicationRepository = externalPublicationRepository;
        }

        public async Task Delete(ExternalPublication entity)
            => await _externalPublicationRepository.Delete(entity);

        public async Task<ExternalPublication> Get(Guid id)
            => await _externalPublicationRepository.Get(id);

        public async Task<DataTablesStructs.ReturnedData<object>> GetExternalPublicationDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
            => await _externalPublicationRepository.GetExternalPublicationDatatable(parameters, searchValue);

        public async Task Insert(ExternalPublication entity)
            => await _externalPublicationRepository.Insert(entity);

        public async Task Update(ExternalPublication entity)
            => await _externalPublicationRepository.Update(entity);
    }
}
