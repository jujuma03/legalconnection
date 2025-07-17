using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerCardRepository : IRepository<LawyerCard>
    {
        Task<PaginationStructs.ReturnedData<LawyerCard>> GetLawyerCards(PaginationStructs.SentParameters sentParameters, Guid lawyerId);
        Task<int> GetLawyerCardQuantity(Guid lawyerId);
        Task<LawyerCard> GetDefaultLawyerCard(Guid lawyerId);
    }
}
