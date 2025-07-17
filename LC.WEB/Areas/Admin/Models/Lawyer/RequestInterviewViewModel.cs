using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Lawyer
{
    public class RequestInterviewViewModel
    {
        public string Date { get; set; }
        public string StartRange { get; set; }
        public string EndRange { get; set; }
        public bool Selected { get; set; }
    }
}
