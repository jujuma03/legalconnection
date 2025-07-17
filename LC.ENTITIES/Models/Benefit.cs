using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Benefit
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ICollection<PlanBenefit> PlanBenefits { get; set; }
    }
}
