using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerPublicationRepository : IRepository<LawyerPublication>
    {
        Task<IEnumerable<LawyerPublication>> GetLawyerPublications(Guid? lawyerId, byte? status);
        Task<DataTablesStructs.ReturnedData<object>> GetPublicationsDatatable(DataTablesStructs.SentParameters sentparameters, byte status, string search);
        Task<PaginationStructs.ReturnedData<LawyerPublication>> GetLawyerPublications(PaginationStructs.SentParameters sentParameters, Guid lawyerid);
    }
}
