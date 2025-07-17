using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerPublicationService
    {
        Task<IEnumerable<LawyerPublication>> GetLawyerPublications(Guid? lawyerId, byte? status);
        Task Insert(LawyerPublication entity);
        Task<LawyerPublication> Get(Guid id);
        Task Delete(LawyerPublication entity);
        Task<DataTablesStructs.ReturnedData<object>> GetPublicationsDatatable(DataTablesStructs.SentParameters sentparameters, byte status, string search);
        Task Update(LawyerPublication publication);
        Task<PaginationStructs.ReturnedData<LawyerPublication>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters, Guid lawyerid);
    }
}
