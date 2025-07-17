using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerCardService
    {
        Task Insert(LawyerCard entity);
        Task Delete(LawyerCard entity);
        Task<LawyerCard> Get(string id);
        Task<PaginationStructs.ReturnedData<LawyerCard>> GetLawyerCards(PaginationStructs.SentParameters sentParameters, Guid lawyerId);
        Task<int> GetLawyerCardQuantity(Guid lawyerId);
        Task<LawyerCard> GetDefaultLawyerCard(Guid lawyerId);
    }
}
