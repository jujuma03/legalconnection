using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Qualification
{
    public class QualificationViewModel
    {
        public Guid LegalCaseId { get; set; }
        public ClientViewModel Client { get; set; }
        public LawyerViewModel Lawyer { get; set; }
        public string Commentary { get; set; }
        public int Qualification { get; set; }
    }
}
