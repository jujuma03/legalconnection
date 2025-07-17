using LC.ENTITIES.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface ILawyerInterviewService
    {
        Task InsertRange(IEnumerable<LawyerInterview> entities);
        Task<LawyerInterview> Get(Guid id);
        Task Update(LawyerInterview entity);
        Task<IEnumerable<LawyerInterview>> GetInterviewsByLawyer(Guid lawyerId);
    }
}
