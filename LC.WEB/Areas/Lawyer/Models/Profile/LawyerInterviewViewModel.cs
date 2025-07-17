using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class LawyerInterviewViewModel
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string StartRange { get; set; }
        public string EndRange { get; set; }
        public bool Selected { get; set; }
    }
}
