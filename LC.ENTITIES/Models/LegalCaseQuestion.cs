using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseQuestion
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ICollection<LegalCaseResponse> LegalCaseResponses { get; set; }
    }
}
