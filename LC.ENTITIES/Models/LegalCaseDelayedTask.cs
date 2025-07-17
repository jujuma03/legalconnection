using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseDelayedTask
    {
        [Key]
        public Guid LegalCaseId { get; set; }
        [Key]
        public byte Task { get; set; }
        public string HangfireJobId { get; set; }
        public LegalCase LegalCase { get; set; }
    }
}
