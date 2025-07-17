using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LawyerProfile
{
    public class ProfileViewModel
    {
        public Guid LawyerId { get; set; }
        public string AboutMe { get; set; }
        public PersonalInformationViewModel PersonalInformation { get; set; }
        public BasicInformationViewModel BasicInformation { get; set; }
        public List<ExperienceViewModel> Experiences { get; set; }
        public List<StudyViewModel> Studies { get; set; }
        public List<PublicationViewModel> Publications { get; set; }
        public List<LanguageViewModel> Languages { get; set; }
        public List<QualificationViewModel> Qualifications { get; set; }
        public bool CanAccessToViewInfo { get; set; }
        public bool ProfileWithChanges { get; set; }
        public bool ViewContact { get; set; }
        public LawyerInfoViewModel LawyerInformation { get; set; }
    }
}
