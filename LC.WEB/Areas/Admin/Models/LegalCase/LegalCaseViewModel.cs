using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.LegalCase
{
    public class LegalCaseViewModel
    {
        public Guid Id { get; set; }
        public byte Status { get; set; }
        public List<LegalCaseQuestionViewModel> LegalCaseQuestions { get; set; }
        public bool Completed { get; set; }
    }
}
