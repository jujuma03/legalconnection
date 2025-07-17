using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.LegalCase
{
    public class RejectViewModel
    {
        public Guid LegalCaseId { get; set; }
        public string Observations { get; set; }
    }
}
