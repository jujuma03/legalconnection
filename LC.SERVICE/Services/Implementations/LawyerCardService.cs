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
    public class LawyerCardService : ILawyerCardService
    {
        private readonly ILawyerCardRepository _lawyerCardRepository;

        public LawyerCardService(ILawyerCardRepository lawyerCardRepository)
        {
            _lawyerCardRepository = lawyerCardRepository;
        }

        public async Task Delete(LawyerCard entity)
            => await _lawyerCardRepository.Delete(entity);

        public async Task<LawyerCard> Get(string id)
            => await _lawyerCardRepository.Get(id);

        public async Task<LawyerCard> GetDefaultLawyerCard(Guid lawyerId)
            => await _lawyerCardRepository.GetDefaultLawyerCard(lawyerId);

        public async Task<int> GetLawyerCardQuantity(Guid lawyerId)
            => await _lawyerCardRepository.GetLawyerCardQuantity(lawyerId);

        public async Task<PaginationStructs.ReturnedData<LawyerCard>> GetLawyerCards(PaginationStructs.SentParameters sentParameters, Guid lawyerId)
            => await _lawyerCardRepository.GetLawyerCards(sentParameters, lawyerId);

        public async Task Insert(LawyerCard entity)
            => await _lawyerCardRepository.Insert(entity);
    }
}
