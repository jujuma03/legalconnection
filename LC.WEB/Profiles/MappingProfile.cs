using AutoMapper;
using LC.REPOSITORY.Repositories.Template;
using LC.WEB.Areas.Admin.Models.LegalCase;

namespace AKDEMIC.LC.WEB.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LegalCaseExcelTemplate, CaseViewModel>();
        }
    }
}
