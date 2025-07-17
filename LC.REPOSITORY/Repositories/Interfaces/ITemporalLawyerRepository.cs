using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface ITemporalLawyerRepository : IRepository<TemporalLawyer>
    {
        Task<TemporalLawyer> GetTemporalLawyer(Guid lawyerId);
        Task SaveTemporalLawyer(TemporalLawyer entity);
        Task AcceptProfileChanges(Guid lawyerId);
        Task RejectProfileChanges(Guid lawyerId);
        Task<TemporalLawyerExperience> GetTemporalLawyerExperience(Guid lawyerExperienceId);
        Task<TemporalLawyerStudy> GetTemporalLawyerStudy(Guid lawyerStudyId);
        Task<TemporalLawyerLanguage> GetTemporalLawyerLanguage(Guid lawyerLanguageId);
    }
}
