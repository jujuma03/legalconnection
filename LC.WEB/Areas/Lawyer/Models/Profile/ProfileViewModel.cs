using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class ProfileViewModel
    {
        public Guid LawyerId { get; set; }
        public PersonalInformationViewModel PersonalInformation { get; set; }
        public BasicInformationViewModel BasicInformation { get; set; }
        public List<LawyerInterviewViewModel> LawyerInterviews { get; set; }
        public string AboutMe { get; set; }
        public LawyerObservationViewModel LawyerObservation { get; set; }
        public bool PublicProfile { get; set; }
        public byte Status { get; set; }
        public bool FreeUser { get; set; }
    }

}
