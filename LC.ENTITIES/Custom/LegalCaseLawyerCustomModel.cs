using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class LegalCaseLawyerCustomModel
    {
        public Guid LawyerId { get; set; }
        public List<LegalCaseCustomModel> LegalCases { get; set; }
    }
}
