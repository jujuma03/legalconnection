using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class StudyViewModel
    {
        public Guid? Id { get; set; }
        public byte Grade { get; set; }
        public string Mention { get; set; }
        public string Ubication { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}
