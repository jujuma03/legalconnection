using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public Guid LegalCaseQuestionId { get; set; }
        public LegalCaseQuestion LegalCaseQuestion { get; set; }
        public Guid LegalCaseId { get; set; }
        public LegalCase LegalCase { get; set; }
    }
}
