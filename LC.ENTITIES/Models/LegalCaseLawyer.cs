using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseLawyer
    {
        [Key]
        public Guid LawyerId { get; set; }
        [Key]
        public Guid LegalCaseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public byte Status { get; set; } = ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.PENDING;
        public Lawyer Lawyer { get; set; }
        public LegalCase LegalCase { get; set; }
        public int ResponseTime { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Fee { get; set; }
    }
}
