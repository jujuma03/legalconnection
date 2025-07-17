using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Lawyer
{
    public class LawyerInfoViewModel
    {
        public Guid LawyerId { get; set; }
        public byte Status { get; set; }
        public string RegisterDate { get; set; }
        public string ValidatedDate { get; set; }
        public bool ProfileWithChanges { get; set; }
        public List<RequestInterviewViewModel> RequestInterviews { get; set; }
        public LawyerObservationViewModel LawyerObservation { get; set; }
    }
}
