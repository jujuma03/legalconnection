using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerObservationRepository : IRepository<LawyerObservation>
    {
        Task<LawyerObservation> GetLastObservationByType(Guid lawyerId, byte process);
    }
}
