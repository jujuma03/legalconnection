using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LawyerProfile
{
    public class LawyerInfoViewModel
    {
        public string RegisterDate { get; set; }
        public byte Status { get; set; }
        public string ValidationDate { get; set; }
        public Guid? LegalCaseId { get; set; }
    }
}
