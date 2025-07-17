using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.LegalCase
{
    public class PostulateViewModal
    {
        public Guid LegalCaseId { get; set; }
        public int ResponseTime { get; set; }
    }
}
