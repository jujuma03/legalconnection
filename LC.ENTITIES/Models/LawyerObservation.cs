using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerObservation
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid Id { get; set; }
        public string Observation { get; set; }
        public byte Process { get; set; }
        public bool HasBeenCorrected { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
    }
}
