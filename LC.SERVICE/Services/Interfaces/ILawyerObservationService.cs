using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerObservationService
    {
        public Task Insert(LawyerObservation entity);
        Task<LawyerObservation> GetLastObservationByType(Guid lawyerId, byte process);
        Task Update(LawyerObservation entity);
    }
}
