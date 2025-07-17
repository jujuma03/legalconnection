using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Plan
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string DescriptionLC { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Amount { get; set; }
        public int IntervalCount { get; set; }
        public byte Interval { get; set; }
        public int TrialDays { get; set; }
        public ICollection<PlanBenefit> PlanBenefits { get; set; }
    }
}
