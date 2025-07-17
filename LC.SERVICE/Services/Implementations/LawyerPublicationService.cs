using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class LawyerPublicationService : ILawyerPublicationService
    {
        private readonly ILawyerPublicationRepository _lawyerPublicationRepository;

        public LawyerPublicationService(ILawyerPublicationRepository lawyerPublicationRepository)
        {
            _lawyerPublicationRepository = lawyerPublicationRepository;
        }

        public async Task Delete(LawyerPublication entity)
            => await _lawyerPublicationRepository.Delete(entity);

        public async Task<LawyerPublication> Get(Guid id)
            => await _lawyerPublicationRepository.Get(id);

        public async Task<IEnumerable<LawyerPublication>> GetLawyerPublications(Guid? lawyerId, byte? status)
            => await _lawyerPublicationRepository.GetLawyerPublications(lawyerId, status);

        public async Task<DataTablesStructs.ReturnedData<object>> GetPublicationsDatatable(DataTablesStructs.SentParameters sentparameters, byte status, string search)
        {
            return await _lawyerPublicationRepository.GetPublicationsDatatable(sentparameters, status, search);
        }

        public async Task Insert(LawyerPublication entity)
            => await _lawyerPublicationRepository.Insert(entity);

        public async Task Update(LawyerPublication publication)
        {
            await _lawyerPublicationRepository.Update(publication);
        }
        public async Task<PaginationStructs.ReturnedData<LawyerPublication>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters, Guid lawyerid)
        {
            return await _lawyerPublicationRepository.GetLawyerPublications(sentParameters, lawyerid);
        }
    }
}
