using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseFiledLawyer
    {
        public DateTime CreatedAt { get; set; }
        public DateTime EndDateTimeAt{ get; set; }
        [Key]
        public Guid LegalCaseId { get; set; }
        [Key]
        public Guid LawyerId { get; set; }
        public LegalCase LegalCase { get; set; }
        public Lawyer Lawyer { get; set; }
    }
}
