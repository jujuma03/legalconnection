using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ILawyerInterviewRepository : IRepository<LawyerInterview>
    {
        Task<IEnumerable<LawyerInterview>> GetInterviewsByLawyer(Guid lawyerId);
    }
}
