using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseObservation
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid Id { get; set; }
        public string Observation { get; set; }
        public byte Process { get; set; }
        public bool HasBeenCorrected { get; set; }
        public Guid LegalCaseId { get; set; }
        //Datos del que registro la observación
        public string CreatorUserId { get; set; }
        public string CreatorRoleId { get; set; }
        public LegalCase LegalCase { get; set; }
        public ApplicationUser CreatorUser { get; set; }
        public ApplicationRole CreatorRole { get; set; }
    }
}
