using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseApplicantLawyer
    {
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        [Key]
        public Guid LawyerId { get; set; }
        [Key]
        public Guid LegalCaseId { get; set; }
        public Lawyer Lawyer { get; set; }
        public LegalCase LegalCase { get; set; }
        public byte Status { get; set; } = ConstantHelpers.ENTITIES.LEGAL_CASE_APPLICANT_LAWYER.STATUS.PENDING;
        public int ResponseTime { get; set; }
    }
}
