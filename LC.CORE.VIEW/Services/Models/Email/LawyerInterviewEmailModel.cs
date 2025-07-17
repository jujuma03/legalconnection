using System;
using System.Collections.Generic;
using System.Text;

namespace LC.CORE.VIEW.Services.Models.Email
{
    public class LawyerInterviewEmailModel
    {
        public string LinkRedirect { get; set; }
        public string LinkName { get; set; }
        public List<LawyerInterviewDetailEmailModel> Details { get; set; }
    }

    public class LawyerInterviewDetailEmailModel
    {
        public string Date { get; set; }
        public string StartRange { get; set; }
        public string EndRange { get; set; }
    }
}
