using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Models.LegalCase
{
    public class LegalCaseDetailViewModel
    {
        public Guid Id { get; set; }
        public bool IsDirected { get; set; }
        public int MaxVacancies { get; set; }
        public List<LegalCaseLawyerViewModel> LegalCaseLawyers { get; set; }
    }
}
