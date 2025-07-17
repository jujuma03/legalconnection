using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.LegalCase
{
    public class LegalCaseQuestionViewModel
    {
        public Guid QuestionId { get; set; }
        public string Description { get; set; }
        public Guid? ResponseId { get; set; }
        public string Response { get; set; }
    }
}
