using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class PlanBenefit
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Key]
        public string PlanId { get; set; }
        [Key]
        public Guid BenefitId { get; set; }
        public Plan Plan { get; set; }
        public Benefit Benefit { get; set; }
    }
}
