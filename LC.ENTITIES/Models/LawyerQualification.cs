using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerQualification
    {
        [Key]
        public Guid LawyerId { get; set; }
        [Key]
        public Guid ClientId { get; set; }
        [Key]
        public Guid LegalCaseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Commentary { get; set; }
        public int Qualification { get; set; }
        public Lawyer Lawyer { get; set; }
        public Client Client { get; set; }
        public LegalCase LegalCase { get; set; }
    }
}
