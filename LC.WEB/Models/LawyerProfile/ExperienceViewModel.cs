using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LawyerProfile
{
    public class ExperienceViewModel
    {
        public string Company { get; set; }
        public string Position { get; set; }
        public string WorkArea { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PhotoUrl { get; set; }
    }
}
